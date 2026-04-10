using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using UglyToad.PdfPig;
using UglyToad.PdfPig.Content;
using ClosedXML.Excel;

namespace QrApp.Utilidades
{
    public class Registro
    {
        public string OrigenBloque { get; set; } = "";
        public int? Item { get; set; }
        public string Guia { get; set; } = "";
        public string Tipo { get; set; } = "";
        public DateTime? Fecha { get; set; }
        public string OrigenCiudad { get; set; } = "";
        public string Usuario { get; set; } = "";
        public string ClienteUnidad { get; set; } = "";
        public string CiudadDestino { get; set; } = "";
        public string SectorDestino { get; set; } = "";
        public string Destinatario { get; set; } = "";
        public string Contenido { get; set; } = "";
        public decimal? PKg { get; set; }
        public decimal? PVol { get; set; }
        public decimal? Valor { get; set; }
        public decimal? Seg { get; set; }
        public decimal? IVA { get; set; }
        public decimal? Total { get; set; }
        // Extras numéricos adicionales (Fuel, Reto, Adic, etc.) si existen
        public List<decimal?> Extras { get; set; } = new List<decimal?>();
        // Página donde se encontró el registro (útil para depuración y evitar mezclas entre páginas)
        public int Page { get; set; }
    }

    public class PdfPagina
    {
        public int NumeroPagina { get; set; }
        public List<string> Lineas { get; set; } = new List<string>();
    }

    public static class CopiarRegistrosPdf
    {
        // Regex que acepta entre 6 y 8 números finales agrupados
        private static readonly Regex regexFilaFlexible = new Regex(
            @"^\s*(?<item>\d+)\s+(?<guia>\d{6,13})\s+(?<tipo>[A-Z])\s+(?<fecha>\d{4}[/\-]\d{2}[/\-]\d{2}|\d{2}[/\-]\d{2}[/\-]\d{4})\s+(?<orig>[A-ZÁÉÍÓÚÑ]+)\s+(?<user>[A-Za-z0-9._-]+)\s+(?<resto>.+?)\s+(?<numbers>(?:[\d.,]+\s+){5,7}[\d.,]+)\s*$",
            RegexOptions.Compiled);

        private static readonly Regex regexOrigenBloque = new Regex(@"^\s*Origen:\s*(?<org>.+)$", RegexOptions.Compiled | RegexOptions.IgnoreCase);

        public static List<Registro> ExtraerRegistros(string pdfPath)
        {
            var lista = new List<Registro>();
            string origenBloqueActual = "";

            var paginas = LeerPaginasPorSeparado(pdfPath);

            foreach (var pagina in paginas)
            {
                var regs = ExtraerRegistrosDesdeLineas(pagina.Lineas, ref origenBloqueActual, pagina.NumeroPagina);
                lista.AddRange(regs);
            }

            return lista;
        }

        // Une líneas hasta detectar 6..8 números finales y parsea la fila completa
        public static List<Registro> ExtraerRegistrosDesdeLineas(IEnumerable<string> linesEnum, ref string origenBloqueActual, int pageNumber)
        {
            var lines = linesEnum.ToList();
            var lista = new List<Registro>();

            for (int i = 0; i < lines.Count; i++)
            {
                var raw = Normalizar(lines[i]);
                if (string.IsNullOrWhiteSpace(raw)) continue;

                // Detectar origen de bloque
                var mOrigen = regexOrigenBloque.Match(raw);
                if (mOrigen.Success)
                {
                    origenBloqueActual = mOrigen.Groups["org"].Value.ToUpperInvariant().Trim();
                    continue;
                }

                // Intentar combinar con siguientes líneas hasta que encontremos los 6..8 números finales
                string buffer = raw;
                Match m = regexFilaFlexible.Match(buffer);
                int lookahead = 0;
                while (!m.Success && i + lookahead + 1 < lines.Count && lookahead < 4)
                {
                    // anexar siguiente línea y reintentar
                    lookahead++;
                    buffer = buffer + " " + Normalizar(lines[i + lookahead]);
                    m = regexFilaFlexible.Match(buffer);
                }

                if (!m.Success)
                {
                    // no se pudo parsear como fila completa; intentar anexar a último contenido si aplica
                    if (lista.Count > 0)
                    {
                        var last = lista.Last();
                        bool pareceNuevaFila = Regex.IsMatch(raw, "^\\s*\\d+\\s+");
                        bool esCabeceraPagina = Regex.IsMatch(raw, "^Page\\s+\\d+|^\\d+\\s*/\\s*\\d+|^Total|^Fecha|^Origen", RegexOptions.IgnoreCase);
                        if (!pareceNuevaFila && !esCabeceraPagina && last.Page == pageNumber)
                        {
                            if (!string.IsNullOrWhiteSpace(last.Contenido)) last.Contenido += " ";
                            last.Contenido += raw.Trim();
                            continue;
                        }
                    }

                    // Si no se pudo, saltar
                    continue;
                }

                // Si combinamos varias líneas, avanzar el índice
                if (lookahead > 0) i += lookahead;

                // Construir registro desde match
                var reg = new Registro();
                reg.Page = pageNumber;
                reg.OrigenBloque = origenBloqueActual;
                reg.Item = SafeInt(m.Groups["item"].Value);
                reg.Guia = m.Groups["guia"].Value;
                reg.Tipo = m.Groups["tipo"].Value;

                reg.Fecha = SafeDate(m.Groups["fecha"].Value);
                reg.OrigenCiudad = m.Groups["orig"].Value;
                reg.Usuario = m.Groups["user"].Value;

                var resto = m.Groups["resto"].Value.Replace("\\", " ").Replace("|", " ").Replace("  ", " ").Trim();

                // Separar los números finales (6..8)
                var numbersText = m.Groups["numbers"].Value.Trim();
                var parts = Regex.Split(numbersText, "\\s+").Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();

                // Parsear todos en Extras, y asignar campos principales
                var nums = parts.Select(p => SafeDec(p)).ToArray();
                for (int k = 0; k < nums.Length; k++) reg.Extras.Add(nums[k]);

                if (nums.Length >= 1) reg.PKg = nums[0];
                if (nums.Length >= 2) reg.PVol = nums[1];
                if (nums.Length >= 3) reg.Valor = nums[2];
                if (nums.Length >= 4) reg.Seg = nums[3];
                if (nums.Length >= 6)
                {
                    // IVA is second last, Total is last
                    reg.IVA = nums[nums.Length - 2];
                    reg.Total = nums[nums.Length - 1];
                }
                else if (nums.Length == 5)
                {
                    // fallback: treat 5 as IVA+Total shifted
                    reg.IVA = nums[3];
                    reg.Total = nums[4];
                }

                // Intentar partir resto en ClienteUnidad, CiudadDestino/Sector y Destinatario/Contenido
                var destinoMatch = Regex.Match(resto, @"(?<cd>[A-ZÁÉÍÓÚÑ ]+)\s*/\s*(?<sd>[A-ZÁÉÍÓÚÑ ]+)");
                if (destinoMatch.Success)
                {
                    var idx = destinoMatch.Index;
                    reg.ClienteUnidad = resto.Substring(0, idx).Trim();

                    reg.CiudadDestino = destinoMatch.Groups["cd"].Value.Trim();
                    reg.SectorDestino = destinoMatch.Groups["sd"].Value.Trim();

                    var after = resto.Substring(idx + destinoMatch.Length).Trim();

                    var destContenido = Regex.Match(after, @"^(?<dest>[^/]+)\s*/\s*(?<cont>.+)$");
                    if (destContenido.Success)
                    {
                        reg.Destinatario = destContenido.Groups["dest"].Value.Trim();
                        reg.Contenido = destContenido.Groups["cont"].Value.Trim().TrimEnd(',');
                    }
                    else
                    {
                        reg.Contenido = after.Trim().TrimEnd(',');
                    }
                }
                else
                {
                    reg.ClienteUnidad = resto.Trim();
                }

                lista.Add(reg);
            }

            return lista;
        }

        public static string Normalizar(string s)
        {
            // Quitar dobles espacios, caracteres raros y uniformar
            if (s == null) return "";
            s = s.Replace("\u00A0", " "); // NBSP
            while (s.Contains("  ")) s = s.Replace("  ", " ");
            return s.Trim();
        }

        public static int? SafeInt(string v)
            => int.TryParse(v, out var n) ? n : (int?)null;

        public static DateTime? SafeDate(string v)
        {
            var formatos = new[] { "yyyy/MM/dd", "yyyy-MM-dd", "dd/MM/yyyy", "dd-MM-yyyy" };
            foreach (var f in formatos)
            {
                if (DateTime.TryParseExact(v, f, CultureInfo.InvariantCulture, DateTimeStyles.None, out var d))
                    return d;
            }
            if (DateTime.TryParse(v, out var df)) return df;
            return null;
        }

        public static decimal? SafeDec(string v)
        {
            if (string.IsNullOrWhiteSpace(v)) return null;
            var clean = v.Trim();
            if (clean.Contains(',') && clean.Contains('.'))
                clean = clean.Replace(",", "");
            else if (clean.Contains(',') && !clean.Contains('.'))
                clean = clean.Replace(",", ".");

            if (decimal.TryParse(clean, NumberStyles.Any, CultureInfo.InvariantCulture, out var n)) return n;
            return null;
        }

        public static List<PdfPagina> LeerPaginasPorSeparado(string rutaPdf)
        {
            var paginas = new List<PdfPagina>();

            using (var pdf = PdfDocument.Open(rutaPdf))
            {
                int total = pdf.NumberOfPages;

                for (int i = 1; i <= total; i++)
                {
                    var page = pdf.GetPage(i);

                    var lineas = new List<string>();

                    var grupos = page.GetWords()
                                     .GroupBy(w => Math.Round(w.BoundingBox.Bottom, 0))
                                     .OrderByDescending(g => g.Key);

                    foreach (var g in grupos)
                    {
                        string linea = string.Join(" ",
                            g.OrderBy(w => w.BoundingBox.Left)
                             .Select(w => w.Text));

                        if (!string.IsNullOrWhiteSpace(linea))
                            lineas.Add(linea.Trim());
                    }

                    paginas.Add(new PdfPagina
                    {
                        NumeroPagina = i,
                        Lineas = lineas
                    });
                }
            }

            return paginas;
        }
    }
}

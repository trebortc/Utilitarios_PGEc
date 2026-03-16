using PdfSharp.Pdf.IO;
using PdfSharp.Pdf;

namespace QrApp.Utilidades;

public static class AccionesArchivos
{
    public static void LimpiarNombreArchivo(string directorioUsar)
    {
        //var directorios = Directory.EnumerateFiles(@"C:\QRs\pdfs\").ToList();
        var directorios = Directory.EnumerateFiles(@directorioUsar).ToList();
        int i = 0;
        //Limpiar los nombres de los archivos que se encuentran en un directorio determinado
        foreach (var directorio in directorios)
        {
            string nombreArchivo = UtilidadesArchivos.NombreArchivo(directorio);
            Console.WriteLine("" + i++ + " " + nombreArchivo);
            File.Move(directorio.ToString(), nombreArchivo);
        }
    }

    public static void InvertirNombresPorApellidos(string directorioUsar)
    {
        var directorios = Directory.EnumerateFiles(@directorioUsar).ToList();
        int i = 0;
        foreach (var directorio in directorios)
        {
            string nombreArchivo = directorio.NombreArchivoRutaSinExtension();
            string extension = directorio.ExtensionArchivoRuta();

            int totalOcurrencias = nombreArchivo.ObtenerOcurrenciasCaracter('_');

            if (!(totalOcurrencias == 3))
            {
                continue;
            }

            string apellidoNombre = nombreArchivo.InvertirOrdenNombresPorApellidos();
            string nuevaRutaDirectorio = directorio.Replace(nombreArchivo, apellidoNombre);

            File.Move(directorio, nuevaRutaDirectorio);
            Console.WriteLine("" + i++ + " " + directorio + "  - / -  " + nuevaRutaDirectorio);
        }
    }

    public static void Virar(string directorioUsar)
    {
        //var directorios = Directory.EnumerateFiles(@directorioUsar).ToList();
        int grados = -90; // grados de rotación: 90, 180 o 270

        foreach (var archivo in Directory.GetFiles(directorioUsar, "*.pdf"))
        {
            using (PdfDocument pdf = PdfReader.Open(archivo, PdfDocumentOpenMode.Modify))
            {
                foreach (PdfPage page in pdf.Pages)
                {
                    // Rotación actual
                    int rotacionActual = page.Rotate;
                    // Nueva rotación
                    page.Rotate = (rotacionActual + grados) % 360;
                }

                pdf.Save(archivo); // guarda sobre el mismo archivo
                                   // pdf.Save(archivo.Replace(".pdf", "_rotado.pdf")); // si prefieres uno nuevo
            }
        }

    }
}

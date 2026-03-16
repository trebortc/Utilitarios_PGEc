namespace QrLibreria;

public static class UtilidadesArchivos
{
    public static string NombreArchivoRutaSinExtension(this string ruta)
    {
        return Path.GetFileNameWithoutExtension(ruta);
    }
    public static string ExtensionArchivoRuta(this string ruta)
    {
        return Path.GetExtension(ruta);
    }
    public static string NombreArchivo(this string ruta)
    {
        return "";
        //return ruta.ObtenerNombreArchivoDeDirectorio().EliminarPalabrasDeDirectorio().LimpiarNombreParticipante() + ".pdf";            
    }
    public static string ObtenerNombreArchivoDeDirectorio(this string ruta)
    {
        return ruta.ToString().Substring(ruta.LastIndexOf("\\") + 1);
    }
    public static string EliminarPalabrasDeDirectorio(this string ruta)
    {
        ruta = ruta.TrimStart();
        ruta = ruta.TrimEnd();
        ruta = ruta.Replace("CERTIFICADO", "");
        ruta = ruta.Replace("signed", "");
        ruta = ruta.Replace("-", "");
        ruta = ruta.Replace("_", "");
        ruta = ruta.Replace(".pdf", "");
        ruta = ruta.TrimStart();
        ruta = ruta.TrimEnd();
        return ruta;
    }
    public static int ObtenerOcurrenciasCaracter(this string palabra, char caracter)
    {
        return palabra.Count(c => c == caracter);
    }
    public static string InvertirOrdenNombresPorApellidos(this string nombreArchivo)
    {
        int posicion = nombreArchivo.IndexOf('_', nombreArchivo.IndexOf('_') + 1);
        string nombre = nombreArchivo.Substring(0, posicion);
        string apellido = nombreArchivo.Substring(posicion + 1);

        return apellido + "_" + nombre ;
    }

}

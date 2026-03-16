namespace QrLibreria;

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
}

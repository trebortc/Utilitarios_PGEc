// See https://aka.ms/new-console-template for more information
using QrLibreria;
Console.WriteLine("Hello, World!");
var directorios = Directory.EnumerateFiles(@"C:\QRs\pdfs\").ToList();
//var directorios = Directory.EnumerateFiles(@"C:\QRs\");
int i = 1;

//Limpiar los nombres de los archivos que se encuentran en un directorio determinado
//foreach (var directorio in directorios)
//{
//    //Console.WriteLine(directorio.ToString());
//    string nombreArchivo = UtilidadesArchivos.NombreArchivo(directorio);
//    Console.WriteLine("" + i++ + " " + nombreArchivo);
//    File.Move(directorio.ToString(), nombreArchivo);
//}

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
    //File.Move(directorio, apellidoNombre + ".pdf");
    Console.WriteLine("" + i++ + " " + directorio + "  - / -  " + nuevaRutaDirectorio);
}

Console.ReadLine();
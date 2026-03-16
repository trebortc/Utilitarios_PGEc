using WinSCP;
using Microsoft.Extensions.Configuration;
using QrServidor.Configuraciones;
namespace QrServidor;

public static class UtilitariosServidor
{
    public static OpcionesSesion ObtenerCredencialesServidor()
    {
        OpcionesSesion opcionesSesion = new OpcionesSesion();

        var config = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory) // Establece la base para leer el archivo
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

        // Leer la configuración
        opcionesSesion.Protocol = config["SessionOptions:Protocol"];
        opcionesSesion.HostName = config["SessionOptions:HostName"];
        opcionesSesion.UserName = config["SessionOptions:UserName"];
        opcionesSesion.Password = config["SessionOptions:Password"];
        opcionesSesion.SshHostKeyFingerprint = config["SessionOptions:SshHostKeyFingerprint"];

        return opcionesSesion;
    }
    public static SessionOptions IniciarSesion(OpcionesSesion opcionesSesion)
    {
        SessionOptions sessionOptions = new SessionOptions
        {
            Protocol = (Protocol)Enum.Parse(typeof(Protocol), opcionesSesion.Protocol),
            HostName = opcionesSesion.HostName,
            UserName = opcionesSesion.UserName,
            Password = opcionesSesion.Password,
            SshHostKeyFingerprint = opcionesSesion.SshHostKeyFingerprint
        };

        return sessionOptions;        
    }
    public static void ObtenerArchivo(SessionOptions sessionOptions)
    {
        using (Session session = new Session())
        {
            // Conecta la sesión
            session.Open(sessionOptions);

            // Descarga los archivos
            TransferOptions transferOptions = new TransferOptions();
            transferOptions.TransferMode = TransferMode.Binary;

            session.GetFiles(@"/PortalWeb/pge/administrator/components/com_akeebabackup/backup/archivo.txt", @"C:\Respaldos\archivoServidor.txt", false, transferOptions);
        }
        Console.WriteLine("Paso de archivo con exito");
        Console.ReadLine();
    }
    public static void CrearCarpeta(SessionOptions sessionOptions, string nombreCarpeta, int oficinaPge)
    {
        using (Session session = new Session())
        {
            // Conecta la sesión
            session.Open(sessionOptions);

            //Obtener año
            int ano = DateTime.Now.Year;


            // Ruta para crear la carpeta
            string remotePath = @"/PortalWeb/pge/images/" + ano + "/diplomas/" + nombreCarpeta;

            // Create new folder in server
            session.CreateDirectory(remotePath);
        }
        Console.WriteLine("Paso de archivo con exito");
        Console.ReadLine();
    }
    public static void SubirCarpeta(SessionOptions sessionOptions, string nombreCarpeta, string nombreCarpetaArchivos)
    {
        using (Session session = new Session())
        {
            //Open session to server
            session.Open(sessionOptions);
            
            //Get year
            int ano = DateTime.Now.Year;

            //Ruta para guardar archivos
            string remotePath = @"/PortalWeb/pge/images/" + ano + "/diplomas/" + nombreCarpeta;

            // Opciones de transferencia
            TransferOptions transferOptions = new TransferOptions
            {
                TransferMode = TransferMode.Binary // Usa Binary para archivos, Text para texto
            };

            // Sube todos los archivos
            TransferOperationResult transferResult = session.PutFiles(nombreCarpetaArchivos, remotePath, false, transferOptions);

            // Comprueba el resultado
            transferResult.Check(); // Lanza una excepción si algo falla

            foreach (TransferEventArgs transfer in transferResult.Transfers)
            {
                Console.WriteLine($"Archivo enviado: {transfer.FileName}");
            }

        }
    }

}

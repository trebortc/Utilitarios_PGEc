namespace QrServidor.Configuraciones;

public class OpcionesSesion
{
    public String Protocol { get; set; }
    public String HostName { get; set; }
    public String UserName { get; set; }
    public String Password { get; set; }
    public String SshHostKeyFingerprint { get; set; }
    public OpcionesSesion()
    {
            
    }
    public OpcionesSesion(String protocol, String hostName, String userName, String password, String sshHostKeyFingerprint)
    {
        Protocol = protocol;
        HostName = hostName;
        UserName = userName;
        Password = password;
        SshHostKeyFingerprint = sshHostKeyFingerprint;
    }
}

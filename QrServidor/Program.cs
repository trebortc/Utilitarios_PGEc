// See https://aka.ms/new-console-template for more information
using QrServidor;

Console.WriteLine("Hello, World!");
var credencialesServidor = UtilitariosServidor.ObtenerCredencialesServidor();
UtilitariosServidor.IniciarSesion(credencialesServidor);

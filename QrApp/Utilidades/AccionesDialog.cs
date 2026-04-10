using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QrApp.Utilidades;

public static class AccionesDialog
{
    public static string? obtenerRutaCarpetaSeleccionada(this FolderBrowserDialog folderDialog)
    {
        folderDialog.Description = "Seleccione una carpeta";

        if ((folderDialog.ShowDialog() != DialogResult.OK))
        {
            return "";
        }

        return folderDialog.SelectedPath;
    }

    public static string? ObtenerArchivoSeleccionado(this OpenFileDialog dlg)
    {

        dlg.Title = "Seleccione un archivo";
        dlg.Filter = "Todos los archivos (*.*)|*.*";


        if ((dlg.ShowDialog() != DialogResult.OK))
        {
            return "";
        }

        return dlg.FileName;
    }

}

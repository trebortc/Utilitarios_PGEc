using DocumentFormat.OpenXml.Packaging;
using A = DocumentFormat.OpenXml.Drawing;
using P = DocumentFormat.OpenXml.Presentation;
using System.Drawing;
using System.Drawing.Imaging;
using System.Reflection;
using System.Runtime.InteropServices;

namespace QrApp.Utilidades;

public static class CertificadosPowerPoint
{
    private const int PpSaveAsPdf = 32;
    private const int MsoFalse = 0;

    public static void CrearDesdePlantilla(string rutaPlantilla, string rutaSalida, string nombrePersona, Bitmap qrBitmap)
    {
        File.Copy(rutaPlantilla, rutaSalida, true);

        using var documento = PresentationDocument.Open(rutaSalida, true);

        bool textoReemplazado = false;
        bool imagenReemplazada = false;

        foreach (var slidePart in ObtenerSlides(documento))
        {
            textoReemplazado |= ReemplazarTexto(slidePart, "{{NOMBRE_PERSONA}}", nombrePersona);
            imagenReemplazada |= ReemplazarImagen(slidePart, "QR_PLACEHOLDER", qrBitmap);
            slidePart.Slide.Save();
        }

        documento.PresentationPart?.Presentation.Save();

        if (!textoReemplazado)
            throw new InvalidOperationException("No se encontró el marcador {{NOMBRE_PERSONA}} en la plantilla de PowerPoint.");

        if (!imagenReemplazada)
            throw new InvalidOperationException("No se encontró la imagen QR_PLACEHOLDER en la plantilla de PowerPoint.");
    }

    public static string ExportarAPdf(string rutaPresentacion)
    {
        string rutaPdf = Path.ChangeExtension(rutaPresentacion, ".pdf");
        ExportarAPdf(rutaPresentacion, rutaPdf);
        return rutaPdf;
    }

    public static void ExportarAPdf(string rutaPresentacion, string rutaPdf)
    {
        Type? powerPointType = Type.GetTypeFromProgID("PowerPoint.Application");
        if (powerPointType == null)
            throw new InvalidOperationException("Microsoft PowerPoint no está instalado en este equipo. No se puede exportar a PDF.");

        object? powerPointApp = null;
        object? presentations = null;
        object? presentation = null;

        try
        {
            powerPointApp = Activator.CreateInstance(powerPointType)
                ?? throw new InvalidOperationException("No se pudo iniciar Microsoft PowerPoint.");

            presentations = powerPointType.InvokeMember("Presentations", BindingFlags.GetProperty, null, powerPointApp, null);
            presentation = presentations?.GetType().InvokeMember(
                "Open",
                BindingFlags.InvokeMethod,
                null,
                presentations,
                new object[] { rutaPresentacion, MsoFalse, MsoFalse, MsoFalse });

            if (presentation == null)
                throw new InvalidOperationException("No se pudo abrir la presentación para exportarla a PDF.");

            presentation.GetType().InvokeMember(
                "SaveAs",
                BindingFlags.InvokeMethod,
                null,
                presentation,
                new object[] { rutaPdf, PpSaveAsPdf });
        }
        catch (TargetInvocationException ex)
        {
            throw new InvalidOperationException("PowerPoint no pudo exportar la presentación a PDF.", ex.InnerException ?? ex);
        }
        finally
        {
            CerrarCom(presentation, "Close");
            CerrarCom(powerPointApp, "Quit");
            LiberarCom(presentation);
            LiberarCom(presentations);
            LiberarCom(powerPointApp);
        }
    }

    private static IEnumerable<SlidePart> ObtenerSlides(PresentationDocument documento)
    {
        var presentationPart = documento.PresentationPart;
        if (presentationPart?.Presentation?.SlideIdList == null)
            yield break;

        foreach (P.SlideId slideId in presentationPart.Presentation.SlideIdList.Elements<P.SlideId>())
        {
            if (presentationPart.GetPartById(slideId.RelationshipId!) is SlidePart slidePart)
                yield return slidePart;
        }
    }

    private static bool ReemplazarTexto(SlidePart slidePart, string marcador, string valor)
    {
        bool reemplazado = false;

        foreach (var texto in slidePart.Slide.Descendants<A.Text>())
        {
            if (!texto.Text.Contains(marcador, StringComparison.Ordinal))
                continue;

            texto.Text = texto.Text.Replace(marcador, valor, StringComparison.Ordinal);
            reemplazado = true;
        }

        return reemplazado;
    }

    private static bool ReemplazarImagen(SlidePart slidePart, string nombreImagen, Bitmap qrBitmap)
    {
        bool reemplazado = false;

        var pictures = slidePart.Slide.Descendants<P.Picture>()
            .Where(p => string.Equals(
                p.NonVisualPictureProperties?.NonVisualDrawingProperties?.Name?.Value,
                nombreImagen,
                StringComparison.OrdinalIgnoreCase));

        foreach (var picture in pictures)
        {
            var relationshipId = picture.BlipFill?.Blip?.Embed?.Value;
            if (string.IsNullOrWhiteSpace(relationshipId))
                continue;

            if (slidePart.GetPartById(relationshipId) is not ImagePart imagePart)
                continue;

            using var stream = new MemoryStream();
            GuardarBitmap(qrBitmap, imagePart.ContentType, stream);
            stream.Position = 0;
            imagePart.FeedData(stream);
            reemplazado = true;
        }

        return reemplazado;
    }

    private static void GuardarBitmap(Bitmap qrBitmap, string contentType, Stream stream)
    {
        var formato = ImageFormat.Png;

        if (string.Equals(contentType, "image/jpeg", StringComparison.OrdinalIgnoreCase))
            formato = ImageFormat.Jpeg;
        else if (string.Equals(contentType, "image/bmp", StringComparison.OrdinalIgnoreCase))
            formato = ImageFormat.Bmp;
        else if (string.Equals(contentType, "image/gif", StringComparison.OrdinalIgnoreCase))
            formato = ImageFormat.Gif;

        qrBitmap.Save(stream, formato);
    }

    private static void CerrarCom(object? comObject, string metodo)
    {
        if (comObject == null)
            return;

        try
        {
            comObject.GetType().InvokeMember(metodo, BindingFlags.InvokeMethod, null, comObject, null);
        }
        catch
        {
        }
    }

    private static void LiberarCom(object? comObject)
    {
        if (comObject == null || !Marshal.IsComObject(comObject))
            return;

        Marshal.FinalReleaseComObject(comObject);
    }
}

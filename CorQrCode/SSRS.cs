
namespace CorQrCode
{
    public class QR
    {
        // https://sites.google.com/site/wsuipartechtips/reporting-services/custom-assemblies-in-ssrs

        // In Visual Studio 2019, there are two locations to place all custom DLLs--each DLL needs to be in both directories:
        // "C:\Program Files (x86)\Microsoft Visual Studio\2019\Enterprise\Common7\IDE\PrivateAssemblies"
        // "C:\Program Files (x86)\Microsoft Visual Studio\2019\Enterprise\Common7\IDE\CommonExtensions\Microsoft\SSRS"

        // C:\Program Files (x86)\Microsoft Visual Studio\2019\Community\Common7\IDE\PrivateAssemblies
        // C:\Program Files (x86)\Microsoft Visual Studio\2019\Community\Common7\IDE\CommonExtensions\Microsoft\SSRS


        // On the report server(SSRS 2008 R2), the file needs to be loaded to "C:\Program Files\Microsoft SQL Server\MSSQL.3\Reporting Services\ReportServer\bin"
        // On the report server(SSRS 2016), the file needs to be loaded to "C:\Program Files\Microsoft SQL Server\MSRS13.MSSQLSERVER\Reporting Services\ReportServer\bin"

        public static byte[] Render(string text, int width, int height)
        {
            libQrCode.QrCode.QRCodeWriter qw = new libQrCode.QrCode.QRCodeWriter();

            byte[] imageBytes = qw.RenderImageBytes(text, libQrCode.BarcodeFormat.QR_CODE, width, height);
            return imageBytes;
        }


    }
}

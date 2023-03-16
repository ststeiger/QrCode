
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

        public static byte[] RenderAndRotate(string text, int width, int height, int angle)
        {
            libQrCode.QrCode.QRCodeWriter qw = new libQrCode.QrCode.QRCodeWriter();

            byte[] imageBytes = qw.RenderImageBytes(text, libQrCode.BarcodeFormat.QR_CODE, width, height);
            try
            {

                if (angle == 0)
                    return imageBytes;

                if (angle == 90)
                    return Rotate90(imageBytes);

                if (angle == 180)
                    return Rotate180(imageBytes);

                if (angle == 270)
                    return Rotate270(imageBytes);

                return RotateImage(imageBytes, angle);
            }
            catch (System.Exception)
            { }

            return imageBytes;
        }


        private static System.Drawing.Image ByteArrayToImage(byte[] byteArrayIn)
        {
            System.Drawing.Image img = null;

            using (System.IO.Stream ms = new System.IO.MemoryStream(byteArrayIn))
            {
                img = System.Drawing.Image.FromStream(ms);
            }

            return img;
        }


        private static byte[] ImageToPngByteArray(System.Drawing.Image image)
        {
            byte[] ba = null;

            using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
            {
                // image.Save(ms, image.RawFormat);
                image.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                ba = ms.ToArray();
            }

            return ba;
        }


        private static byte[] Rotate90(byte[] inputBytes)
        {
            byte[] output = null;

            // create an object that we can use to examine an image file
            // using (System.Drawing.Image img = System.Drawing.Image.FromFile(input))
            using (System.Drawing.Image img = ByteArrayToImage(inputBytes))
            {
                //rotate the picture by 90 degrees and re-save the picture as a Jpeg
                img.RotateFlip(System.Drawing.RotateFlipType.Rotate90FlipNone);
                // img.Save(output, System.Drawing.Imaging.ImageFormat.Jpeg);
                output = ImageToPngByteArray(img);
            }

            return output;
        }


        private static byte[] Rotate180(byte[] inputBytes)
        {
            byte[] output = null;

            using (System.Drawing.Image img = ByteArrayToImage(inputBytes))
            {
                img.RotateFlip(System.Drawing.RotateFlipType.Rotate180FlipNone);
                output = ImageToPngByteArray(img);
            }

            return output;
        }



        private static byte[] Rotate270(byte[] inputBytes)
        {
            byte[] output = null;

            using (System.Drawing.Image img = ByteArrayToImage(inputBytes))
            {
                img.RotateFlip(System.Drawing.RotateFlipType.Rotate270FlipNone);
                output = ImageToPngByteArray(img);
            }

            return output;
        }


        private static byte[] RotateImage(byte[] inputBytes, float angle)
        {
            byte[] output = null;

            using (System.Drawing.Image image = ByteArrayToImage(inputBytes))
            {
                using (System.Drawing.Bitmap rotatedImage = new System.Drawing.Bitmap(image.Width, image.Height))
                {
                    rotatedImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

                    using (System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(rotatedImage))
                    {
                        // Set the rotation point to the center in the matrix
                        g.TranslateTransform(image.Width / 2, image.Height / 2);
                        // Rotate
                        g.RotateTransform(angle);
                        // Restore rotation point in the matrix
                        g.TranslateTransform(-image.Width / 2, -image.Height / 2);
                        // Draw the image on the bitmap
                        g.DrawImage(image, new System.Drawing.Point(0, 0));
                    } // End Using g 

                    output = ImageToPngByteArray(rotatedImage);
                } // End Using rotatedImage

            } // End Using image 

            return output;
        } // End Sub RotateImage 


    } // End Class QR 


} // End Namespace SSRS

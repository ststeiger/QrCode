
namespace TestQrCode
{


    public class SimpleBitmapHandler
    {

        private enum PixelFormat
        {
            Rgb24bpp
        }


        public static System.Drawing.Image GetImage(libQrCode.Common.BitMatrix matrix)
        {
            int width = matrix.Width;
            int height = matrix.Height;

            byte[] bmpBuffer = new byte[width * height * 3];

            int pos = 0;

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    if (matrix[x, y])
                    {
                        // black
                        bmpBuffer[pos + 0] = 0;
                        bmpBuffer[pos + 1] = 0;
                        bmpBuffer[pos + 2] = 0;
                    }
                    else
                    {
                        // white
                        bmpBuffer[pos + 0] = 255;
                        bmpBuffer[pos + 1] = 255;
                        bmpBuffer[pos + 2] = 255;
                    }
                    
                   
                   pos += 3;
                }
            }



            byte[] bitmapBytes = GetImageData(bmpBuffer);

            System.IO.MemoryStream ms = new System.IO.MemoryStream(bitmapBytes);
            return System.Drawing.Image.FromStream(ms);
        }

        public static byte[] GetImageData(byte[] bmpBuffer)
        {
            int bmpBufferSize = bmpBuffer.Length;
            int image_width = 200;
            int image_height = 200;
            PixelFormat format = PixelFormat.Rgb24bpp;

            // BmpBufferSize : a pure length of raw bitmap data without the header.
            // the 54 value here is the length of bitmap header.
            byte[] bitmapBytes = new byte[bmpBufferSize + 54];

            // 0~2 "BM"
            bitmapBytes[0] = 0x42;
            bitmapBytes[1] = 0x4d;

            // 2~6 Size of the BMP file - Bit cound + Header 54
            System.Array.Copy(System.BitConverter.GetBytes(bmpBufferSize + 54), 0, bitmapBytes, 2, 4);

            // 6~8 Application Specific : normally, set zero
            System.Array.Copy(System.BitConverter.GetBytes(0), 0, bitmapBytes, 6, 2);

            // 8~10 Application Specific : normally, set zero
            System.Array.Copy(System.BitConverter.GetBytes(0), 0, bitmapBytes, 8, 2);

            // 10~14 Offset where the pixel array can be found - 24bit bitmap data always starts at 54 offset.
            System.Array.Copy(System.BitConverter.GetBytes(54), 0, bitmapBytes, 10, 4);



            // 14~18 Number of bytes in the DIB header. 40 bytes constant.
            System.Array.Copy(System.BitConverter.GetBytes(40), 0, bitmapBytes, 14, 4);

            // 18~22 Width of the bitmap.
            System.Array.Copy(System.BitConverter.GetBytes(image_width), 0, bitmapBytes, 18, 4);

            // 22~26 Height of the bitmap.
            System.Array.Copy(System.BitConverter.GetBytes(image_height), 0, bitmapBytes, 22, 4);

            // 26~28 Number of color planes being used
            System.Array.Copy(System.BitConverter.GetBytes(0), 0, bitmapBytes, 26, 2);

            // 28~30 Number of bits. If you don't know the pixel format, trying to calculate it with the quality of the video/image source.
            if (format == PixelFormat.Rgb24bpp)
            {
                System.Array.Copy(System.BitConverter.GetBytes(24), 0, bitmapBytes, 28, 2);
            }

            // 30~34 BI_RGB no pixel array compression used : most of the time, just set zero if it is raw data.
            System.Array.Copy(System.BitConverter.GetBytes(0), 0, bitmapBytes, 30, 4);

            // 34~38 Size of the raw bitmap data ( including padding )
            System.Array.Copy(System.BitConverter.GetBytes(bmpBufferSize), 0, bitmapBytes, 34, 4);

            // 38~46 Print resolution of the image, 72 DPI x 39.3701 inches per meter yields
            if (format == PixelFormat.Rgb24bpp)
            {
                System.Array.Copy(System.BitConverter.GetBytes(0), 0, bitmapBytes, 38, 4);
                System.Array.Copy(System.BitConverter.GetBytes(0), 0, bitmapBytes, 42, 4);
            }

            // 46~50 Number of colors in the palette
            System.Array.Copy(System.BitConverter.GetBytes(0), 0, bitmapBytes, 46, 4);

            // 50~54 means all colors are important
            System.Array.Copy(System.BitConverter.GetBytes(0), 0, bitmapBytes, 50, 4);

            // 54~end : Pixel Data : Finally, time to combine your raw data, BmpBuffer in this code, with a bitmap header you've just created.
            System.Array.Copy(bmpBuffer as System.Array, 0, bitmapBytes, 54, bmpBufferSize);

            return bitmapBytes;
        }


    }
}

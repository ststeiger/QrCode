
// Uses FluxJpeg from Brian Donahue to implement JPEG
// https://github.com/briandonahue/FluxJpeg.Core
namespace OxyPlot
{

    /// <summary>
    /// Implements support for encoding bmp images.
    /// </summary>
    public class JpegEncoder 
        : IImageEncoder
    {
        /// <summary>
        /// The options
        /// </summary>
        private readonly JpegEncoderOptions options;

        /// <summary>
        /// Initializes a new instance of the <see cref="BmpEncoder" /> class.
        /// </summary>
        /// <param name="options">The options.</param>
        public JpegEncoder(JpegEncoderOptions options)
        {
            this.options = options;
        }

        public JpegEncoder()
            : this(new JpegEncoderOptions())
        { }

        public byte[] Encode(OxyColor[,] pixels)
        {
            int jpgQuality = 100;
            int bands = 3;
            byte[][,] raster = new byte[bands][,];
            int width = pixels.GetLength(0);
            int height = pixels.GetLength(1);

            for (int i = 0; i < bands; i++)
            {
                raster[i] = new byte[width, height];
            }

            for (int row = 0; row < height; row++)
            {
                for (int column = 0; column < width; column++)
                {
                    raster[0][column, row] = pixels[column, row].R;
                    raster[1][column, row] = pixels[column, row].G;
                    raster[2][column, row] = pixels[column, row].B;
                }
            }

            byte[] retValue = null;

            using (System.IO.MemoryStream stream = new System.IO.MemoryStream())
            {
                FluxJpeg.Core.ColorModel model = new FluxJpeg.Core.ColorModel { colorspace = FluxJpeg.Core.ColorSpace.RGB };
                FluxJpeg.Core.Image img = new FluxJpeg.Core.Image(model, raster);
                FluxJpeg.Core.Encoder.JpegEncoder encoder = new FluxJpeg.Core.Encoder.JpegEncoder(img, jpgQuality, stream);

                encoder.Encode();
                stream.Flush();
                stream.Seek(0, System.IO.SeekOrigin.Begin);

                retValue = stream.ToArray();
            } // End Using stream 

            return retValue;
        }


        public byte[] Encode(byte[,] pixels, OxyColor[] palette)
        {
            int jpgQuality = 100;
            int bands = 3;
            byte[][,] raster = new byte[bands][,];

            // Is this correct ??? 
            int width = pixels.GetLength(0)/3;
            int height = pixels.GetLength(1);

            for (int i = 0; i < bands; i++)
            {
                raster[i] = new byte[width, height];
            }

            for (int row = 0; row < height; row++)
            {
                for (int column = 0; column < width; column++)
                {
                    raster[0][column, row] = pixels[column, row*3+0];
                    raster[1][column, row] = pixels[column, row*3+1];
                    raster[2][column, row] = pixels[column, row*3+2];

                    //int pixel = bitmap.Pixels[width * row + column];
                    //raster[0][column, row] = (byte)(pixel >> 16);
                    //raster[1][column, row] = (byte)(pixel >> 8);
                    //raster[2][column, row] = (byte)pixel;
                }
            }

            byte[] retValue = null;

            using (System.IO.MemoryStream stream = new System.IO.MemoryStream())
            {
                FluxJpeg.Core.ColorModel model = new FluxJpeg.Core.ColorModel { colorspace = FluxJpeg.Core.ColorSpace.RGB };
                FluxJpeg.Core.Image img = new FluxJpeg.Core.Image(model, raster);
                FluxJpeg.Core.Encoder.JpegEncoder encoder = new FluxJpeg.Core.Encoder.JpegEncoder(img, jpgQuality, stream);

                encoder.Encode();
                stream.Flush();
                stream.Seek(0, System.IO.SeekOrigin.Begin);

                retValue = stream.ToArray();
            } // End Using stream 

            return retValue;
        }
    }

}
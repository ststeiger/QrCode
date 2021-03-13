
using System.Windows.Forms;


namespace TestQrCode
{


    public partial class Form1
        : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        /// <summary>
        /// Renders the bitmatrix.
        /// </summary>
        private static System.Drawing.Image RenderMatrix(
            libQrCode.Common.BitMatrix matrix)
        {
            int width = matrix.Width;
            int height = matrix.Height;

            System.Drawing.Color black = System.Drawing.Color.Black;
            System.Drawing.Color white = System.Drawing.Color.White;

            System.Drawing.Bitmap img = new System.Drawing.Bitmap(width, height);
            using (System.Drawing.Graphics context = System.Drawing.Graphics.FromImage(img))
            {
                for (int x = 0; x < width; x++)
                {
                    for (int y = 0; y < height; y++)
                    {
                        using (System.Drawing.Brush brush = new System.Drawing.SolidBrush(matrix[x, y] ? black : white))
                        {
                            context.FillRectangle(brush, new System.Drawing.Rectangle(x, y, 1, 1));
                        }

                    }
                }
            }

            return img;
        }


        private void Form1_Load(object sender, System.EventArgs e)
        {
            libQrCode.QrCode.QRCodeWriter qw = new libQrCode.QrCode.QRCodeWriter();
            // libQrCode.Common.BitMatrix bm = qw.Encode("https://cordemo.cor-asp.ch/FM_COR_Demo_V4", libQrCode.BarcodeFormat.QR_CODE, 200, 200);
            // this.pbZXING.Image = SimpleBitmapHandler.GetImage(bm);
            
            byte[] imageBytes = qw.RenderImageBytes("https://cordemo.cor-asp.ch/FM_COR_Demo_V4", libQrCode.BarcodeFormat.QR_CODE, 200, 200);
            System.IO.MemoryStream ms = new System.IO.MemoryStream(imageBytes);
            this.pbZXING.Image = System.Drawing.Image.FromStream(ms);
        }


    }
}

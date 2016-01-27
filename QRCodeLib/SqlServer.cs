


namespace ThoughtWorks.QRCode
{


    public class SqlServer
    {


        // http://stackoverflow.com/questions/4213788/how-to-create-clr-stored-procedure-with-nvarcharmax-parameter
        // Warning: QR-Code has upper limit in number of characters accepted
        public static byte[] GenerateQrCode(string data)
        {
            byte[] ba = null;
            ThoughtWorks.QRCode.Codec.QRCodeEncoder QRcodeInstance = new ThoughtWorks.QRCode.Codec.QRCodeEncoder();
            QRcodeInstance.QRCodeEncodeMode = ThoughtWorks.QRCode.Codec.QRCodeEncoder.ENCODE_MODE.BYTE;
            QRcodeInstance.QRCodeErrorCorrect = ThoughtWorks.QRCode.Codec.QRCodeEncoder.ERROR_CORRECTION.H;
            QRcodeInstance.QRCodeScale = 2;
            //QRcodeInstance.QRCodeVersion = 7;
            QRcodeInstance.QRCodeVersion = 20;


            using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
            {
                using (System.Drawing.Image img = QRcodeInstance.Encode(data))
                {
                    img.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                }

                ba = ms.ToArray();
            }

            return ba;
        }


        // http://stackoverflow.com/questions/840552/clr-udf-returning-varbinarymax
        [Microsoft.SqlServer.Server.SqlFunction]
        public static System.Data.SqlTypes.SqlBytes GenerateQR(System.Data.SqlTypes.SqlString qrCodeText)
        {
            if (qrCodeText.IsNull)
                return null;

            // return new System.Data.SqlTypes.SqlBytes(System.Text.Encoding.UTF8.GetBytes(qrCodeText.Value));
            return new System.Data.SqlTypes.SqlBytes(GenerateQrCode(qrCodeText.Value));
        }


    }


}

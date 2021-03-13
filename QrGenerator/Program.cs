
namespace QrGenerator
{


    static class Program
    {


        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        [System.STAThread]
        static void Main()
        {
            if (false)
            {
                System.Windows.Forms.Application.EnableVisualStyles();
                System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(false);
                System.Windows.Forms.Application.Run(new Form1());
            }

#if true 

            string dir = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            dir = System.IO.Path.Combine(dir, "../../..");
            dir = System.IO.Path.Combine(dir, "QRCodeLib/bin/Release");
            dir = System.IO.Path.Combine(dir, "ThoughtWorks.QRCode.dll");
            dir = System.IO.Path.GetFullPath(dir);

            byte[] ba = System.IO.File.ReadAllBytes(dir);
            //System.Runtime.Remoting.Metadata.W3cXsd2001.SoapHexBinary shb = new System.Runtime.Remoting.Metadata.W3cXsd2001.SoapHexBinary(ba);
            //string soapHex = shb.ToString();
            string hexString = ByteArrayToHexViaByteManipulation2(ba);
            // System.Console.WriteLine(hexString);
            //System.Console.WriteLine(soapHex);

            System.Console.WriteLine(dir);


            System.Reflection.Assembly ass = typeof(System.Drawing.Color).Assembly;
            System.Console.WriteLine(ass.Location);


            string assemblyName = "foobar";

            string sql = "CREATE ASSEMBLY [" + assemblyName + "] FROM " + hexString +
                         " WITH PERMISSION_SET = " + "UNSAFE";


            // SELECT * FROM sys.dm_clr_properties
            // http://blogs.msdn.com/b/dohollan/archive/2012/04/20/sql-server-2012-sqlclr-net-framework-version.aspx
            // http://how-i-fixed-it.blogspot.ch/2013/01/use-systemdrawingdll-from-sqlclr-c-code.html
            // http://www.codeproject.com/Tips/791953/SQL-CLR-functions


            System.IO.File.WriteAllText(@"D:\stefan.steiger\Downloads\myfile.sql", sql, System.Text.Encoding.UTF8);

#endif

            System.Console.WriteLine(System.Environment.NewLine);
            System.Console.WriteLine(" --- Press any key to continue --- ");
            System.Console.ReadKey();
        }


        // Benchmark: 
        // http://stackoverflow.com/questions/311165/how-do-you-convert-byte-array-to-hexadecimal-string-and-vice-versa
        private static string ByteArrayToHexViaByteManipulation2(byte[] bytes)
        {
            char[] c = new char[bytes.Length * 2 + 2];
            int b;

            c[0] = '0';
            c[1] = 'x';

            for (int i = 0; i < bytes.Length; i++)
            {
                b = bytes[i] >> 4;
                c[2 + i * 2] = (char)(55 + b + (((b - 10) >> 31) & -7));
                b = bytes[i] & 0xF;
                c[2 + i * 2 + 1] = (char)(55 + b + (((b - 10) >> 31) & -7));
            }

            return new string(c);
        }


    }


}


// Alternative
// http://beyondrelational.com/modules/2/blogs/65/posts/11604/generating-qr-codes-in-ssrs.aspx
// http://qrcode.kaywa.com/img.php?s=8&d=noobishNoob

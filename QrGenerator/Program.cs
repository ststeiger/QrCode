using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace QrGenerator
{
    static class Program
    {
        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        [STAThread]
        static void Main()
        {
            if (false)
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Form1());
            }

            string dir = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            dir = System.IO.Path.Combine(dir, "../../..");
            dir = System.IO.Path.Combine(dir, "QRCodeLib/bin/Release");
            dir = System.IO.Path.Combine(dir, "ThoughtWorks.QRCode.dll");
            dir = System.IO.Path.GetFullPath(dir);

            byte[] ba = System.IO.File.ReadAllBytes(dir);
            //System.Runtime.Remoting.Metadata.W3cXsd2001.SoapHexBinary x = new System.Runtime.Remoting.Metadata.W3cXsd2001.SoapHexBinary(ba);
            //string lalalala = x.ToString();
            string hexString = ByteArrayToHexViaByteManipulation2(ba);
            // System.Console.WriteLine(hexString);
            //System.Console.WriteLine(lalalala);

            System.Console.WriteLine(dir);


            System.Reflection.Assembly ass = typeof(System.Drawing.Color).Assembly;
            System.Console.WriteLine(ass.Location);


            string assemblyName = "foobar";

            string sql = "CREATE ASSEMBLY [" + assemblyName + "] FROM " + hexString +
                         " WITH PERMISSION_SET = " + "UNSAFE";


            System.IO.File.WriteAllText(@"D:\stefan.steiger\Downloads\myfile.sql", sql, System.Text.Encoding.UTF8);

            System.Console.WriteLine(System.Environment.NewLine);
            System.Console.WriteLine(" --- Press any key to continue --- ");
            System.Console.ReadKey();
        }




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

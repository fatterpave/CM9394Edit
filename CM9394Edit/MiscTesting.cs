using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CM9394Edit
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Windows.Forms;
    using System.IO;

    
    public class MiscTesting
    {
        public MiscTesting()
        {
            Go4();
        }

        public void Go4()
        {
            BinaryReader br = new BinaryReader(File.OpenRead(@"E:\cm\compare\CMENGV2.EXE"));

            StreamWriter sw = new StreamWriter(@"E:\cm\compare\lastnames.txt");
            string hex = null;
            int counter = 0;

            for (int i = 0x73A0; i <= 0xCB9F; i++)
            {
                br.BaseStream.Position = i;
                hex += br.ReadByte().ToString("X2");
                counter++;

                if (counter == 16)
                {
                    StringBuilder sb = new StringBuilder();
                    for (int j = 0; j <= hex.Length - 2; j += 2)
                    {
                        sb.Append(Convert.ToString(Convert.ToChar(Int32.Parse(hex.Substring(j, 2), System.Globalization.NumberStyles.HexNumber))));
                    }
                    sw.WriteLine(sb.ToString());
                    hex = "";
                    counter = 0;
                }

            }
            sw.Close();
        }

        public void Go()
        {
            BinaryReader br = new BinaryReader(File.OpenRead(@"C:\dill\Progs\Championship Manager 93-94 EOS\piss\SVGAMEG4"));
            string currentSkills = null;
            int counter = 0;
            for (int i = 0x6812; i <= 0x6F55; i++)
            {
                counter++;
                br.BaseStream.Position = i;
                string hexValue = br.ReadByte().ToString("X2");
                int decAgain = int.Parse(hexValue, System.Globalization.NumberStyles.HexNumber);
                currentSkills += "Player " + counter + " current skill: " + hexValue + "(" + decAgain + ")\r\n";

            }
            br.Close();

            //textBox1.Text = currentSkills;

            BinaryWriter bw = new BinaryWriter(File.OpenWrite(@"C:\dill\Progs\Championship Manager 93-94 EOS\piss\SVGAMEG4"));
            for (int i = 0x6812; i <= 0x6F55; i++)
            {
                bw.BaseStream.Position = i;
                bw.Write(0xC8);
            }
            bw.Close();
        }

        public void Go3()
        {
            BinaryReader br = new BinaryReader(File.OpenRead(@"E:\cm\compare\CMENGV2.EXE"));
            
            StreamWriter sw = new StreamWriter(@"E:\cm\compare\firstnames.txt");
            string hex = null;
            int counter = 0;

            for (int i = 0x5760; i <= 0x739F; i++)
            {
                br.BaseStream.Position = i;
                hex += br.ReadByte().ToString("X2");
                counter++;

                if (counter == 16)
                {
                    StringBuilder sb = new StringBuilder();
                    for (int j = 0; j <= hex.Length - 2; j += 2)
                    {
                        sb.Append(Convert.ToString(Convert.ToChar(Int32.Parse(hex.Substring(j, 2), System.Globalization.NumberStyles.HexNumber))));
                    }
                    sw.WriteLine(sb.ToString());
                    hex = "";
                    counter = 0;
                }

            }
            sw.Close();
        }

        public void Go5()
        {
            BinaryReader br = new BinaryReader(File.OpenRead(@"C:\dill\Progs\Championship Manager 93-94 EOS\piss\SVGAMEG4"));
            string currentSkills = null;
            int counter = 0;
            for (int i = 0x6812; i <= 0x6F55; i++)
            {
                counter++;
                br.BaseStream.Position = i;
                string hexValue = br.ReadByte().ToString("X2");
                int decAgain = int.Parse(hexValue, System.Globalization.NumberStyles.HexNumber);
                currentSkills += "Player " + counter + " current skill: " + hexValue + "(" + decAgain + ")\r\n";

            }
            br.Close();

            //textBox1.Text = currentSkills;

            BinaryWriter bw = new BinaryWriter(File.OpenWrite(@"C:\dill\Progs\Championship Manager 93-94 EOS\piss\SVGAMEG4"));
            for (int i = 0x6812; i <= 0x6F55; i++)
            {
                bw.BaseStream.Position = i;
                bw.Write(0xC8);
            }
            bw.Close();
        }

        public void Go2()
        {
            StreamWriter sw = new StreamWriter(@"C:\dill\Progs\Championship Manager 93-94 EOS\piss\teams.txt");
            BinaryReader br = new BinaryReader(File.OpenRead(@"C:\dill\Progs\Championship Manager 93-94 EOS\piss\CMENGV2.EXE"));
            string hex = null;
            int counter = 0;

            for (int i = 0x4E90; i <= 0x575F; i++)
            {
                br.BaseStream.Position = i;
                hex += br.ReadByte().ToString("X2");
                counter++;

                if (counter == 16)
                {

                    StringBuilder sb = new StringBuilder();
                    for (int j = 0; j <= hex.Length - 2; j += 2)
                    {
                        sb.Append(Convert.ToString(Convert.ToChar(Int32.Parse(hex.Substring(j, 2), System.Globalization.NumberStyles.HexNumber))));
                    }
                    sw.WriteLine(sb.ToString());
                    hex = "";
                    counter = 0;
                }

            }

            br.Close();
            sw.Close();
        }
    }
    
 
}

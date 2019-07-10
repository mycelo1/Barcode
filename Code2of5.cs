using System;
using System.Collections.Generic;
using System.Text;

namespace Barcode
{
    public static class Code2of5
    {
        // n = barra estreita
        // N = barra larga
        // w = espaço estreito
        // W = espaço largo

        private static Dictionary<byte, byte[]> codec = new Dictionary<byte, byte[]>()
            {
                {0, new byte[] {0, 0, 1, 1, 0}},
                {1, new byte[] {1, 0, 0, 0, 1}},
                {2, new byte[] {0, 1, 0, 0, 1}},
                {3, new byte[] {1, 1, 0, 0, 0}},
                {4, new byte[] {0, 0, 1, 0, 1}},
                {5, new byte[] {1, 0, 1, 0, 0}},
                {6, new byte[] {0, 1, 1, 0, 0}},
                {7, new byte[] {0, 0, 0, 1, 1}},
                {8, new byte[] {1, 0, 0, 1, 0}},
                {9, new byte[] {0, 1, 0, 1, 0}}
            };

        // montar string com representação do código de barras
        public static string Encode(string digits)
        {
            StringBuilder result = new StringBuilder("nwnw");
            StringBuilder normal = new StringBuilder(digits);

            if ((normal.Length % 2) != 0)
            {
                normal.Insert(0, '0');
            }

            byte bar_digit = default(byte);
            bool even = false;
            foreach (char digit in normal.ToString())
            {
                if (!even)
                {
                    bar_digit = (byte)(Convert.ToByte(digit) - 48);
                }
                else
                {
                    byte space_digit = (byte)(Convert.ToByte(digit) - 48);
                    for (int i = 0; i <= 4; i++)
                    {
                        result.Append(codec[bar_digit][i] == 1 ? 'N' : 'n');
                        result.Append(codec[space_digit][i] == 1 ? 'W' : 'w');
                    }
                }
                even = !even;
            }

            result.Append("Nwn");
            return result.ToString();
        }
    }
}

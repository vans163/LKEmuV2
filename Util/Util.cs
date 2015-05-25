using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Globalization;

namespace LKCamelotV2
{
    public static class Util
    {
        public static int ToInt32(string value)
        {
            int i;

            if (value.StartsWith("0x"))
                int.TryParse(value.Substring(2), NumberStyles.HexNumber, null, out i);
            else
                int.TryParse(value, out i);

            return i;
        }

        public static Point2D AdjecentTile(World.Tile curtile, int swingloc, World.Map curmap)
        {
            if (swingloc == -1)
                swingloc = 7;
            if (swingloc == 8)
                swingloc = 0;

            if (swingloc == 0 && curtile.Y - 1 >= 0)
                return new Point2D(curtile.X, curtile.Y - 1);
            if (swingloc == 1 && curtile.X + 1 < curmap.sizeX && curtile.Y - 1 >= 0)
                return new Point2D(curtile.X + 1, curtile.Y - 1);
            if (swingloc == 2)
                return new Point2D(curtile.X + 1, curtile.Y);
            if (swingloc == 3)
                return new Point2D(curtile.X + 1, curtile.Y + 1);
            if (swingloc == 4)
                return new Point2D(curtile.X, curtile.Y + 1);
            if (swingloc == 5)
                return new Point2D(curtile.X - 1, curtile.Y + 1);
            if (swingloc == 6)
                return new Point2D(curtile.X - 1, curtile.Y);
            if (swingloc == 7)
                return new Point2D(curtile.X - 1, curtile.Y - 1);

            return null;
        }

        public static T DeepClone<T>(this T obj)
        {
            using (var ms = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(ms, obj);
                ms.Position = 0;

                return (T)formatter.Deserialize(ms);
            }
        }

        public static string ToHexString(byte[] data)
        {
            var dataLength = data.Count();
            string result = "";

            for (int i = 0; i < dataLength; i++)
            {
                result += data[i].ToString("X2");
            }
            return result;
        }

        public static byte[] HexStringToByte(string hexString)
        {
            byte[] HexAsBytes = new byte[hexString.Length / 2];
            for (int index = 0; index < HexAsBytes.Length; index++)
            {
                string byteValue = hexString.Substring(index * 2, 2);
                HexAsBytes[index] = byte.Parse(byteValue, System.Globalization.NumberStyles.HexNumber, System.Globalization.CultureInfo.InvariantCulture);
            }

            return HexAsBytes;
        }

        public static short GetAngle(int myx, int myy, int x2, int y2)
        {
            var angle = Math.Atan2(y2 - myy, x2 - myx);
            var test = (angle > 0 ? angle : (2 * Math.PI + angle)) * 360 / (2 * Math.PI);

            if (test == 90)
                return 4;
            if (test > 90 && test < 180)
                return 5;
            if (test == 180)
                return 6;
            if (test > 180 && test < 270)
                return 7;
            if (test == 270)
                return 0;
            if (test > 270 && test < 360)
                return 1;
            if (test == 360)
                return 2;
            if (test > 0 && test < 90)
                return 3;

            return 0;
        }
    }
}

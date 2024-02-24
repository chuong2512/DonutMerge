using System;
using System.Collections.Generic;
using System.Globalization;
using System.Security.Cryptography;

namespace _Game.Scripts.Utils
{
    public static class Extension
    {
        public static void Shuffle<T>(this IList<T> list)
        {
            RNGCryptoServiceProvider provider = new RNGCryptoServiceProvider();
            int n = list.Count;
            while (n > 1)
            {
                byte[] box = new byte[1];
                do provider.GetBytes(box);
                while (!(box[0] < n * (Byte.MaxValue / n)));
                int k = (box[0] % n);
                n--;
                (list[k], list[n]) = (list[n], list[k]);
            }
        }

        public static string ConvertCurrency(this int amount, bool thousand = false)
        {
            if (amount >= 1000000000)
            {
                return (amount / 1000000000D).AsString("0.##") + "B";
            }

            if (amount >= 1000000)
            {
                return (amount / 1000000D).AsString("0.##") + "M";
            }

            if (amount >= 1000 && thousand)
            {
                return (amount / 1000D).AsString("0.##") + "k";
            }

            return amount.ToString();
        }

        public static string ChangeColor(this string text, string hexColor)
        {
            return $"<color=#{hexColor}>{text}</color>";
        }

        public static string AsString(this float number, string format = "F1")
        {
            return number.ToString(format, CultureInfo.InvariantCulture);
        }

        public static string AsString(this double number, string format = "F1")
        {
            return number.ToString(format, CultureInfo.InvariantCulture);
        }

        public static string Pad0Left(this int number, int count = 2)
        {
            return number.ToString().PadLeft(count, '0');
        }
    }
}
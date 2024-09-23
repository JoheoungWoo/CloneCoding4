using System;
using UnityEngine;

public static class Helper
{
    static readonly string[] CurrencyUnits = new string[] { "", "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z", "aa", "ab", "ac", "ad", "ae", "af", "ag", "ah", "ai", "aj", "ak", "al", "am", "an", "ao", "ap", "aq", "ar", "as", "at", "au", "av", "aw", "ax", "ay", "az", "ba", "bb", "bc", "bd", "be", "bf", "bg", "bh", "bi", "bj", "bk", "bl", "bm", "bn", "bo", "bp", "bq", "br", "bs", "bt", "bu", "bv", "bw", "bx", "by", "bz", "ca", "cb", "cc", "cd", "ce", "cf", "cg", "ch", "ci", "cj", "ck", "cl", "cm", "cn", "co", "cp", "cq", "cr", "cs", "ct", "cu", "cv", "cw", "cx" };


    public static Transform FindNameChild(this Transform myTransform, string childName)
    {
        for (int i = 0; i < myTransform.childCount; i++)
        {
            Transform child = myTransform.GetChild(i);
            if (child.name == childName)
            {
                return child;
            }

            Transform found = FindNameChild(child, childName);
            if (found != null)
            {
                return found;
            }
        }
        return null;
    }
    
    public static string ToCurrencyString(this double number)
    {

        if (number == 0) return "0";
        if (double.IsInfinity(number)) return "Infinity";


        string sign = number < 0 ? "-" : "";


        string[] parts = number.ToString("E").Split('E');
        if (parts.Length < 2) return "0";  // ???? ???


        int exponent = int.Parse(parts[1]);


        int unitIndex = exponent / 3;
        string unit = unitIndex < CurrencyUnits.Length ? CurrencyUnits[unitIndex] : "";


        double baseValue = double.Parse(parts[0]);
        string formattedNumber = (baseValue * Math.Pow(10, exponent % 3)).ToString("F2").TrimEnd('0').TrimEnd('.');


        return $"{sign}{formattedNumber}{unit}";
    }

}




using UnityEngine;
using UnityEditor;

public class UtilMath 
{
    public static int GetDecimalPart(float value)
    {
        return (int)Mathf.Round((int)((int)(value) - value)*10);
    }

    public static int Mod(int  value, int n)
    {
        return value & n;
    }
}
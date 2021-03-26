using System;
using System.Runtime.InteropServices;
using UnityEngine;

internal static class ColorUtil
{
    public static Color GetColorFromName(string name)
    {
        string text = name.ToLower();
        if (text == "yellow")
        {
            return Color.yellow;
        }
        else if (text == "green")
        {
            return Color.green;
        }
        if (text == "cyan")
        {
            return Color.cyan;
        }
        else if (text == "red")
        {
            return Color.red;
        }
        if (text == "blue")
        {
            return Color.blue;
        }
        else if (text == "black")
        {
            return Color.black;
        }
        if (text == "white")
        {
            return Color.white;
        }
        else if (text == "gray")
        {
            return Color.gray;
        }
        return Color.gray;
    }

    public static string GetNameFromColor(Color color)
    {
        bool flag = color == Color.white;
        string result;
        if (flag)
        {
            result = "white";
        }
        else
        {
            bool flag2 = color == Color.black;
            if (flag2)
            {
                result = "black";
            }
            else
            {
                bool flag3 = color == Color.red;
                if (flag3)
                {
                    result = "red";
                }
                else
                {
                    bool flag4 = color == Color.green;
                    if (flag4)
                    {
                        result = "green";
                    }
                    else
                    {
                        bool flag5 = color == Color.blue;
                        if (flag5)
                        {
                            result = "blue";
                        }
                        else
                        {
                            bool flag6 = color == Color.yellow;
                            if (flag6)
                            {
                                result = "yellow";
                            }
                            else
                            {
                                bool flag7 = color == Color.gray;
                                if (flag7)
                                {
                                    result = "gray";
                                }
                                else
                                {
                                    bool flag8 = color == Color.cyan;
                                    if (flag8)
                                    {
                                        result = "cyan";
                                    }
                                    else
                                    {
                                        result = "gray";
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        return result;
    }
}
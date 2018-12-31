using UnityEngine;

public class Utils
{

    public static Color ParseColor(string color)
    {
        Color newColor = Color.clear;
        ColorUtility.TryParseHtmlString(color, out newColor);
        return newColor;
    }

}

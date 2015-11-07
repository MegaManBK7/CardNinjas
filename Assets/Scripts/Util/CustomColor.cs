using UnityEngine;

namespace Assets.Scripts.Util
{
    public static class CustomColor
    {
        private const float MAX_RGBVal = 255.0f;

        public static Color Convert255(float r, float g, float b)
        {
            r = Mathf.Clamp(r, 0f, MAX_RGBVal);
            g = Mathf.Clamp(g, 0f, MAX_RGBVal);
            b = Mathf.Clamp(b, 0f, MAX_RGBVal);
            return new Color((r / MAX_RGBVal), (g / MAX_RGBVal), (b / MAX_RGBVal));
        }

        public static Color Convert255(float r, float g, float b, float a)
        {
            r = Mathf.Clamp(r, 0f, MAX_RGBVal);
            g = Mathf.Clamp(g, 0f, MAX_RGBVal);
            b = Mathf.Clamp(b, 0f, MAX_RGBVal);
            a = Mathf.Clamp(a, 0f, MAX_RGBVal);
            return new Color((r / MAX_RGBVal), (g / MAX_RGBVal), (b / MAX_RGBVal), (a / MAX_RGBVal));
        }
    }
}
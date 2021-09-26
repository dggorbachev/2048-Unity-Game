using UnityEngine;

public class ColorController : MonoBehaviour
{
    public static ColorController Pattern;
    public Color[] Colors;

    public Color ScoreDarkColor;
    public Color ScoreLightColor;
    private void Awake()
    {
        if (Pattern == null)
            Pattern = this;
    }
}
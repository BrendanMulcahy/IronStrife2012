using UnityEngine;

public class CloudFader : MonoBehaviour
{
    private CS_Cloud cloud;
    public Color color;
    public float baseVal = 167; // The color at the darkest time.
    public float baseAlpha = 127;

    public float maxVal = 255;
    public float maxAlpha = 255;

    private float currentVal;
    private float currentAlpha;

    void Awake() { cloud = GetComponent<CS_Cloud>(); }

    void Update()
    {
        var currentTime = GameTime.CurrentTime;
        // 0 is darkest, 12 is brightest.

        if (currentTime > 6 && currentTime < 9)
        {
            float currentPercentage = (currentTime - 6f) / 3f;
            currentVal = baseVal + (currentPercentage * (maxVal - baseVal));
            currentAlpha = baseAlpha + (currentPercentage * (maxAlpha - baseAlpha));
            color = new Color(currentVal / 255f, currentVal / 255f, currentVal / 255f, currentAlpha / 255f);
        }

        else if (currentTime > 18 && currentTime < 21)
        {
            float currentPercentage = (currentTime - 18f) / 3f;
            currentAlpha = maxAlpha - (currentPercentage * (maxAlpha - baseAlpha));
            color = new Color(currentVal / 255f, currentVal / 255f, currentVal / 255f, currentAlpha / 255f);

        }

        cloud.Tint = color;

    }

}
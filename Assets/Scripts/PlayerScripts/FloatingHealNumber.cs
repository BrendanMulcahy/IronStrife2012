using UnityEngine;

public class FloatingHealNumber : SimpleObjectLabel
{
    public int amount;
    float heightPerSecond = 1f;
       float elapsedTime = 0f;
    float maxTime = 3.0f;
    float fadePoint;


    protected override void Start()
    {
        base.Start();
        guiText.fontSize = 36;
        guiText.material.color = Color.green;
        Destroy(this.gameObject, maxTime);

        float percentage = ((float)amount / (float)transform.root.gameObject.GetCharacterStats().Health.MaxValue);
        guiText.text = amount.ToString();
        if (percentage > .33f)
        {
            guiText.fontSize = (int)(guiText.fontSize * 1.33f);
            guiText.fontStyle = FontStyle.Bold;
            guiText.material.color = Color.cyan;
        }

        offset += Vector3.forward * Random.Range(-.1f, .1f);
        offset += Vector3.right * Random.Range(-.1f, .1f);

        fadePoint = maxTime * .75f;
    }

    void Update()
    {
        offset += Vector3.up * (heightPerSecond * Time.deltaTime);
    }

    protected override void FadeTextColor()
    {
        base.FadeTextColor();
        elapsedTime += Time.deltaTime;
        if (elapsedTime >= fadePoint)
        {
            var percent = (elapsedTime - fadePoint) / (maxTime - fadePoint);
            var color = guiText.material.color;
            var prevAlpha = color.a;
            var alpha = prevAlpha - (prevAlpha) * percent;
            color.a = alpha;
            guiText.material.color = color;

        }
    }
}
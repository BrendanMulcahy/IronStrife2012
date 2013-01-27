using UnityEngine;

public class FloatingHealNumber : SimpleObjectLabel
{
    public int amount;
    float heightPerSecond = 1f;

    protected override void Start()
    {
        base.Start();
        guiText.fontSize = 36;
        guiText.material.color = Color.green;
        Destroy(this.gameObject, 3.0f);

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


    }

    void Update()
    {
        offset += Vector3.up * (heightPerSecond * Time.deltaTime);
    }
}
using UnityEngine;

public class FloatingDamageNumber : SimpleObjectLabel
{
    public Damage damage;
    float heightPerSecond = 1f;

    protected override void Start()
    {
        base.Start();
        Destroy(this.gameObject, 3.0f);
        if (damage != null)
        {
            float percentage = ((float)damage.amount / (float)transform.root.gameObject.GetCharacterStats().Health.MaxValue);
            guiText.text = damage.amount.ToString();
            if (percentage > .33f)
            {
                guiText.fontSize = (int)(guiText.fontSize * 2f);
                guiText.fontStyle = FontStyle.Italic;
                guiText.material.color = Color.red;
            }
        }
        offset += Vector3.forward * Random.Range(-.1f, .1f);
        offset += Vector3.right * Random.Range(-.1f, .1f);

        guiText.fontSize = 26;

    }

    void Update()
    {
        offset += Vector3.up * (heightPerSecond * Time.deltaTime);
    }
}
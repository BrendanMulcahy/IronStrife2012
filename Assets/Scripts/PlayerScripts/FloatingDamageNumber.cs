using UnityEngine;

public class FloatingDamageNumber : SimpleObjectLabel
{
    public Damage damage;
    float heightPerSecond = 1f;

    protected override void Start()
    {
        base.Start();
        Destroy(this.gameObject, 3.0f);

        guiText.text = damage.amount.ToString();
        offset += Vector3.forward * Random.Range(-.1f, .1f);
        offset += Vector3.right * Random.Range(-.1f, .1f);

        guiText.fontSize = 26;

    }

    void Update()
    {
        offset += Vector3.up * (heightPerSecond * Time.deltaTime);
    }
}
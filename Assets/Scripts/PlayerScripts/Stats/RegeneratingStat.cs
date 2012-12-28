using UnityEngine;

public abstract class RegeneratingStat : MonoBehaviour
{
    protected int _currentValue;
    protected int _maxValue;

    private bool statRegenerating;
    private float timeTilStatRegenerating;
    private float maxStatRegenTime = 5.0f;
    private int statRegenRate = 1;

    public abstract int CurrentValue { get; set; }
    public abstract int MaxValue { get; set; }

    protected event StatChangedEventHandler Changed;

    protected void OnChanged(GameObject sender, StatChangedEventArgs e)
    {
        if (Changed != null)
            Changed(sender, e);
    }

    private void Awake()
    {
        StartCoroutine(Regenerate());
        if (Network.isServer)
        {

        }
    }

    protected System.Collections.IEnumerator Regenerate()
    {
        if (statRegenerating)
        {
            CurrentValue = Mathf.Min(MaxValue, CurrentValue + statRegenRate);
        }
        else
        {
            timeTilStatRegenerating -= .25f;
            statRegenerating = (timeTilStatRegenerating <= 0);
        }
        yield return new WaitForSeconds(.25f);
    }
}
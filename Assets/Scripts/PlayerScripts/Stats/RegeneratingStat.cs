using UnityEngine;

public abstract class RegeneratingStat : MonoBehaviour
{
    protected int _currentValue;
    protected int _maxValue;

    protected bool statRegenerating;
    protected float timeTilStatRegenerating;
    protected float maxStatRegenTime = 5.0f;
    public int statRegenRate = 1;

    public int CurrentValue
    {
        get { return _currentValue; }
        set
        {
            var temp = _currentValue;
            OnChanged(this.gameObject, new StatChangedEventArgs() { newValue = value, oldValue = temp });
            _currentValue = value;
        }
    }
    public int MaxValue { get { return _maxValue; } set { _maxValue = value; } }

    public event StatChangedEventHandler Changed;

    protected void OnChanged(GameObject sender, StatChangedEventArgs e)
    {
        if (Changed != null)
            Changed(sender, e);
    }

    protected void Awake()
    {
        StartCoroutine(Regenerate());
        if (Network.isServer)
        {
            StartCoroutine(Monitor());
        }
    }

    protected abstract System.Collections.IEnumerator Monitor();

    protected System.Collections.IEnumerator Regenerate()
    {
        while (true)
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
}
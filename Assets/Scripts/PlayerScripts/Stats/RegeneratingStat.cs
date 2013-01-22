using UnityEngine;

/// <summary>
/// Base class for regenerating stats (Health, Mana, Stamina)
/// </summary>
[System.Serializable]
public abstract class RegeneratingStat : MonoBehaviour
{
    protected int _currentValue;
    protected int _maxValue;

    protected bool statRegenerating;
    protected float timeTilStatRegenerating;
    protected float maxStatRegenTime = 5.0f;
    public int statRegenRate = 1;

    /// <summary>
    /// Returns the current value of this Stat
    /// </summary>
    public int CurrentValue
    {
        get { return _currentValue; }
        set
        {
            var temp = _currentValue;
            var e = new StatChangedEventArgs() { newValue = value, oldValue = temp };
            OnChanged(this.gameObject, e);
            if (e.newValue < e.oldValue)
            {
                StopRegeneration();
            }
            _currentValue = e.newValue;
        }
    }

    /// <summary>
    /// Returns the current percentage of this Stat (current / max)
    /// </summary>
    public float CurrentPercentage { get { return ((float)_currentValue / (float)_maxValue); } } 

    private void StopRegeneration()
    {
        statRegenerating = false;
        timeTilStatRegenerating = maxStatRegenTime;
    }

    /// <summary>
    /// Returns the max value of this stat
    /// </summary>
    public int MaxValue { get { return _maxValue; } set { _maxValue = value; } }

    /// <summary>
    /// Event that is fired when this stat's current value is changed.
    /// </summary>
    public event StatChangedEventHandler Changed;

    public void SetInitialValues(int current, int max)
    {
        this._currentValue = current;
        this._maxValue = max;
    }

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
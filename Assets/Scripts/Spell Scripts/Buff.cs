using UnityEngine;

public abstract class Buff : MonoBehaviour
{
    /// <summary>
    /// The duration of the buff, after which its effects wear off
    /// </summary>
    protected abstract float duration { get; }
    /// <summary>
    /// The gameObject responsible for this buff's creation.
    /// </summary>
    protected GameObject source { get { return _source; } }
    private GameObject _source;

    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        if (DuplicateBuffAllowed())
        {
            if (this.gameObject.GetComponents(this.GetType()).Length > 1)
            {
                Destroy(this); return;
            }
        }

        AddBuffEffects();
        Invoke("RemoveBuffEffects", duration);
        Destroy(this, duration + .01f);
    }

    public void SetSource(GameObject source)
    {
        this._source = source;
    }

    protected virtual bool DuplicateBuffAllowed() { return false; }

    protected abstract void AddBuffEffects();

    protected abstract void RemoveBuffEffects();
}
public enum DamageType
{
    /// <summary>
    /// Reduced by physical resistance
    /// </summary>
    Physical,

    /// <summary>
    /// Reduced by magical resistance
    /// </summary>
    Magical,

    /// <summary>
    /// Reduced by both physical and magical resistance
    /// </summary>
    Composite,

    /// <summary>
    /// Not reduced by any resistance.
    /// </summary>
    Pure
}
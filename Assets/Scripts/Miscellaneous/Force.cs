using UnityEngine;

/// <summary>
/// Represents a force applied to a character
/// </summary>
public class Force
{
    public Vector3 vector;
    private Vector3 maxVector;
    public float duration;
    public float remainingTime;

    /// <summary>
    /// Creates a new force.
    /// </summary>
    /// <param name="vector">The direction and magnitude of the force</param>
    /// <param name="duration">The duration of the force on the player</param>
    public Force(Vector3 vector, float duration)
    {
        this.vector = vector;
        this.duration = duration;
        this.remainingTime = duration;
        this.maxVector = vector;
    }

    internal void DecayForce()
    {
        remainingTime -= Time.deltaTime;
        vector = maxVector * remainingTime / duration;

    }
}
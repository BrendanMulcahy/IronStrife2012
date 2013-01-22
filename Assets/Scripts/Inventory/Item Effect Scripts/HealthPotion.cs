using UnityEngine;

/// <summary>
/// This effect heals the user for some amount.
/// </summary>
public class HealthPotion : MonoBehaviour
{
    GameObject particle;
    void Start()
    {
        gameObject.GetCharacterStats().Health.CurrentValue += 30;

        particle = GameObject.Instantiate(Resources.Load("Particles/SparkleRising") as GameObject) as GameObject;
        particle.transform.SetParentAndCenter(this.transform.root);
        particle.transform.localPosition += Vector3.up * 1.7f;
        Invoke("TurnOffParticles", 1.1f);
        Destroy(particle, 5.0f);
        Destroy(this, 5.0f);
    }

    void TurnOffParticles()
    {
        foreach (ParticleEmitter pe in particle.GetComponentsInChildren<ParticleEmitter>())
        {
            pe.emit = false;
        }
    }
}
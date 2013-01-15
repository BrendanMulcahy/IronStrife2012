using UnityEngine;

public class ManaPotion : MonoBehaviour
{
    GameObject particle;
    void Start()
    {
        gameObject.GetCharacterStats().Mana.CurrentValue += 50;

        particle = GameObject.Instantiate(Resources.Load("Particles/SparkleRising") as GameObject) as GameObject;
        particle.transform.SetParentAndCenter(this.transform.root);
        particle.transform.localPosition += Vector3.up * 1.7f;
        Invoke("TurnOffParticles", 1.1f);
        StartCoroutine(Util.DestroyInSeconds(particle, 5.0f));
        StartCoroutine(Util.DestroyInSeconds(this, 5.0f));
    }

    void TurnOffParticles()
    {
        foreach (ParticleEmitter pe in particle.GetComponentsInChildren<ParticleEmitter>())
        {
            pe.emit = false;
        }
    }
}
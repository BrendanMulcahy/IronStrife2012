using UnityEngine;

[PlayerComponent(PlayerScriptType.AllEnabled)]
public class PlayerMotor : MonoBehaviour
{
    float mass = 3.0f; // defines the character mass
    Vector3 impact = Vector3.zero;
    CharacterController character;

    void Start()
    {
        character = GetComponent<CharacterController>();
        gameObject.GetCharacterStats().Damaged += PlayerMotor_Damaged;
    }

    void PlayerMotor_Damaged(GameObject sender, DamageEventArgs e)
    {
        var direction =  sender.transform.position - e.attacker.transform.position;
        float magnitude = e.damage.amount * 1.5f;
        AddImpact(direction, magnitude);
    }

    // call this function to add an impact force:
    void AddImpact(Vector3 dir, float force)
    {
        dir.Normalize();
        if (dir.y < 0) dir.y = -dir.y; // reflect down force on the ground
        impact += dir.normalized * force / mass;
    }

    void Update()
    {
        // apply the impact force:
        if (impact.magnitude > 0.2) character.Move(impact * Time.deltaTime);
        // consumes the impact energy each cycle:
        impact = Vector3.Lerp(impact, Vector3.zero, 5 * Time.deltaTime);
    }
}
using UnityEngine;
using System.Collections;

public class PlayerMotor : MonoBehaviour {

    private Vector3 externalVelocity;

    private ArrayList forces = new ArrayList();

	// Update is called once per frame
	void Update () 
    {
        externalVelocity = Vector3.zero;
        if (Input.GetKey(KeyCode.F7))
        {
            ApplyForce(new Force(Vector3.left , 2));
        }

        for (int g = 0; g < forces.Count; g++)
        {
            var force = forces[g] as Force;
            externalVelocity += force.vector * Time.deltaTime ;
            force.DecayForce();
            if (force.remainingTime <= 0)
            {
                forces.RemoveAt(g);
                g--;
            }
        }

        var characterController = GetComponent<CharacterController>();
        if (externalVelocity.magnitude!=0)
        characterController.Move(externalVelocity);

	}

    void DidLand()
    {
        externalVelocity.y = 0;
    }

    public void ApplyForce(Force force)
    {
        forces.Add(force);
    }
}

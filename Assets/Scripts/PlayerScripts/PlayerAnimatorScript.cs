using UnityEngine;

public class PlayerAnimatorScript : MonoBehaviour
{
    private Animator anim;
    private PlayerInputManager inputManager;
    private ThirdPersonController controller;

    void Start()
    {
        anim = GetComponent<Animator>();
        inputManager = GetComponent<PlayerInputManager>();
        controller = GetComponent<ThirdPersonController>();
    }

    void Update()
    {
        anim.SetFloat("horizontalInput", inputManager.horizontalInput);
        anim.SetFloat("verticalInput", inputManager.verticalInput);
        anim.SetBool("jumpButton", inputManager.jumpButton);
        if (inputManager.verticalInput == 0 && inputManager.horizontalInput == 0)
            anim.SetBool("idleInput", true);
        else
            anim.SetBool("idleInput", false);

        anim.SetBool("sprintButton", inputManager.sprintButton);

        if (controller.IsGrounded())
            anim.SetBool("grounded", true);
        else
            anim.SetBool("grounded", false);

    }
}
using UnityEngine;
using System.Collections;
using System;
using System.Linq;

public class ThirdPersonSimpleAnimation : MonoBehaviour
{
    Transform torso;
    bool isRunning = false;
    bool isWalking = false;

    bool firstJumpFrame = false;
    bool firstLandFrame = false;
    bool firstBowFrame = false;
    bool lastBowFrame = false;
    bool stillAiming = false;

    int comboCounter = -1;
    int maxCombos = 4;

    public int numWeaponHands;

    public IController thisController;
    public IController ThisController
    {
        get
        {
            if (thisController != null) return thisController;
            else { thisController = GetComponent<ThirdPersonController>(); return thisController; }
        }
        set { thisController = value; }
    }

    private bool IsAttacking { get { return thisController.IsAttacking; } }

    private bool IsDefending { get { return thisController.IsDefending; } }

    private bool IsAiming { get { return thisController.IsAiming; } }

    private bool IsMovingBackwards { get { return thisController.IsMovingBackwards; } }

    private bool IsCasting { get { return thisController.IsCasting; } }

    public string animationPrefix = "idle";
    //Contains all animations currently loaded into the player.
    private string currentAttackAnimation = "";

    private void Awake()
    {
        InitializeAnimation(animation);
        thisController = GetComponent<ThirdPersonController>();
    }

    private void Start()
    {
        GetComponent<InventoryManager>().weaponChanged += ThirdPersonSimpleAnimation_weaponChanged;
        if (GetComponent<InventoryManager>().currentWeapon == null)
            numWeaponHands = 1;
        else
        {
            numWeaponHands = GetComponent<InventoryManager>().currentWeapon.numHands;
            if (GetComponent<InventoryManager>().currentWeapon.name.ToLower().Contains("bow"))
            {
                numWeaponHands = 1;
            }
        }

    }

    void ThirdPersonSimpleAnimation_weaponChanged(WeaponChangedEventArgs e)
    {
        numWeaponHands = e.newWeapon.numHands;
        if (GetComponent<InventoryManager>().currentWeapon.name.ToLower().Contains("bow"))
        {
            numWeaponHands = 1;
        }
    }

    public static void InitializeAnimation(Animation ani)
    {
        var clips = Resources.LoadAll("Animations/animationFull", typeof(AnimationClip)).Cast<AnimationClip>();

        foreach (var c in clips)
        {
            ani.AddClip(c, c.name);
        }

        foreach (AnimationState state in ani)
        {
            state.layer = -1;
        }
        ani["RunSwing"].layer = 3;
        ani["RunSwing"].normalizedSpeed = 5 / 4;
        ani["IdleSwing"].layer = 3;
        ani["IdleSwing"].normalizedSpeed = 5 / 4;
        ani["RunTwoHandedSwing"].layer = 3;
        ani["RunTwoHandedSwing"].normalizedSpeed = 5 / 4;

        ani["Combo1"].layer = 3;
        ani["Combo1"].normalizedSpeed = 5 / 4;
        ani["Combo2"].layer = 3;
        ani["Combo2"].normalizedSpeed = 5 / 4;
        ani["Combo3"].layer = 3;
        ani["Combo3"].normalizedSpeed = 5 / 4;
        ani["Combo4"].layer = 3;
        ani["Combo4"].normalizedSpeed = 5 / 4;

        //ani["TwoHandedCombo1"].layer = 3;
        //ani["TwoHandedCombo1"].normalizedSpeed = 5 / 4;
        //ani["TwoHandedCombo2"].layer = 3;
        //ani["TwoHandedCombo2"].normalizedSpeed = 5 / 4;
        //ani["TwoHandedCombo3"].layer = 3;
        //ani["TwoHandedCombo3"].normalizedSpeed = 5 / 4;

        ani["IdleTwoHandedSwing"].layer = 3;
        ani["IdleTwoHandedSwing"].normalizedSpeed = 6 / 4;

        ani["IdleConjure"].layer = 3;
        ani["IdleConjure"].wrapMode = WrapMode.Loop;

        ani["IdleBowAim"].layer = 3;
        ani["IdleBowAim"].wrapMode = WrapMode.Once; //Doesnt loop these animations

        ani["WalkBowAim"].layer = 3;
        ani["WalkBowAim"].wrapMode = WrapMode.Once;

        ani["IdleBowStartAim"].layer = 4;
        ani["IdleBowStartAim"].speed = 1.5f;
        ani["WalkBowStartAim"].layer = 4;
        ani["WalkBowStartAim"].speed = 1.5f;

        ani["IdleBowShootReload"].layer = 4;
        ani["IdleBowShootReload"].wrapMode = WrapMode.Once;
        ani["IdleBowShootReload"].speed = 1.5f;
        ani["WalkBowShootReload"].layer = 4;
        ani["WalkBowShootReload"].wrapMode = WrapMode.Once;
        ani["WalkBowShootReload"].speed = 1.5f;

        ani["IdleBowRelax"].layer = 1;
        ani["IdleBowRelax"].speed = 1.5f;
        ani["WalkBowRelax"].layer = 1;
        ani["WalkBowRelax"].speed = 1.5f;

        ani["RunJumpStart"].layer = 2;
        ani["RunJumpHover"].layer = 1;
        ani["RunJumpLand"].layer = 1;

        ani["Death"].layer = 20;
        ani["Death"].wrapMode = WrapMode.ClampForever;

        //ani["RunFlinch"].layer = 5;
        //ani["IdleShieldBlockFlinch"].layer = 5;


        ani["RunJumpHover"].speed = .5f;

        ani["Run"].clip.AddEvent(new AnimationEvent()
        {
            time = .175f,
            functionName = "PlayFootStep",
        });
        ani["Run"].clip.AddEvent(new AnimationEvent()
        {
            time = .525f,
            functionName = "PlayFootStep",
        });

        ani["Sprint"].clip.AddEvent(new AnimationEvent()
        {
            time = .125f,
            functionName = "PlayFootStep",
        });
        ani["Sprint"].clip.AddEvent(new AnimationEvent()
        {
            time = .375f,
            functionName = "PlayFootStep",
        });

        ani["RunSwing"].clip.AddEvent(new AnimationEvent()
        {
            time = .12f,
            functionName = "PlaySwordSwing",
        });
        ani["IdleSwing"].clip.AddEvent(new AnimationEvent()
        {
            time = .28f,
            functionName = "PlaySwordSwing",
        });
        ani["Combo1"].clip.AddEvent(new AnimationEvent()
        {
            time = .28f,
            functionName = "PlaySwordSwing",
        });
        ani["Combo2"].clip.AddEvent(new AnimationEvent()
        {
            time = .28f,
            functionName = "PlaySwordSwing",
        });
        ani["Combo3"].clip.AddEvent(new AnimationEvent()
        {
            time = .13f,
            functionName = "PlaySwordSwing",
        });
        ani["Combo4"].clip.AddEvent(new AnimationEvent()
        {
            time = .13f,
            functionName = "PlaySwordSwing",
        });

        ani["RunTwoHandedSwing"].clip.AddEvent(new AnimationEvent()
        {
            time = .3f,
            functionName = "PlaySwordSwing",
        });
        ani["IdleTwoHandedSwing"].clip.AddEvent(new AnimationEvent()
        {
            time = 1.2f,
            functionName = "PlaySwordSwing",
        });

        ani.Stop();
        ani.Play("Idle01");
    }

    private void Update()
    {
        maxCombos = (numWeaponHands == 1) ? 4 : 3;

        var currentSpeed = ThisController.MoveSpeed;
        if (currentSpeed > ThisController.WalkSpeed + 3)
        {
            isRunning = true;
            isWalking = false;
            animationPrefix = "Run";
        }
        else if (currentSpeed > 0.1)
        {
            isWalking = true;
            isRunning = false;
            animationPrefix = "Run";
        }
        else
        {
            isWalking = false;
            isRunning = false;
            animationPrefix = "Idle";
        }
        if (!IsCasting)
            animation.Stop("IdleConjure");
        

        //Priority of Animations for player
        if (IsAttacking)
        {
            CrossFadeAndSync(currentAttackAnimation, .1f);
        }

        else if (IsCasting)
        {
            CrossFadeAndSync("IdleConjure", .5f);
        }

        else if (ThisController.IsJumping())
        {
            if (firstJumpFrame)
            {
                animation.Stop();

                CrossFadeAndSync("RunJumpStart", .03f);
                firstJumpFrame = false;
            }
            else
            {
                CrossFadeAndSync("RunJumpHover", .2f);
            }
        }
        else if (lastBowFrame)
        {
            if (!isWalking && !isRunning)
            {

                lastBowFrame = false;
                CrossFadeAndSync("IdleBowRelax", .01f);
            }
            else if (isWalking || isRunning)
            {

                lastBowFrame = false;
                CrossFadeAndSync("WalkBowRelax", .01f);
            }
        }

        else if (stillAiming)
        {

            if (!isWalking && !isRunning)
            {
                animation.Stop();
                stillAiming = false;
                CrossFadeAndSync("IdleBowShootReload", .03f);
            }
            else if (isWalking || isRunning)
            {
                animation.Stop();
                stillAiming = false;
                CrossFadeAndSync("WalkBowShootReload", .03f);
            }


        }

        else if (IsAiming)
        {
            if (!isWalking && !isRunning)
            {
                if (firstBowFrame)
                {
                    animation.Stop();
                    CrossFadeAndSync("IdleBowStartAim", .1f);
                    firstBowFrame = false;
                }
                else
                {
                    CrossFadeAndSync("IdleBowAim", .2f);
                }
            }

            else if (isWalking || isRunning)
            {
                if (firstBowFrame)
                {
                    animation.Stop();
                    CrossFadeAndSync("WalkBowStartAim", .1f);
                    firstBowFrame = false;
                }
                else
                {

                    CrossFadeAndSync("WalkBowAim", .2f);
                }
            }

        }

        else if (IsDefending)
        {
            CrossFadeAndSync("IdleShieldBlock", .3f);
        }

        // Fade in Run
        else if (isRunning)
        {
            CrossFadeAndSync("Sprint", .1f);
        }
        // Fade in Walk
        else if (isWalking)
        {
            if (numWeaponHands < 2)
                CrossFadeAndSync("Run", .1f);
            else
                CrossFadeAndSync("RunTwoHanded", .1f);

        }
        // Fade out Walk and Run
        else
        {
            if (numWeaponHands < 2)
                CrossFadeAndSync("Idle01", .2f);
            else
                CrossFadeAndSync("IdleTwoHanded", .2f);
        }

        if (firstLandFrame)
        {
            firstLandFrame = false;
            CrossFadeAndSync("RunJumpLand", .1f);
        }
        
    }

    private void CrossFadeAndSync(string anim, float fadeLength = 0.0f)
    {
        animation.CrossFade(anim, fadeLength);
        SendMessage("SyncAnimation", anim);
    }

    private void BeganJump()
    {
        firstJumpFrame = true;
    }

    private void DidLand()
    {
        firstLandFrame = true;
    }

    /// <summary>
    /// Beginning Animation for bow
    /// </summary>
    private void BeginAim()
    {
        firstBowFrame = true;
    }

    //Ending animation for bow
    private void EndAim()
    {
        lastBowFrame = true;
    }

    //If person aims again right after letting go of an arrow
    private void StillAiming()
    {
        stillAiming = true;
    }

    private void ApplyDamage()
    {
        if (IsDefending)
        {
            animation.Play("IdleShieldBlockFlinch");
        }
        else
        {
            animation.Play(animationPrefix + "Flinch");
        }
    }

    private void StartAttacking()
    {
        if (numWeaponHands == 2)
        {
            if (animationPrefix == "Idle")
            {
                comboCounter = (comboCounter + 1) % maxCombos;
                currentAttackAnimation = "TwoHandedCombo" + (comboCounter + 1);
            }
            else
            {
                currentAttackAnimation = animationPrefix + "Swing";

            }
        }
        else
        {
            if (animationPrefix == "Idle")
            {
                comboCounter = (comboCounter + 1) % maxCombos;
                currentAttackAnimation = "Combo" + (comboCounter + 1);
            }
            else
            {
                currentAttackAnimation = animationPrefix + "Swing";

            }
        }
    }

    [RPC]
    public void BeginDying()
    {
        CrossFadeAndSync("Death", .1f);
    }

    [RPC]
    public void StopDying()
    {
        animation.Stop("Death");
    }    
}
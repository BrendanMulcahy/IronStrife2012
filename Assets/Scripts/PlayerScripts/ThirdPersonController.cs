using UnityEngine;
using System.Collections;
using System.Linq;

[PlayerComponent(PlayerScriptType.ClientOwnerEnabled, PlayerScriptType.ServerEnabled)]
public class ThirdPersonController : MonoBehaviour, IController
{

    // The speed when walking
    float walkSpeed = 6;
    public float WalkSpeed
    {
        get { return walkSpeed; }
        set { walkSpeed = value; }
    }
    // after trotAfterSeconds of walking we trot with trotSpeed
    float trotSpeed = 8;
    // when pressing "Fire3" button (cmd) we start running
    float runSpeed = 10;
    float sprintCostPerSecond = 6f;
    float sprintStaminaThreshold = 10f;
    bool lastFrameSprinting = false;

    float inAirControlAcceleration = 3.0f;

    // How high do we jump when pressing jump and letting go immediately
    float jumpHeight = 2.0f;
    // We add extraJumpHeight meters on top when holding the button down longer while jumping
    float extraJumpHeight = 2.5f;
    // Cost of stamina to jump
    float jumpStaminaCost = 15f;

    // The gravity for the character
    float gravity = 20.0f;
    float speedSmoothing = 10.0f;
    float trotAfterSeconds = 0.5f;
    bool isAffectedByGravity = true;
    public void HaltGravity() { isAffectedByGravity = false; }
    public void ResumeGravity() { isAffectedByGravity = true; }

    //Attack timer for bow so we know what animation to play
    public float attacktimer = 0;

    bool canJump = true;
    private bool isAttacking = false;
    public bool IsAttacking { get { return isAttacking; } }

    private bool isDefending;
    public bool IsDefending { get { return isDefending; } }

    public bool isBowShooting;
    public bool IsAiming { get { return isBowShooting; } }

    private bool isCasting;
    public bool IsCasting { get { return isCasting; } }

    public CameraMode CameraMode { get { return inputManager.cameraMode; } set { regularCamera.CameraMode = value; } }

    private bool lockedOn = false;
    public bool LockedOn { get { return lockedOn; } set { lockedOn = value; } }

    private GameObject lockedTarget;
    public GameObject LockedTarget { get { return lockedTarget; } set { lockedTarget = value; } }

    private float jumpRepeatTime = 0.05f;
    private float jumpTimeout = 0.15f;
    private float groundedTimeout = 0.25f;

    // The current move direction in x-z
    public Vector3 moveDirection = Vector3.zero;

    public Vector3 MoveDirection
    {
        get { return moveDirection; }
    }
    // The current vertical speed
    private float verticalSpeed = 0.0f;

    public float VerticalSpeed
    {
        get { return verticalSpeed; }
    }

    // The current x-z move speed
    private float moveSpeed = 0.0f;

    public float MoveSpeed
    {
        get { return moveSpeed; }
    }

    // The last collision flags returned from controller.Move
    private CollisionFlags collisionFlags;

    public CollisionFlags CollisionFlags
    {
        get { return collisionFlags; }
        set { collisionFlags = value; }
    }

    // Are we jumping? (Initiated with jump button and not grounded yet)
    private bool jumping = false;
    private bool jumpingReachedApex = false;

    // Are we moving backwards (This locks the camera to not do a 180 degree spin)
    private bool movingBack = false;
    // Is the user pressing any keys?
    public bool isMoving = false;
    // When did the user start walking (Used for going into trot after a while)
    private float walkTimeStart = 0.0f;
    // Last time the jump button was clicked down
    private float lastJumpButtonTime = -10.0f;
    // Last time we performed a jump
    private float lastJumpTime = -1.0f;
    // the height we jumped from (Used to determine for how long to apply extra jump power after jumping.)
    private float lastJumpStartHeight = 0.0f;

    private Vector3 inAirVelocity = Vector3.zero;

    public Vector3 InAirVelocity
    {
        get { return inAirVelocity; }
    }

    private float lastGroundedTime = 0.0f;

    private RegularCamera regularCamera;
    private PlayerSoundGenerator playerSound;
    private Inventory inventory;
    private PlayerInputManager inputManager;
    private CharacterStats characterStats;

    public bool isLocallyControlledPlayer = false;
    private float arrowFireVelocity = 30.0f;
    public float maxLockRange = 35.0f;
    public float ArrowFireVelocity { get { return arrowFireVelocity; } }

    public float VerticalInput { get { return inputManager.verticalInput; } set { inputManager.verticalInput = value; } }

    public float HorizontalInput { get { return inputManager.horizontalInput; } set { inputManager.horizontalInput = value; } }

    public Vector3 ForwardDirection { get { return inputManager.forwardCameraDirection; } set { inputManager.forwardCameraDirection = value; } }

    public bool JumpButton { get { return inputManager.jumpButton; } set { inputManager.jumpButton = value; } }

    public bool SprintButton { get { return inputManager.sprintButton; } set { inputManager.sprintButton = value; } }

    public bool DefendButton { get { return inputManager.defendButton; } set { inputManager.defendButton = value; } }

    public bool AttackButton { get { return inputManager.attackButton; } set { inputManager.attackButton = value; } }

    public bool AimButton { get { return inputManager.aimButton; } set { inputManager.aimButton = value; } }

    public bool LockButton { get { return inputManager.lockButton; } }


    public WeaponType WeaponType { get { return inventory.CurrentWeaponType; } }

    private void Awake()
    {
        regularCamera = Camera.main.GetComponent<RegularCamera>();
        inputManager = GetComponent<PlayerInputManager>();
        inventory = gameObject.GetInventory();
        playerSound = GetComponent<PlayerSoundGenerator>();
        characterStats = gameObject.GetCharacterStats();

    }

    void OnSetOwnership()
    {
        this.isLocallyControlledPlayer = true;
    }

    private void Update()
    {
        UpdateMovementSpeed();

        if (JumpButton)
            lastJumpButtonTime = Time.time;

        UpdateSmoothedMovementDirection();

        // Apply gravity
        // - extra power jump modifies gravity
        ApplyGravity();

        // Apply jumping logic
        ApplyJumping();

        // Apply swinging attack logic
        ApplyAttack();
        if (isLocallyControlledPlayer && CameraMode == CameraMode.Aim && !AimButton && !isCasting)
            CameraMode = CameraMode.Regular;

        ApplyDefending();

        ApplyLocking();

        ApplySpellCasting();

        // Calculate actual motion
        var movement = moveDirection * moveSpeed + new Vector3(0, verticalSpeed, 0) + inAirVelocity;
        movement *= Time.deltaTime;

        // Move the controller
        CharacterController controller = GetComponent<CharacterController>();
        collisionFlags = controller.Move(movement);

        // Set rotation to the move direction
        if (CameraMode == CameraMode.Aim)
            transform.rotation = Quaternion.LookRotation(ForwardDirection);
        else if (LockedOn)
        {
            var towardTarget = lockedTarget.transform.position - transform.position;
            towardTarget.y -= .5f;
            transform.rotation = Quaternion.LookRotation(towardTarget);
        }

        else
        {
            if (IsGrounded() && moveDirection != Vector3.zero)
            {
                transform.rotation = Quaternion.LookRotation(moveDirection);
            }
            else
            {
                var xzMove = movement;
                xzMove.y = 0;
                if (xzMove.magnitude > 0.001)
                {
                    transform.rotation = Quaternion.LookRotation(xzMove);
                }
            }
        }
        // We are in jump mode but just became grounded
        if (IsGrounded())
        {
            lastGroundedTime = Time.time;
            inAirVelocity = Vector3.zero;
            if (jumping)
            {
                jumping = false;
                SendMessage("DidLand", SendMessageOptions.DontRequireReceiver);
            }
        }

        attacktimer -= Time.deltaTime;
    }

    private void UpdateMovementSpeed()
    {
        runSpeed = characterStats.MoveSpeed.ModifiedValue;
        trotSpeed = runSpeed * .8f;
        walkSpeed = runSpeed * .6f;
    }

    private void ApplySpellCasting()
    {
        if (inputManager.spellButton && inputManager.spellBeingCast != null && !IsCasting)
        {
            if (inputManager.spellBeingCast.manaCost > characterStats.Mana.CurrentValue)
            {
                isCasting = false;
                inputManager.spellButton = false;
                inputManager.spellBeingCast = null;
                return;
            }

            switch (inputManager.spellBeingCast.TargetType)
            {
                case SpellTargetType.Point:
                    CastPointSpell(inputManager.spellBeingCast);
                    break;

                case SpellTargetType.Target:
                    StartCoroutine(CastTargetSpell());
                    break;

                case SpellTargetType.Self:
                    CastSelfSpell(inputManager.spellBeingCast);
                    break;
                default:
                    Debug.Log("Invalid spell target type");
                    break;
            }
        }
    }

    private void CastSelfSpell(Spell spell)
    {
        if (spell.manaCost <= characterStats.Mana.CurrentValue && !isCasting)
        {
            StartCoroutine(SelfSpellCastTimer(spell));
        }
    }

    private IEnumerator SelfSpellCastTimer(Spell spell)
    {
        isCasting = true;
        float startTime = Time.time;
        float endTime = startTime + spell.castTime;
        while (startTime < endTime)
        {
            yield return null;
            startTime += Time.deltaTime;
            if (startTime > endTime) Debug.Log("Cast time complete.");
            if (isMoving || IsJumping()) { isCasting = false; inputManager.spellButton = false; inputManager.spellBeingCast = null; yield break; }
        }
        characterStats.Mana.CurrentValue -= spell.manaCost;

        ((ISelfSpell)spell).Execute(this.gameObject);
        if (Network.isServer)
        networkView.RPC("SimulateISelfSpellExecute", RPCMode.Others, (int)spell);

        isCasting = false;
        inputManager.spellButton = false;
        inputManager.spellBeingCast = null;
        yield break;
    }


    
    private void CastPointSpell(Spell spell)
    {
        if (AttackButton && spell.manaCost <= characterStats.Mana.CurrentValue && !isCasting)
        {
            StartCoroutine(PointSpellCastTimer(spell));
        }
    }

    private IEnumerator PointSpellCastTimer(Spell spell)
    {
        isCasting = true;
        float startTime = Time.time;
        float endTime = startTime + spell.castTime;
        while (startTime < endTime)
        {
            yield return null;
            startTime += Time.deltaTime;
            if (startTime > endTime) Debug.Log("Cast time complete.");
            if (isMoving || IsJumping()) { isCasting = false; inputManager.spellButton = false; inputManager.spellBeingCast = null; yield break; }
        }

        characterStats.Mana.CurrentValue -= spell.manaCost;
        ((IPointSpell)spell).Execute(gameObject, inputManager.targetClickLocation);
        if (Network.isServer)
        networkView.RPC("SimulateIPointSpellExecute", RPCMode.Others, (int)spell, inputManager.targetClickLocation);
        isCasting = false;
        inputManager.spellButton = false;
        inputManager.spellBeingCast = null;
        yield break;
    
    }

    private void UpdateSmoothedMovementDirection()
    {
        bool grounded = IsGrounded();

        // Forward vector relative to the camera along the x-z plane	
        Vector3 forward = ForwardDirection;
        forward.y = 0;
        forward = forward.normalized;

        // Right vector relative to the camera
        // Always orthogonal to the forward vector
        var right = new Vector3(forward.z, 0, -forward.x);

        // Are we moving backwards or looking backwards
        if (VerticalInput < -0.2)
            movingBack = true;
        else
            movingBack = false;

        isMoving = Mathf.Abs(HorizontalInput) > 0.1 || Mathf.Abs(VerticalInput) > 0.1;

        // Target direction relative to the camera
        Vector3 targetDirection = HorizontalInput * right + VerticalInput * forward;
        

        // Grounded controls
        if (grounded)
        {
            // We store speed and direction seperately,
            // so that when the character stands still we still have a valid forward direction
            // moveDirection is always normalized, and we only update it if there is user input.
            if (targetDirection != Vector3.zero)
            {
                moveDirection = targetDirection.normalized;
            }

            // Smooth the speed based on the current target direction
            var curSmooth = speedSmoothing * Time.deltaTime;

            // Choose target speed
            //* We want to support analog input but make sure you cant walk faster diagonally than just forward or sideways
            var targetSpeed = Mathf.Min(targetDirection.magnitude, 1.0f);



            // Pick speed modifier
            if (SprintButton && !IsAiming && (characterStats.Stamina.CurrentValue > sprintStaminaThreshold || (lastFrameSprinting && characterStats.Stamina.CurrentValue > sprintCostPerSecond*Time.deltaTime)))
            {
                targetSpeed *= runSpeed;
                characterStats.Stamina.CurrentValue -= (int)(Time.deltaTime * sprintCostPerSecond);
                lastFrameSprinting = true;
            }
            
            else if (Time.time - trotAfterSeconds > walkTimeStart)
            {
                targetSpeed *= trotSpeed;
                lastFrameSprinting = false;
            }
            else
            {
                targetSpeed *= walkSpeed;
                lastFrameSprinting = false;
            }

            if (isAttacking)
                targetSpeed *= .5f;
            if (IsAiming)
                targetSpeed *= .3333f;

            moveSpeed = Mathf.Lerp(moveSpeed, targetSpeed, curSmooth);

            // Reset walk time start when we slow down
            if (moveSpeed < walkSpeed * 0.3)
                walkTimeStart = Time.time;
        }
        // In air controls
        else
        {
            if (isMoving)
                inAirVelocity += targetDirection.normalized * Time.deltaTime * inAirControlAcceleration;
        }
    }

    private void ApplyGravity()
    {
        // When we reach the apex of the jump we send out a message
        if (jumping && !jumpingReachedApex && verticalSpeed <= 0.0)
        {
            jumpingReachedApex = true;
            SendMessage("DidJumpReachApex", SendMessageOptions.DontRequireReceiver);
        }

        // * When jumping up we don't apply gravity for some time when the user is holding the jump button
        //   This gives more control over jump height by pressing the button longer
        var extraPowerJump = IsJumping() && verticalSpeed > 0.0 && JumpButton && transform.position.y < lastJumpStartHeight + extraJumpHeight;

        if (extraPowerJump)
            return;
        else if (IsGrounded() && isAffectedByGravity)
            verticalSpeed = -gravity * 0.2f;
        else if (isAffectedByGravity)
            verticalSpeed -= gravity * Time.deltaTime;
    }

    private void ApplyJumping()
    {
        if (isAttacking)
            return;
        // Prevent jumping too fast after each other
        if (lastJumpTime + jumpRepeatTime > Time.time)
            return;

        if (IsGrounded())
        {
            // Jump
            // - Only when pressing the button down
            // - With a timeout so you can press the button slightly before landing		
            if (canJump && Time.time < lastJumpButtonTime + jumpTimeout && characterStats.Stamina.CurrentValue >= jumpStaminaCost)
            {
                characterStats.Stamina.CurrentValue -= (int)jumpStaminaCost;
                verticalSpeed = CalculateJumpVerticalSpeed(jumpHeight);
                DidJump();
            }
        }
    }

    private float CalculateJumpVerticalSpeed(float targetJumpHeight)
    {
        // From the jump height and gravity we deduce the upwards speed 
        // for the character to reach at the apex.
        return Mathf.Sqrt(2 * targetJumpHeight * gravity);
    }

    private void DidJump()
    {
        jumping = true;
        jumpingReachedApex = false;
        lastJumpTime = Time.time;
        lastJumpStartHeight = transform.position.y;
        lastJumpButtonTime = -10;
        SendMessage("BeganJump", SendMessageOptions.DontRequireReceiver);
    }

    private void ApplyLocking()
    {
        if (LockButton)
        {
            if (LockedOn)
            {
                StopLocking();
            }
            else
            {
                LockClosestTarget();
            }
        }
        if (LockedOn)
        {
            if (lockedTarget == null || Vector3.Distance(transform.position, lockedTarget.transform.position) > maxLockRange)
                StopLocking();
        }
    }

    private void LockClosestTarget()
    {
        GameObject closestTarget = null;
        float targetDistance = 10000;

        var playerTargets = GameObject.FindGameObjectsWithTag("Player");
        var lockableTargets = GameObject.FindGameObjectsWithTag("Lockable");
        GameObject[] allTargets = playerTargets.Concat(lockableTargets).ToArray();
        foreach (GameObject gameObject in allTargets)
        {
            if (gameObject.transform.root == this.transform.root)
                continue;
            Vector3 gameObjectPosition = gameObject.transform.position;
            Camera yourCamera;
            yourCamera = Camera.main;

            Vector3 viewpoint = yourCamera.WorldToViewportPoint(gameObjectPosition);
            if (viewpoint.x >= 0 && viewpoint.x <= 1 && viewpoint.y >= 0 && viewpoint.y <= 1 && viewpoint.z > 0 && viewpoint.z < maxLockRange)
            {
                if (Vector3.Distance(transform.position, gameObjectPosition) < targetDistance)
                {
                    closestTarget = gameObject;
                    targetDistance = Vector3.Distance(transform.position, gameObjectPosition);
                }
            }
        }
        if (closestTarget != null)
        {
            lockedTarget = closestTarget;
            lockedOn = true;
            CameraMode = CameraMode.Locked;
        }

    }

    private void StopLocking()
    {
        lockedTarget = null;
        lockedOn = false;
        CameraMode = CameraMode.Regular;
    }

    [RPC]
    private void ClientLockTarget(NetworkViewID viewID)
    {
        lockedTarget = NetworkView.Find(viewID).gameObject;
        lockedOn = true;
    }

    [RPC]
    private void ClientStopLocking()
    {
        lockedTarget = null;
        lockedOn = false;
    }

    private void ApplyDefending()
    {

        isDefending = (DefendButton && (inventory.currentShield!=null) && !IsJumping());
        
    }

    private void ApplyAttack()
    {
        if (IsJumping() || isAttacking)
            return;
        if (AttackButton && !inputManager.spellButton && inputManager.spellBeingCast == null)
        {
            switch (WeaponType)
            {
                case WeaponType.Swing:
                    BeginSwingAttack();
                    break;
                case WeaponType.Aim:
                    if (!isBowShooting)
                    {
                        StartCoroutine(AimAttack());
                    }
                    break;

            }
        }
    }

    [RPC]
    private void BeginSwingAttack()
    {
        if (Network.isServer) networkView.RPC("BeginSwingAttack", RPCMode.Others);
        StartCoroutine(SwingAttack());
    }

    private IEnumerator SwingAttack()
    {
        SendMessage("StartAttacking", SendMessageOptions.DontRequireReceiver);
        isAttacking = true;
        var swingLength = .8f;
        if (inventory.currentWeapon.numHands == 2)
            swingLength = .95f;
        yield return new WaitForSeconds(.15f);
        try { GetComponentInChildren<WeaponCollider>().StartSwingCollisionChecking(); }
        catch { isAttacking = false; yield break; }
        yield return new WaitForSeconds(swingLength - .15f);
        try { GetComponentInChildren<WeaponCollider>().isActive = false; }
        catch { isAttacking = false; yield break; }
        isAttacking = false;
        yield break;
    }

    private IEnumerator AimAttack()
    {
        float timeRemaining;
        //Check to see if aiming right after previous attack to use the reload animation
        if (attacktimer > 0)
        {
            SendMessage("StillAiming");
            timeRemaining = .3f; //timer for allowing another arrow to be released
        }
        else
        {
            SendMessage("BeginAim");
            timeRemaining = 0.6f; //longer timer if drawing back bowstring for first time
        }
        isBowShooting = true;
        var aimReady = false;
        
        playerSound.PlayArrowDraw();
        while (true)
        {
            if (isLocallyControlledPlayer && AimButton)
                CameraMode = CameraMode.Aim;

            if (!AttackButton)
            {
                if (aimReady)
                {
                    isBowShooting = false;
                    FireArrow();
                    break;
                }
                else
                {
                    isBowShooting = false;
                    EarlyArrowRelease();

                    break;
                }
            }
            yield return new WaitForSeconds(.05f);

            aimReady = ((timeRemaining -= .05f) <= 0);
        }
    }

    private IEnumerator CastTargetSpell()
    {
        Debug.Log("Starting coroutine CastTargetSpell");
        isCasting = true;
        isBowShooting = true;
        if (isLocallyControlledPlayer)
        {
            CameraMode = CameraMode.Aim;
        }
        while (true)
        {
            if (inputManager.attackButton && inputManager.spellBeingCast != null && characterStats.Mana.CurrentValue >= inputManager.spellBeingCast.manaCost)
            {
                if (inputManager.spellBeingCast == null) Debug.Log("spell being cast is null");
                    ((ITargetSpell)inputManager.spellBeingCast).Execute(this.gameObject, transform.forward, inputManager.homingTarget);
                if (Network.isServer)
                    networkView.RPC("SimulateITargetSpellExecute", RPCMode.Others, (int)inputManager.spellBeingCast, transform.forward, inputManager.homingTarget ? inputManager.homingTarget.GetNetworkViewID() : NetworkViewID.unassigned);

                characterStats.Mana.CurrentValue -= inputManager.spellBeingCast.manaCost;
                yield return new WaitForSeconds(.1f);
                isCasting = false;
                isBowShooting = false;
                inputManager.spellButton = false;
                inputManager.spellBeingCast = null;
                yield break;
            }
            yield return null;
        }
    }

    private void EarlyArrowRelease()
    {
        SendMessage("EndAim"); //Ending animation for aiming
    }

    private void FireArrow()
    {
        if (Network.isServer)
        {
            Vector3 startLocation = inventory.currentWeaponGameobject.transform.position;

            networkView.RPC("SpawnArrowPrefab", RPCMode.All, startLocation, transform.forward, arrowFireVelocity);

        }
        SendMessage("EndAim");
        attacktimer = 1f; //timer to allow character to reload bow animation
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        //	Debug.DrawRay(hit.point, hit.normal);
        if (hit.moveDirection.y > 0.01)
            return;
    }

    public bool IsJumping()
    {
        return jumping;
    }

    public bool IsGrounded()
    {
        return (collisionFlags & CollisionFlags.CollidedBelow) != 0;
    }

    private Vector3 GetDirection()
    {
        return moveDirection;
    }

    public bool IsMovingBackwards
    {
        get { return movingBack; }
    }

    public float GetLean()
    {
        return 0.0f;
    }

    private bool HasJumpReachedApex()
    {
        return jumpingReachedApex;
    }

    private bool IsGroundedWithTimeout()
    {
        return lastGroundedTime + groundedTimeout > Time.time;
    }
}

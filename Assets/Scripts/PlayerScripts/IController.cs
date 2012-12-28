using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

/// <summary>
/// Common interface for pulling information for animations.
/// </summary>
public interface IController
{
    float WalkSpeed { get; }
    float MoveSpeed { get; }
    float VerticalSpeed { get; }
    Vector3 InAirVelocity { get; }
    bool IsJumping();
    bool IsAttacking { get; }
    bool IsDefending { get; }
    Vector3 MoveDirection { get; }
    CollisionFlags CollisionFlags { get; set; }
    bool IsAiming { get; }
    GameObject LockedTarget { get; }
    bool LockedOn { get; }
    bool IsMovingBackwards { get; }
    bool IsCasting { get; }
}


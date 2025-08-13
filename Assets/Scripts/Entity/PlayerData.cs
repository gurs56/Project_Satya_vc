using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Project Satya/PlayerData")]
public class PlayerData : ScriptableObject {
    [Header("Movement")]
    public float sprintSpeed;
    public float dashSpeed;
    public float dashSpeedChangeFactor;
    public float walkSpeed;

    public float groundDrag;
    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;

    [Header("Gravity")]
    public float gravityMultiplier;
    public float fallMultiplier;

    [Header("Ground Check")]
    public float playerHeight;
    [Tooltip("Determines which layers count as ground")]
    public LayerMask groundLayerMask;
}

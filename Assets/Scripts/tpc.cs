﻿using Unity.Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tpc : MonoBehaviour
{
    [Header("References")]
    public Transform orientation;
    public Transform playerObject;
    public Transform player;
    public Rigidbody rb;

    public float rotationSpeed;

    public Transform comboatLoakAt;

    public GameObject ThirdPersonCam;
    public GameObject CombatCam;

    public CameraState currentStyle;
    
    public enum CameraState
    {
        Basic,
        Combat
    }


    // Start is called before the first frame update
    void Start()
    {
        if (playerObject == null)
            Debug.LogError("playerObject is not assigned!");
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            SwitchCameraStyle(CameraState.Basic);
        if (Input.GetKeyDown(KeyCode.Alpha2))
            SwitchCameraStyle(CameraState.Combat);


        // Rotate orientation
        Vector3 viewDir = player.position - new Vector3(transform.position.x, player.position.y, transform.position.z);
        orientation.forward = viewDir.normalized;

        if(currentStyle == CameraState.Basic)
        {
            // Rotate player object
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");
            Vector3 inputDir = orientation.forward * verticalInput + orientation.right * horizontalInput;

            if (inputDir != Vector3.zero)
                playerObject.forward = Vector3.Slerp(playerObject.forward, inputDir.normalized, Time.deltaTime * rotationSpeed);
        }
    
        else if (currentStyle == CameraState.Combat)
        {
            Vector3 dirToCombatLookAt = comboatLoakAt.position - new Vector3(transform.position.x, player.position.y, transform.position.z);
            orientation.forward = dirToCombatLookAt.normalized;

            playerObject.forward = dirToCombatLookAt.normalized;
        }
    
    }

    private void SwitchCameraStyle(CameraState newState)
    {
        ThirdPersonCam.SetActive(false);
        CombatCam.SetActive(false);

        if (newState == CameraState.Basic) CombatCam.SetActive(true);
        if (newState == CameraState.Basic) ThirdPersonCam.SetActive(true);

        currentStyle = newState;

    }
        



}

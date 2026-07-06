using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Variables & References")]
    [SerializeField] float moveSpeed = 5;
    [SerializeField] GameObject playerCamera;
    [SerializeField] Rigidbody rb;

    Vector3 movementDir;
    bool isGrounded;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    #region Movement
    //Function that PlayerInput component calls, edits movementDir depending on player input
    public void GetMovementInput(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.canceled)
        {
            movementDir = Vector3.zero;
        }
        else
        {
            Vector2 temp = callbackContext.ReadValue<Vector2>();
            movementDir = new Vector3(temp.x, 0, temp.y);
            
        }
    }
    //Changes the rigidbody's linear velocity depending on movementDir and camera direction
    private void MovementUpdate()
    {
        var camForward= playerCamera.transform.forward;
        camForward.y = 0;
        Quaternion camRotationFlattened = Quaternion.LookRotation(camForward, Vector3.up);
        transform.rotation = camRotationFlattened;
        
        Vector3 newVel = rb.linearVelocity;
        
        newVel.x = movementDir.x * moveSpeed;
        newVel.z = movementDir.z * moveSpeed;

        newVel = camRotationFlattened * newVel;
        
        rb.linearVelocity = newVel;
    }
    #endregion

    void Update()
    {
        MovementUpdate();
    }
}

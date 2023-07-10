using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace myStateMachine
{
    public class Character : MonoBehaviour
    {
        [Header("Movement")]
        public float walkingSpeed;

        public Transform orientation;

        Vector3 moveDirection;

        Rigidbody rb;
        public Vector2 playerInput { get; set; }
        public PlayerInput input;


        private void Awake()
        {
            input = new PlayerInput();
        }

        private void OnEnable()
        {
            input.Enable();
        }

        private void OnDisable()
        {
            input.Disable();
        }
        private void Start()
        {

            rb = GetComponent<Rigidbody>();
        }
        private void FixedUpdate()
        {
            MovePlayer();
            SpeedControl();
            Rotation();
        }

        public void MovePlayer()
        {
            moveDirection = orientation.forward * input.Player.Move.ReadValue<Vector2>().y + orientation.right * input.Player.Move.ReadValue<Vector2>().x;
            rb.AddForce(moveDirection.normalized * walkingSpeed * 10f, ForceMode.Force);
            Debug.Log(rb.velocity);
        }
        public void SpeedControl()
        {
            Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
            if (flatVel.magnitude > walkingSpeed)
            {
                Vector3 limitedVel = flatVel.normalized * walkingSpeed;
                rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
            }
        }
        public void Rotation()
        {
            Vector3 mousePosMain = Input.mousePosition;
            mousePosMain.z = Camera.main.transform.position.y;
            Vector3 curPosition = Camera.main.ScreenToWorldPoint(mousePosMain);
            Vector3 lookPos = curPosition - transform.position;
            lookPos.y = 0;
            transform.rotation = Quaternion.LookRotation(lookPos);
        }
    }
}
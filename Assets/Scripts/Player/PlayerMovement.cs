using System;
using Input;
using UnityEngine;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
        private readonly AInput _input = InputManager.Input;
        private readonly AMouseInput _mouseInput = InputManager.MouseInput;

        [Header("Specifications")]
        [SerializeField] private float speed = 5f;
        [SerializeField] private float sprintSpeed = 5f;
        [SerializeField] private float jumpHeight = 4f;
        [SerializeField] private float gravity = -9.81f;
        
        [Header("Camera point")]
        [SerializeField] private Vector3 cameraPointOffset = new Vector3(1, 1, 1);
        [SerializeField] [Range(2, 8)] private float cameraPointDistance = 5f;
        [SerializeField] private float cameraPointMinDistance = 2f;
        [SerializeField] private float cameraPointMaxDistance = 8f;
        [SerializeField] private float cameraPointMaxY = 5f;
        [SerializeField] private float cameraPointMinY = 2f;
        [SerializeField] private float horizontalMouseSensitivity = 50f;
        [SerializeField] private float verticalMouseSensitivity = 50f;

        [Header("Links")]
        [SerializeField] private CharacterController characterController;
        [SerializeField] private Transform cameraPoint;

        private Vector3 _cameraPointTransformVelocity;

        private Vector3 _velocity;
        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        private void Update()
        {
            _input.Update();
            _mouseInput.Update();
        }

        private void FixedUpdate()
        {
            var source = transform;

            if (characterController.isGrounded)
                _velocity.y = -2f;
            
            var inputDirection = new Vector3(_input.LeftRight, 0, _input.ForwardBack).normalized;
            
            if (!(inputDirection.magnitude >= 0.1f)) return;
            
            var x = inputDirection.x * (speed + sprintSpeed * _input.Sprint) * Time.fixedDeltaTime;
            var z = inputDirection.z * (speed + sprintSpeed * _input.Sprint) * Time.fixedDeltaTime;
            
            var move = source.forward * z + source.right * x;
            
            characterController.Move(move);
            
            source.rotation = Quaternion.Euler(0, cameraPoint.eulerAngles.y, 0);
            
            _velocity.y += gravity * Time.fixedDeltaTime;
            
            characterController.Move(_velocity * Time.fixedDeltaTime);
            
        }

        private void LateUpdate()
        {
            var source = transform;
            
            var mouseX = _mouseInput.MouseX * horizontalMouseSensitivity * Time.fixedDeltaTime;
            var mouseY = _mouseInput.MouseY * verticalMouseSensitivity * Time.fixedDeltaTime;
            
            cameraPointDistance += _mouseInput.MouseWheel;

            cameraPointDistance = Mathf.Clamp(cameraPointDistance, cameraPointMinDistance, cameraPointMaxDistance);

            var rotation = Quaternion.Euler(0, mouseX, 0) ;
            cameraPointOffset = rotation * cameraPointOffset;
            cameraPointOffset.y += mouseY;
            cameraPointOffset.y = Mathf.Clamp(cameraPointOffset.y, cameraPointMinY, cameraPointMaxY);
            
            var position = source.position;
            var desiredPosition = position + cameraPointOffset * cameraPointDistance;

            cameraPoint.position = desiredPosition;
            cameraPoint.LookAt(position);
        }
    }
}
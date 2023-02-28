using System;
using UnityEngine;

public class Ship : MonoBehaviour
 {
     [SerializeField] private Rigidbody starShip;
     [SerializeField] private FixedJoystick joystick;
     [SerializeField] private Camera mainCamera;
     [SerializeField] private RotationController rotationController;
     
     [SerializeField, Range(0f,5f)] private float cameraRotationSpeed;
     [SerializeField, Range(0, 5f)] private float shipRotationSpeed;
     
     [SerializeField, Range(0, 100)] private int shipMoveSpeed;
     [SerializeField, Range(0, 100)] private int cameraMoveSpeed;

     private PlayerCamera _playerCamera;
     
     private void Awake()
     {
         _playerCamera = new PlayerCamera(mainCamera, rotationController, transform,
             cameraRotationSpeed, cameraMoveSpeed);
     }

     private void Move()
     {
         var transform1 = transform;
         var direction = joystick.Direction.x * transform1.right + joystick.Direction.y * transform1.forward;
         starShip.velocity =direction * shipMoveSpeed ;
     }
     
     private void FixedUpdate()
     {
         Move();
         Rotate();
         
         _playerCamera.UpdateCameraView();
     }

     private void Rotate()
     {
         var myRotation = gameObject.transform.rotation;
         myRotation = Quaternion.Lerp(myRotation, rotationController.AddTo,  Time.fixedTime * shipRotationSpeed);

         gameObject.transform.rotation = myRotation;
     }
 }
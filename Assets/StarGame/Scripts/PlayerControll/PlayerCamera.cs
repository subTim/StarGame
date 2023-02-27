using DG.Tweening;
using UnityEngine;

 public class PlayerCamera
 {
     private RotationController _rotationController;
     private Camera _camera;
     private Transform _cameraOrbit;
     private Transform _shipCords;
     
     private Vector3 _offset;
     private float _speedMove;
     private float _speedRotation;


     public PlayerCamera(Camera camera, RotationController rotationController,
         Transform shipPosition, float speedRotation, float speedMove )
     {
         _camera = camera;
         
         var cameraTransform = _camera.transform;
         
         _cameraOrbit = cameraTransform.parent;
         _rotationController = rotationController;
         
         _speedRotation = speedRotation;
         _speedMove = speedMove;
         _shipCords = shipPosition;
     }

     public void UpdateCameraView()
     {
         Move();
         Rotate();
     }
     
     private void Move()
     {
         _cameraOrbit.position = Vector3.Lerp(_cameraOrbit.position, _shipCords.position,Time.fixedTime *  _speedMove);
     }

     private void Rotate()
     {
         var cameraRotation = _cameraOrbit.rotation;

         cameraRotation = Quaternion.Lerp(cameraRotation, _rotationController.AddTo, Time.fixedTime * _speedRotation);
             
         _cameraOrbit.rotation = cameraRotation;
     }
 }
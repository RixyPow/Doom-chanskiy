using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSMovement : MonoBehaviour
{
    public float _speed = 6.0f;
    public float _gravity = 9.81f;
    private CharacterController _characterController;
   private void Start()
   {
      _characterController = GetComponent<CharacterController>();
      if (_characterController == null)
         Debug.Log("CharacterController is NULL");

   }
   private void Update(){
      float deltaX = Input.GetAxis("Horizontal") * _speed;
      float deltaZ = Input.GetAxis("Vertical") * _speed;
      Vector3 movement = new Vector3(deltaX, 0, deltaZ);
      movement = Vector3.ClampMagnitude(movement, _speed);
      movement *= Time.deltaTime;
      movement = transform.TransformDirection(movement);
      _characterController.Move(movement);
      movement.y = _gravity;
     }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSMovement : MonoBehaviour
{
      public float _speed = 6.0f;
   public float _gravity = 9.81f;
   public float _jumpHeight = 2.0f; // Высота прыжка
   private CharacterController _characterController;
   private Vector3 _velocity;

   private void Start()
   {
      _characterController = GetComponent<CharacterController>();
      if (_characterController == null)
         Debug.Log("CharacterController is NULL");
   }

   private void Update()
   {
      float deltaX = Input.GetAxis("Horizontal") * _speed;
      float deltaZ = Input.GetAxis("Vertical") * _speed;
      Vector3 movement = new Vector3(deltaX, 0, deltaZ);
      movement = Vector3.ClampMagnitude(movement, _speed);
      movement = transform.TransformDirection(movement);

      if (_characterController.isGrounded)
      {
         _velocity.y = 0; // Обнуление вертикальной скорости при касании земли

         if (Input.GetButtonDown("Jump"))
         {
               _velocity.y = Mathf.Sqrt(_jumpHeight * 2 * _gravity); // Расчет скорости прыжка
         }
      }

      _velocity.y -= _gravity * Time.deltaTime; // Применение гравитации
      movement += _velocity * Time.deltaTime; // Добавление вертикальной скорости к движению

      _characterController.Move(movement);
   }
}

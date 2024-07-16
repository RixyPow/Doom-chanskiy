using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSMovement : MonoBehaviour
{
    public float _speed = 6.0f;
public float _gravity = 9.81f;
public float _jumpHeight = 2.0f;
private CharacterController _characterController;
private Vector3 _velocity;
private Jetpack _jetpack; 

private void Start() 
{
    _characterController = GetComponent<CharacterController>();
    _jetpack = GetComponent<Jetpack>();
    if (_characterController == null)
        Debug.Log("CharacterController is NULL");
}

private void Update() //передвижение
{
    float deltaX = Input.GetAxis("Horizontal") * _speed;
    float deltaZ = Input.GetAxis("Vertical") * _speed;
    Vector3 movement = new Vector3(deltaX, 0, deltaZ);
    movement = Vector3.ClampMagnitude(movement, _speed);
    movement = transform.TransformDirection(movement);

    if (_characterController.isGrounded) //детектор соприкосновения с землей
    {
        _velocity.y = 0; 

        if (Input.GetButtonDown("Jump"))
        {
            _velocity.y = Mathf.Sqrt(_jumpHeight * 2 * _gravity); 
        }

        
        if (_jetpack != null)
        {
            _jetpack.ResetJetpackTime();
        }
    }

    
    if (_jetpack != null && _jetpack.IsJetpackActive && !_characterController.isGrounded) //импульс джетпака
    {
        _velocity.y += _jetpack.ApplyJetpackForce();
    }

    _velocity.y -= _gravity * Time.deltaTime; 
    movement += _velocity * Time.deltaTime; 

    _characterController.Move(movement);
}

}

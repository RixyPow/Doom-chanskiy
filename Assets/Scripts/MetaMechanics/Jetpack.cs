using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jetpack : MonoBehaviour
{
    public float _jetpackForce = 5.0f; 
    public float _jetpackDuration = 2.0f; 
    private float _jetpackTime;

    public float ApplyJetpackForce()
    {
        if (Input.GetButton("Jump") && _jetpackTime < _jetpackDuration)
        {
            _jetpackTime += Time.deltaTime; 
            return _jetpackForce * Time.deltaTime; 
        }
        else if (GetComponent<CharacterController>().isGrounded)
        {
            _jetpackTime = 0; 
        }

        return 0; 
    }
}

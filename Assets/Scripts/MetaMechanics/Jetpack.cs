using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jetpack : MonoBehaviour
{
    public float _jetpackForce = 5.0f; 
    public float _jetpackDuration = 2.0f; 
    public float _jetpackCooldown = 10.0f; 
    private float _jetpackTime;
    private float _cooldownTime;
    public bool IsJetpackActive = false; 

    public float ApplyJetpackForce()
    {
        if (IsJetpackActive && Input.GetButton("Jump") && _jetpackTime < _jetpackDuration && _cooldownTime <= 0)
        {
            _jetpackTime += Time.deltaTime;
            return _jetpackForce * Time.deltaTime; 
        }

        return 0;
    }

    public void ResetJetpackTime()
    {
        _jetpackTime = 0; 
        _cooldownTime = _jetpackCooldown; 
    }

    private void Update()
    {
        if (_cooldownTime > 0)
        {
            _cooldownTime -= Time.deltaTime; 
        }
    }
}

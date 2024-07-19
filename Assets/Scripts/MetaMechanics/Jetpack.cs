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
    public bool IsJetpackActive = false; // Переменная для активации джетпака

    public float ApplyJetpackForce()
    {
        // Проверяем активен ли джетпак и не находимся ли мы в кулдауне
        if (IsJetpackActive && Input.GetButton("Jump") && _jetpackTime < _jetpackDuration && _cooldownTime <= 0)
        {
            _jetpackTime += Time.deltaTime;
            return _jetpackForce * Time.deltaTime; 
        }

        return 0; // Возвращаем 0, если джетпак не активен
    }

    public void ResetJetpackTime()
    {
        // Сбрасываем время использования джетпака и устанавливаем кулдаун
        _jetpackTime = 0; 
        _cooldownTime = _jetpackCooldown; 
    }

    private void Update()
    {
        // Уменьшаем время кулдауна
        if (_cooldownTime > 0)
        {
            _cooldownTime -= Time.deltaTime; 
        }
    }
}



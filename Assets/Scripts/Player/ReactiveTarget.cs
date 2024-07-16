using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;


public class ReactiveTarget : MonoBehaviour
{
    private EnemyAI _enemyAI;
    private EnemyCharacter _enemyHealth;

    private void Start()
    {
        _enemyAI = GetComponent<EnemyAI>();
        _enemyHealth = GetComponent<EnemyCharacter>();
    }

    public void ReactToHit(int damage) // детектор попаданий
    {
        if (_enemyAI != null)
        {
            _enemyAI.SetAlive(false);
        }
        if (_enemyHealth != null)
        {
            _enemyHealth.TakeDamage(damage);
        }
    }
}




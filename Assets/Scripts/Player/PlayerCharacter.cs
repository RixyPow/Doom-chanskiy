using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    [SerializeField] private int _health = 100;
    [SerializeField] private int _armor = 50;

    public int Health => _health;
    public int Armor => _armor;

    private void Start()
    {
        _health = 100;
        _armor = 50;
    }

    public void Hurt(int damage)
    {
        if (_armor > 0)
        {
            int effectiveDamage = Mathf.Min(damage, _armor);
            _armor -= effectiveDamage;
            damage -= effectiveDamage;
        }

        if (damage > 0)
        {
            _health -= damage;
        }

        if (_health <= 0)
        {
            Destroy(gameObject);
        }

        Debug.Log("Player health: " + _health + ", Player armor: " + _armor);
    }
}

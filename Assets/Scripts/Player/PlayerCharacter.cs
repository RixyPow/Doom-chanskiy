using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement; // Добавьте эту строку

public class PlayerCharacter : MonoBehaviour
{
    [SerializeField] public int _health = 100;
    [SerializeField] public int _armor = 50;

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
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex -1);
        }

        Debug.Log("Player health: " + _health + ", Player armor: " + _armor);
    }

    public void IncreaseHealth(int amount)
    {
        _health = Mathf.Min(_health + amount, 100);
        Debug.Log("Player health increased: " + _health);
    }
}


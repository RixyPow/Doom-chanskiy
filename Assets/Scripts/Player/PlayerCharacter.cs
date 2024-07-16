using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
     [SerializeField]private int _health = 5;

    private void Start(){
        _health = 100;

    }
    public void Hurt(int damage){
        _health -= damage;
        if(_health<=0){
            Destroy(gameObject);
        }
        Debug.Log("Player health " + _health);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    public float speed = 10f;
    public int damage = 1;

    private void Update(){
        transform.Translate(0,0,speed*Time.deltaTime);
    }
    
    private void OnTriggerEnter(Collider other){
        Debug.Log(other.name);

        PlayerCharacter player = other.GetComponent<PlayerCharacter>();
        if(player != null){
            player.Hurt(damage);
        }
        Destroy(this.gameObject);
    }
}

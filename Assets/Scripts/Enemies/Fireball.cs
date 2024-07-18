using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    public float speed = 10f;
    public int damage = 10;

    private void Update(){
        transform.Translate(0,0,speed*Time.deltaTime);
        StartCoroutine(AutoDestroy());
    }

    private void OnTriggerEnter(Collider other){
        Debug.Log(other.name);

        PlayerCharacter player = other.GetComponent<PlayerCharacter>();
        if(player != null){
            player.Hurt(damage);
        }
        Destroy(this.gameObject);
    }
    private IEnumerator AutoDestroy()
    {
        yield return new WaitForSeconds(2.0f); 
        Destroy(this.gameObject);
    }
        
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    public float speed = 10f;
    public int damage = 1;

    private void Update(){
        transform.Translate(0,0,speed*Time.deltaTime);
        // Вызов метода для автоудаления объекта
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

    // Метод для автоудаления объекта через заданное время
    private IEnumerator AutoDestroy()
    {
        yield return new WaitForSeconds(2.0f); // Задержка перед удалением объекта (например, 2 секунды)
        Destroy(this.gameObject);
    }
        
}

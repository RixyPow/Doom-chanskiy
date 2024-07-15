using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyAI : MonoBehaviour
{
    public float speed = 5.0f;
    public bool _alive = true;

    [SerializeField]
    private GameObject[] _fireballsPrefab;
    private GameObject _fireball;
    private float timerAttack = 0.0f;
    private float intervalAttack = 1.5f;

    public void Start(){
        _alive = true;
    }

    private void Update(){
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        timerAttack += Time.deltaTime;

        Debug.DrawRay(transform.position, transform.forward * 100, Color.red); //отрисовка

        if (Physics.Raycast(ray, out hit)){
            GameObject hitObject = hit.transform.gameObject;

            if (hitObject.GetComponent<CharacterController>()){
                
                if(timerAttack >= intervalAttack){
                    int randFireball= Random.Range(0, _fireballsPrefab.Length);
                    _fireball = Instantiate(_fireballsPrefab[randFireball]) as GameObject;
                    _fireball.transform.position = transform.TransformPoint(Vector3.forward * 1.5f);
                    _fireball.transform.rotation = transform.rotation;
                    timerAttack = 0f;
                }
            }
        }
    }
    public void SetAlive(bool alive)
    {
        _alive = alive;
    }

}

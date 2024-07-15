using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class ReactiveTarget : MonoBehaviour
{
    private EnemyAI _enemyAI;
    private void Start(){
        _enemyAI = GetComponent<EnemyAI>();
    }

    public void ReactToHit(){ //детектор попаданий
        if(_enemyAI != null)
        _enemyAI.SetAlive(false);
        StartCoroutine(DieCoroutine(1));
    }
    private IEnumerator DieCoroutine(float waitSecond){ //смерть, потом добавлю анимацию
        this.transform.Rotate(45, 0 , 0);
        yield return new WaitForSeconds(waitSecond);
        Destroy(this.transform.gameObject);
    }

}


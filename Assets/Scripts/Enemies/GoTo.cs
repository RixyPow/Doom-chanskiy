using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GoTo : MonoBehaviour
{
    NavMeshAgent agent;
    public float distanceAttack=1.0f;
    public float rotationSpeed = 0.5f;

    void Start(){
        agent = GetComponent<NavMeshAgent>();
    }
    void Update(){
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null){
            Transform playerTransform = player.transform;
            agent.destination = playerTransform.position;
            if(agent.desiredVelocity.magnitude == 0) {
            var direction = playerTransform.position - transform.position;
            var targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);}
        }
    }
}

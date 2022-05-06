using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStateMachine : MonoBehaviour
{
    private NavMeshAgent agent;
    private SphereCollider sc;
    public Transform playerObj;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        sc = GetComponent<SphereCollider>();
        playerObj = GameObject.Find("PlayerCapsule").transform;
    }
    private void OnTriggerStay(Collider other)
    {
        ChaseState();
    }

    public void ChaseState()
    {
        agent.SetDestination(playerObj.position);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStateMachine : MonoBehaviour
{
    private NavMeshAgent agent;
    private SphereCollider sc;
    public Transform playerObj;
    public state State = state.idle;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        sc = GetComponent<SphereCollider>();
        playerObj = GameObject.Find("PlayerCapsule").transform;
        State = state.idle;
    }
    private void OnTriggerStay(Collider other)
    {
        ChaseState();
    }

    private void OnTriggerExit(Collider other)
    {
        State = state.idle;
    }
    public void ChaseState()
    {
        State = state.chase;
        agent.SetDestination(playerObj.position);
    }
    public enum state
    {
        idle,
        patrol,
        chase,
        shoot
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}

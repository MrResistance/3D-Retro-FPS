using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public EnemyStateMachine StateMachine;
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.GetType() == typeof(SphereCollider))
        {
            Debug.Log("SHOOT");
            StateMachine.State = EnemyStateMachine.state.shoot;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        StateMachine.State = EnemyStateMachine.state.chase;
    }
    void Start()
    {
        StateMachine = GetComponentInParent<EnemyStateMachine>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

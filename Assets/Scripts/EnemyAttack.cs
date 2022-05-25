using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public EnemyStateMachine StateMachine;
    public objectPooler objPool;
    [SerializeField]
    private GameObject proj;
    //private Animator animator;
    //public AnimationEvent shootProj;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            StateMachine.State = EnemyStateMachine.state.shoot;
            Shoot();
        }
    }
    public void Shoot()
    {
        objPool.GetObjectFromPool(proj);
        proj.SetActive(true);
        proj.transform.position = this.transform.position + new Vector3(0, 1, 1);
    }
    private void OnTriggerExit(Collider other)
    {
        StateMachine.State = EnemyStateMachine.state.chase;
    }
    void Start()
    {
        StateMachine = GetComponentInParent<EnemyStateMachine>();
        objPool = GameObject.FindObjectOfType<objectPooler>();
    }
}

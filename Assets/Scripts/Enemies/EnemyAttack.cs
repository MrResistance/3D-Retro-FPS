using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public EnemyStateMachine StateMachine;
    public ObjectPooler objPool;
    [SerializeField]
    //private GameObject proj;
    private Transform firePos;

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
        objPool.SpawnFromPool("Enemy Projectile", firePos.position, firePos.rotation);
        //proj.SetActive(true);
        //proj.transform.position = this.transform.position + new Vector3(0, 1, 1);
    }
    private void OnTriggerExit(Collider other)
    {
        StateMachine.State = EnemyStateMachine.state.chase;
    }
    void Start()
    {
        StateMachine = GetComponentInParent<EnemyStateMachine>();
        objPool = GameObject.FindObjectOfType<ObjectPooler>();
        firePos = GetComponentInParent<Transform>().parent.GetChild(0).GetChild(0);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundStomp : MonoBehaviour
{
    [SerializeField]
    private float damage;
    private SphereCollider sphereCollider;
    private void Awake()
    {
        sphereCollider = GetComponent<SphereCollider>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Capsule")
        {
            other.GetComponent<Health>().TakeDamage(damage);
        }
    }
    public void DisableObject()
    {
        gameObject.SetActive(false);
    }
}

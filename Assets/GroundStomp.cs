using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundStomp : MonoBehaviour
{
    public float damage;
    private MeshCollider meshCollider;
    private void Awake()
    {
        meshCollider = GetComponentInChildren<MeshCollider>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Capsule")
        {
            other.GetComponent<Health>().TakeDamage(damage);
        }
        Debug.Log("trigger gs: " + other.name);
    }
    public void DisableObject()
    {
        gameObject.SetActive(false);
    }
}

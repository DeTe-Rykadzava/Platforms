using System;
using System.Collections;
using System.Collections.Generic;
using Interfaces;
using UnityEngine;

public class DeadZone : MonoBehaviour
{
    [SerializeField] private float damage = 10f;
    // private void OnCollisionEnter(Collision other)
    // { 
    //     Debug.Log("Damage");
    //     if (other.gameObject.transform is IDamageable obj)
    //     {
    //         obj.TakeDamage(damage);
    //     }
    // }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.TryGetComponent(out IDamageable obj))
        {
            Debug.Log("Damage");
            obj.TakeDamage(damage);
        }
    }
}

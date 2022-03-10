using System;
using UnityEngine;
using System.Collections;

public class EnemyDamage : MonoBehaviour, IDamageable
{
    public float Health { get; set; }
    //public GameObject ragdoll;

    private void OnEnable()
    {
        Health = 100f;
    }

    public void TakeDamage(float damage)
    {
        Health -= damage;
        if (Health <= 0)
        {
            gameObject.SetActive(false);
            //Instantiate(ragdoll, transform.position, transform.rotation);
        }
    }
}
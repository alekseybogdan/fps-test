using UnityEngine;
using System.Collections;

public class EnemyDamage : MonoBehaviour
{
    public int hitPoints;
    private int _hitPoints;
    //public GameObject ragdoll;

    private void OnEnable()
    {
        _hitPoints = hitPoints;
    }

    public void ApplyDamage(int damage)
    {
        _hitPoints -= damage;
        if (_hitPoints <= 0)
        {
            gameObject.SetActive(false);
            //Instantiate(ragdoll, transform.position, transform.rotation);
        }
    }
}
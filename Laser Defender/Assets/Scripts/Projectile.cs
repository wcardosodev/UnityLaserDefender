using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {
    public float damage = 100f;

    public GameObject explosionEffect;

    public float Damage()
    {
        Destroy(gameObject);
        return damage;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (explosionEffect != null)
        {
            Instantiate(explosionEffect, transform.position, transform.rotation);
        }
    }
}

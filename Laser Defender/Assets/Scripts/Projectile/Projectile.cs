using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {
    public float damage = 100f;
    public float speed = 7.5f;
    public AudioClip fireClip;

    private void Start()
    {
        AudioSource.PlayClipAtPoint(fireClip, transform.position);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer != gameObject.layer && !collision.GetComponent<PowerUp>())
        {
            if(collision.GetComponent<PlayerController>())
            {
                PlayerController player = collision.GetComponent<PlayerController>();
                player.TakeDamage(damage);
            }
            else if(collision.GetComponent<Enemy>())
            {
                Enemy enemy = collision.GetComponent<Enemy>();
                enemy.TakeDamage(damage);
            }//If Player projectile can collide with homing missiles
            else if (gameObject.CompareTag("Player"))
            {
                if(collision.GetComponent<HomingProjectile>() && collision.CompareTag("Enemy"))
                {
                    Destroy(collision.gameObject);
                }
            }
            Destroy(gameObject);
        }
    }
}

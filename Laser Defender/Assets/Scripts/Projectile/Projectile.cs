using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {
    public float damage = 100f;
    public float speed = 7.5f;
    public AudioClip fireClip;

    private void Start()
    {
        AudioSource.PlayClipAtPoint(fireClip, transform.position, .5f);
    }

    private void Update()
    {
        bool player = gameObject.CompareTag("Player");
        if (player)
        {
            GetComponent<Rigidbody2D>().velocity = transform.up * speed;
        }
        else
        {
            GetComponent<Rigidbody2D>().velocity = -transform.up * speed;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer != gameObject.layer && !collision.GetComponent<PowerUp>())
        {
            if (collision.GetComponent<Ships>())
            {
                Ships ship = collision.GetComponent<Ships>();
                ship.TakeDamage(damage);
            }
            //If Player projectile can collide with homing missiles
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

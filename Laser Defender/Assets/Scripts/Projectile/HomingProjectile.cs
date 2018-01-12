using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingProjectile : Projectile {
    Transform target;

    Rigidbody2D rb;

    [SerializeField] float destroyTimer = 5f, rotationSpeed = 200f;

    void Awake()
    {
        StartCoroutine(Destroy(destroyTimer));
        rb = GetComponent<Rigidbody2D>();
        if (gameObject.CompareTag("Enemy"))
        {
            target = FindObjectOfType<PlayerController>().transform;
        }
    }

    private void Update()
    {
        if (gameObject.CompareTag("Player"))
        {
            target = FindClosestTarget(transform.position).transform;
        }
    }

    void FixedUpdate()
    {
        if (target != null)
        {
            Vector2 direction = target.position - transform.position;

            direction.Normalize();

            float rotateAmount; 

            if (gameObject.CompareTag("Player"))
            {
                rb.velocity = transform.up * speed;
                rotateAmount = Vector3.Cross(direction, transform.up).z;
            }
            else
            {
                rb.velocity = -transform.up * speed;
                rotateAmount = Vector3.Cross(direction, -transform.up).z;
            }

            rb.angularVelocity = -rotateAmount * rotationSpeed;
        }
        else
        {
            target = FindClosestTarget(transform.position).transform;
        }
    }

    IEnumerator Destroy(float destroyTime = 0)
    {
        yield return new WaitForSeconds(destroyTime);
        Destroy(gameObject);
    }

    GameObject FindClosestTarget(Vector3 currentPos)
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject closestEnemy = null;

        if (enemies != null)
        {
            foreach (GameObject enemy in enemies)
            {
                if (!enemy.GetComponent<Projectile>())
                {
                    if (closestEnemy == null)
                    {
                        closestEnemy = enemy;
                    }

                    if (Vector2.Distance(transform.position, enemy.transform.position) < Vector2.Distance(transform.position, closestEnemy.transform.position))
                    {
                        closestEnemy = enemy;
                    }
                }
            }
            return closestEnemy;
        }

        StartCoroutine(Destroy());
        return null;
    }
}

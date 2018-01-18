using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBoss : EnemyShips {

    public bool bossActivated = false;
    bool hasMoved = false;
    float moveDirection = -1;

    float moveThreshold = .5f;
    //Change Background music to specific boss music?
    [SerializeField] AudioClip backgroundClip;
    EnemiesSpawner enemiesSpawner;

    enum Phase { Phase01, Phase02, Phase03}

    private void Start()
    {
        MusicPlayer musicPlayer = FindObjectOfType<MusicPlayer>();
        enemiesSpawner = FindObjectOfType<EnemiesSpawner>();

        //if (musicPlayer)
        //{
        //    musicPlayer.SetGameClip(backgroundClip);
        //}

        minPos.x = leftWorldPos.x + padding;
        maxPos.x = rightWorldPos.x - padding;

        GetComponent<PolygonCollider2D>().enabled = false;
    }

    void Update () {
        if (!bossActivated)
        {
            EnterTheArea();
        }
        else
        {
            GetComponent<PolygonCollider2D>().enabled = true;
            StartCoroutine(Attack01(3));
            Move(transform.position.x);
        }

    }

    IEnumerator Attack01(int weaponShots)
    {
        if (canAttack)
        {
            canAttack = false;
            GameObject proj = null;

            for (int i = firepoints.Length - 2; i < firepoints.Length; i++)
            {
                float startRotation = 0;
                if (weaponShots == 3)
                {
                    startRotation = 10f;
                }else if(weaponShots == 5)
                {
                    startRotation = 20f;
                }
                
                for (int j = 0; j < weaponShots; j++)
                {
                    proj = Instantiate(projectiles[0], firepoints[i].position, Quaternion.Euler(0, 0, -startRotation));
                    startRotation += -10f;
                }
            }

            yield return new WaitForSeconds(1.5f);
            canAttack = true;
            StartCoroutine(Attack01(weaponShots));
        }
    }

    IEnumerator Attack02(int amntToFire = 1)
    {
        for (int i = 0; i < amntToFire; i++)
        {
            if (canAttack)
            {
                canAttack = false;
                GameObject proj = null;
                for (int j = firepoints.Length - 2; j < firepoints.Length; j++)
                {
                    proj = Instantiate(projectiles[1], firepoints[j].position, transform.rotation);
                }

                yield return new WaitForSeconds(1.5f);
                canAttack = true;
            }
        }
    }

    void Move(float currentXPos)
    {
        if(currentXPos <= minPos.x + .5)
        {
            moveDirection *= -1f;
        }

        if(currentXPos >= maxPos.x - .5)
        {
            moveDirection *= -1f;
        }

        transform.Translate(moveDirection * Time.deltaTime * speed, 0, 0);
    }

    void EnterTheArea()
    {
        if (transform.position.y >= 2)
        {
            transform.position += Vector3.down * Time.deltaTime * speed;
        }
        else
        {
            bossActivated = true;
        }
    }

    protected override void Die()
    {
        if (deathSFX)
        {
            AudioSource.PlayClipAtPoint(deathSFX, transform.position);
        }
        if (explosionEffect)
        {
            GameObject effect = Instantiate(explosionEffect, transform.position, transform.rotation);
            Destroy(effect, 2);
        }

        scoreKeeper.Score(pointsWorth);

        Destroy(gameObject, .2f);
    }
}

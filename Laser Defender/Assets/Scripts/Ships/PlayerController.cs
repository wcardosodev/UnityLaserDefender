using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Ships {

    public int weapon = 1;

    private LevelManager levelManager;

    public bool translating = false;

    void Start () {
        
        try
        {
            levelManager = FindObjectOfType<LevelManager>();
        }
        catch (System.NullReferenceException)
        {
            print("Missing Level Manager");
        }

        ScoreKeeper.Reset();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            if(weapon == 1)
            {
                StartCoroutine(StandardProjectile(1));
            }
            else if(weapon == 2)
            {
                StartCoroutine(StandardProjectile(5));
            }
            else if(weapon == 3)
            {
                StartCoroutine(HomingRockets());
            }
        }

        if (!translating)
        {
            if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
            {
                transform.position += Vector3.up * Time.smoothDeltaTime * speed;
            }
            else if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
            {
                transform.position += Vector3.down * Time.smoothDeltaTime * speed;
            }

            if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
            {
                transform.position += Vector3.left * Time.smoothDeltaTime * speed;
            }
            else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            {
                transform.position += Vector3.right * Time.smoothDeltaTime * speed;
            }
        }
        else
        {
            float x = Input.GetAxis("Horizontal") * Time.deltaTime * speed;
            float y = Input.GetAxis("Vertical") * Time.deltaTime * speed;

            transform.Translate(x, y, 0);
        }

        Vector2 newPos = new Vector2(Mathf.Clamp(transform.position.x, minPos.x, maxPos.x), Mathf.Clamp(transform.position.y, minPos.y, maxPos.y));

        transform.position = newPos;
    }

    IEnumerator StandardProjectile(int weaponShots = 1)
    {
        if (canAttack)
        {
            canAttack = false;
            GameObject proj = null;

            float startRotation = 0;
            if (weaponShots == 1)
            {
                startRotation = 0;
            }
            else if(weaponShots == 3)
            {
                startRotation = -10f;
            }
            else if (weaponShots == 5)
            {
                startRotation = -20f;
            }

            //float endRotation = -20f;

            //float currentRotation = startRotation;
            for (int i = 0; i < weaponShots; i++)
            {
                proj = Instantiate(projectiles[0], firepoints[0].position, Quaternion.Euler(0, 0, startRotation));

                //startRotation += (weaponShots - 1) * currentRotation;
                startRotation += 10f;
            }

            yield return new WaitForSeconds(timeBetweenShots);
            canAttack = true;
        }
    }

    IEnumerator HomingRockets()
    {
        if (canAttack)
        {
            canAttack = false;
            GameObject proj = null;

            for (int i = firepoints.Length - 2; i < firepoints.Length; i++)
            {
                proj = Instantiate(projectiles[1], firepoints[i].position, transform.rotation);
            }

            yield return new WaitForSeconds(timeBetweenShots);
            canAttack = true;
        }
    }

    protected override void Die()
    {
        AudioSource.PlayClipAtPoint(deathSFX, transform.position);
        if (explosionEffect != null)
        {
            GameObject effect = Instantiate(explosionEffect, transform.position, transform.rotation);
            Destroy(effect, 2);
        }

        levelManager.LoadLevel("Win");
    }

    public float HealthAsPercent
    {
        get { return currentHealth / maxHealth; }
    }
}

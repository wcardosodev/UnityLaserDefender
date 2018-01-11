using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Ships {

    public int weapon = 1;

    private LevelManager levelManager;

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
	
	void Update () {
        ShipControls();
	}

    IEnumerator StandardProjectile(int weaponShots = 1)
    {
        if (canAttack)
        {
            canAttack = false;
            GameObject proj = null;
            for (int i = 0; i < weaponShots; i++)
            {
                proj = Instantiate(projectiles[0], firepoints[i].position, transform.rotation);
                proj.GetComponent<Rigidbody2D>().velocity = Vector2.up * proj.GetComponent<Projectile>().speed;
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

            for(int i = firepoints.Length - 2; i < firepoints.Length; i++)
            {
                proj = Instantiate(projectiles[1], firepoints[i].position, transform.rotation);
            }

            yield return new WaitForSeconds(timeBetweenShots);
            canAttack = true;
        }
    }

    void ShipControls()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            if(weapon == 1)
            {
                StartCoroutine(StandardProjectile(1));
            }
            else if(weapon == 2)
            {
                StartCoroutine(StandardProjectile(3));
            }
            else if(weapon == 3)
            {
                StartCoroutine(HomingRockets());
            }
        }

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

        Vector2 newPos = new Vector2(Mathf.Clamp(transform.position.x, minPos.x, maxPos.x), Mathf.Clamp(transform.position.y, minPos.y, maxPos.y));

        transform.position = newPos;
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

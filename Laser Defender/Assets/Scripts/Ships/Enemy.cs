using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : EnemyShips
{

    [SerializeField] int dropChance = 15, pointsPerDifficultyIncrease = 1500;
    public float projectileDamage = 50f;

    void Start () {

        int i = ScoreKeeper.score / pointsPerDifficultyIncrease;
        Mathf.Ceil(i);
        i *= pointsPerDifficultyIncrease;

        if (i != 0)
        {
            if (i % pointsPerDifficultyIncrease == 0)
            {
                int multiplier = i / pointsPerDifficultyIncrease;
                currentHealth += 50f * multiplier;
                if (minFireRate - minFireRateDeclinePerDiff >= minFireRateCap && maxFireRate - maxFireRateDeclinePerDiff >= maxFireRateCap)
                {
                    minFireRate -= .1f * multiplier;
                    maxFireRate -= .15f * multiplier;
                }
            }
        }
    }
	
	void Update () {
        StartCoroutine(Shoot());
    }

    IEnumerator Shoot()
    {
        if (animationEnded)
        {
            if (canAttack == true)
            {
                print("Shooting");
                canAttack = false;
                GameObject proj = Instantiate(projectiles[0], firepoints[0].position, Quaternion.identity);

                //If you want to increase diffuculty you can increase the enemy damage, which will increase proj damage
                proj.GetComponent<Projectile>().damage = projectileDamage;

                yield return new WaitForSeconds(Random.Range(minFireRate, maxFireRate));
                canAttack = true;
            }
        }
    }

    void DropPickUp()
    {
        if (dropChance > 0)
        {
            int r = Random.Range(1, 100);
            if (r <= dropChance)
            {
                int r2 = Random.Range(0, pickups.Length);
                Instantiate(pickups[r2], transform.position, Quaternion.Euler(new Vector2(0, 0)));
            }
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

        DropPickUp();

        Destroy(gameObject);
    }
}

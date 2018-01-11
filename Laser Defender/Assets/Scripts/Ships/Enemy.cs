using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : EnemyShips {

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
                GameObject proj = Instantiate(projectiles[0], transform.position, Quaternion.identity);
                proj.GetComponent<Rigidbody2D>().velocity = Vector2.down * proj.GetComponent<Projectile>().speed;

                //If you want to increase diffuculty you can increase the enemy damage, which will increase proj damage
                proj.GetComponent<Projectile>().damage = projectileDamage;

                yield return new WaitForSeconds(Random.Range(minFireRate, maxFireRate));
                canAttack = true;
            }
        }
    }
}

using UnityEngine;
using System.Collections;

public class EnemyShips : Ships
{
    protected bool animationEnded;

    [SerializeField] protected float minFireRate = 1f, maxFireRate = 3f;
    [SerializeField] protected float minFireRateDeclinePerDiff = .1f, maxFireRateDeclinePerDiff = .15f;
    [SerializeField] protected float minFireRateCap = .5f, maxFireRateCap = 1f;

    [SerializeField] int pointsWorth = 100, dropChance = 15;
    [SerializeField] protected int pointsPerDifficultyIncrease = 1500;
    [SerializeField] GameObject[] pickups;

    protected override void Die()
    {
        AudioSource.PlayClipAtPoint(deathSFX, transform.position);
        if (explosionEffect != null)
        {
            GameObject effect = Instantiate(explosionEffect, transform.position, transform.rotation);
            Destroy(effect, 2);
        }

        scoreKeeper.Score(pointsWorth);

        DropPickUp();

        Destroy(gameObject);
    }

    void DropPickUp()
    {
        int r = Random.Range(1, 100);
        if (r <= dropChance)
        {
            int r2 = Random.Range(0, pickups.Length);
            Instantiate(pickups[r2], transform.position, Quaternion.Euler(new Vector2(0, 0)));
        }
    }

    public void AnimationEnded()
    {
        animationEnded = true;
    }
}

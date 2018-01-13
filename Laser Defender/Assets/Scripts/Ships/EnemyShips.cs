using UnityEngine;
using System.Collections;

public class EnemyShips : Ships
{
    protected bool animationEnded;

    [SerializeField] protected float minFireRate = 1f, maxFireRate = 3f;
    [SerializeField] protected float minFireRateDeclinePerDiff = .1f, maxFireRateDeclinePerDiff = .15f;
    [SerializeField] protected float minFireRateCap = .5f, maxFireRateCap = 1f;

    [SerializeField] protected int pointsWorth = 100;
    [SerializeField] protected GameObject[] pickups;

    public void AnimationEnded()
    {
        animationEnded = true;
    }

    protected override void Die()
    {
    }
}

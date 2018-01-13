using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ships : MonoBehaviour {

    [SerializeField] protected float maxHealth;
    protected float currentHealth;
    public GameObject[] projectiles;
    [SerializeField] protected Transform[] firepoints;

    [SerializeField] protected float speed = 10, timeBetweenShots = 1f;

    [SerializeField] protected Vector2 minPos, maxPos;

    [SerializeField] protected GameObject explosionEffect;
    [SerializeField] protected AudioClip hitSFX, deathSFX;

    [SerializeField] Sprite[] animationSprites;

    protected ScoreKeeper scoreKeeper;

    protected bool canAttack = true;

    private void Awake()
    {
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        SetToMaxHealth();
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    protected IEnumerator PlayIdleAnimation(float time)
    {
        for (int i = 0; i < animationSprites.Length; i++)
        {
            GetComponent<SpriteRenderer>().sprite = animationSprites[i];
            yield return new WaitForSeconds(time);
        }
    }

    protected abstract void Die();

    public void SetToMaxHealth()
    {
        currentHealth = maxHealth;
    }
}

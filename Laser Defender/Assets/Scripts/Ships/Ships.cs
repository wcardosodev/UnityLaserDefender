using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ships : MonoBehaviour {

    [SerializeField] protected float maxHealth;
    protected float currentHealth;
    public GameObject[] projectiles;
    [SerializeField] protected Transform[] firepoints;

    [SerializeField] protected float speed = 10, timeBetweenShots = 1f;

    protected Vector2 minPos, maxPos;
    protected Vector3 leftWorldPos, rightWorldPos;

    [SerializeField] protected GameObject explosionEffect, damagedEffect;
    [SerializeField] protected AudioClip hitSFX, deathSFX;
    GameObject damaged = null;

    [SerializeField] protected float padding;

    [SerializeField] Sprite[] animationSprites;

    protected ScoreKeeper scoreKeeper;

    protected bool canAttack = true;

    private void Awake()
    {
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        SetToMaxHealth();

        float distance = transform.position.z - Camera.main.transform.position.z;
        leftWorldPos = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance));
        rightWorldPos = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distance));
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (HealthAsPercent <= .5)
        {
            if (GetComponent<PlayerController>())
            {
                damaged = Instantiate(damagedEffect, transform.position, Quaternion.Euler(new Vector3(0,0,180)));
            }
            else
            {
                damaged = Instantiate(damagedEffect, transform.position, Quaternion.identity);
            }
            damaged.transform.parent = gameObject.transform;
        }
        if (currentHealth <= 0)
        {
            Destroy(damaged);
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
        Destroy(damaged);
        currentHealth = maxHealth;
    }

    public float HealthAsPercent
    {
        get { return currentHealth / maxHealth; }
    }
}

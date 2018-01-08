using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
    public bool animationEnded;
    public float health = 50;
    public float projectileSpeed = 1.5f;
    public int pointsWorth = 150;

    public AudioClip fireSFX;
    public AudioClip dieSFX;
    public GameObject proj;

    AudioSource audi;
    ScoreKeeper score;
    bool canAttack = true;
    float minFireRate = 1f;
    float maxFireRate = 3f;
    float shotsPerSeconds = 0.5f;

    public int pointsPerDifficultyIncrease = 750;
    
    // Use this for initialization
    void Start () {
        audi = GetComponent<AudioSource>();
        score = GameObject.Find("Score").GetComponent<ScoreKeeper>();


        int i = ScoreKeeper.score / pointsPerDifficultyIncrease;
        Mathf.Ceil(i);
        i *= pointsPerDifficultyIncrease;

        if (i != 0)
        {
            if (i % pointsPerDifficultyIncrease == 0)
            {
                int multiplier = i / pointsPerDifficultyIncrease;
                health += 50f * multiplier;
                if (minFireRate - .1f >= .5f && maxFireRate - .15f >= 1f)
                {
                    minFireRate -= .1f * multiplier;
                    maxFireRate -= .15f * multiplier;
                }
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
        StartCoroutine(Shoot());

    }

    void RandomProbability()
    {
        //prob
        float probability = Time.smoothDeltaTime * shotsPerSeconds;
        //Random & 1.0 Probability = between 0.0 -> 1.0 
        if (Random.value < probability)
        {
            //do dis
        }
    }

    IEnumerator Shoot()
    {
        if (animationEnded)
        {
            if (canAttack == true)
            {
                canAttack = false;
                GameObject laser = Instantiate(proj, transform.position, Quaternion.identity) as GameObject;
                laser.GetComponent<Rigidbody2D>().velocity = new Vector3(0f, -projectileSpeed, 0f);
                AudioSource.PlayClipAtPoint(dieSFX, transform.position);
                yield return new WaitForSeconds(Random.Range(minFireRate, maxFireRate));
                canAttack = true;
            }
        }
    }

    void Die()
    {
        AudioSource.PlayClipAtPoint(dieSFX, transform.position);
        score.Score(pointsWorth);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Projectile proj = collision.gameObject.GetComponent<Projectile>();
        //Basically, if "collision" contains Projectile Component
        if (proj == true)
        {
            health -= proj.Damage();
            if (health <= 0)
            {
                Die();   
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public GameObject projectile;
    public AudioClip fireSFX;
    public AudioClip dieSFX;
    public AudioClip hitSFX;

    public float health = 350;
    public float Speed = 10.0f;
    public float Padding = 0.5f;
    public float ProjectileSpeed;
    public float timeBetweenShots;

    float xMin;
    float xMax;
    bool canAttack = true;
    private Rigidbody2D rb2d;
    private ScoreKeeper score;
    private AudioSource audi;
    private LevelManager level;

	// Use this for initialization
	void Start () {
        score = GameObject.Find("Score").GetComponent<ScoreKeeper>();
        rb2d = GetComponent<Rigidbody2D>();
        audi = GetComponent<AudioSource>();
        level = GameObject.Find("Level Manager").GetComponent<LevelManager>();
        score = GameObject.Find("Score").GetComponent<ScoreKeeper>();

        CameraSettings();

        ScoreKeeper.Reset();
    }
	
	// Update is called once per frame
	void Update () {
        ShipControls();
	}

    IEnumerator Shoot()
    {
        //Fire Laser
        //Instantiate returns object, use as to "convert to" GameObject
        if (canAttack)
        {
            canAttack = false;
            GameObject beam = Instantiate(projectile, transform.position, Quaternion.identity) as GameObject;
            beam.GetComponent<Rigidbody2D>().velocity = new Vector3(0f, ProjectileSpeed);
            AudioSource.PlayClipAtPoint(fireSFX, transform.position);
            yield return new WaitForSeconds(timeBetweenShots);
            canAttack = true;
        }
    }

    void ShipControls()
    {
        if (Input.GetKey(KeyCode.Space))
        {           
            StartCoroutine(Shoot());  
        }

        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            transform.position += new Vector3(-Speed * Time.smoothDeltaTime, 0f, 0f);
            //transform.position += Vector3.left * (Speed * Time.smoothDeltaTime);
        }
        else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            transform.position += new Vector3(Speed * Time.smoothDeltaTime, 0f, 0f);
        }

        //Restrict player to gamespace
        float newX = Mathf.Clamp(transform.position.x, xMin, xMax);
        transform.position = new Vector3(newX, transform.position.y, transform.position.z);
    }

    void Die()
    {
        AudioSource.PlayClipAtPoint(dieSFX, transform.position);
        Destroy(gameObject);
        level.LoadLevel("Win");
    }

    void CameraSettings()
    {
        float distance = transform.position.z - Camera.main.transform.position.z;
        Vector3 leftWorldPos = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance));
        Vector3 rightWorldPos = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distance));

        xMin = leftWorldPos.x + Padding;
        xMax = rightWorldPos.x - Padding;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Projectile proj = collision.gameObject.GetComponent<Projectile>();
        if (proj != null)
        {
            AudioSource.PlayClipAtPoint(dieSFX, transform.position);
            health -= proj.Damage();
            if (health <= 0)
            {               
                Die();
            }
        }
    }
}

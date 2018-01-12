using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBoss : EnemyShips {

	void Start () {
        StartCoroutine(Attack01());
        StartCoroutine(Attack02());
	}

    //Change Background music to specific boss music?
	
	// Update is called once per frame
	void Update () {

        
		//Move From Left to Right, Up and down a bit? Within the boundaries
	}

    IEnumerator Attack01()
    {
        GameObject proj = null;
        for(int i = 0; i < 1; i++)
        {
            proj = Instantiate(projectiles[0], firepoints[i].position, transform.rotation);
        }

        yield return new WaitForSeconds(1.5f);

        StartCoroutine(Attack01());
    }

    IEnumerator Attack02()
    {
        GameObject proj = null;
        for (int i = 1; i < 3; i++)
        {
            proj = Instantiate(projectiles[1], firepoints[i].position, transform.rotation);
        }

        yield return new WaitForSeconds(1.5f);

        StartCoroutine(Attack02());
    }

    Vector2 Velo(GameObject proj, Transform firepoint, int i = 0)
    {
        Vector2 dir;

        if(i == 0)
        {
            return new Vector2(0, -1) * proj.GetComponent<Projectile>().speed;
        }
        else if(i == 1)
        {
            dir = new Vector2(firepoint.position.x + .1f, firepoint.position.y - 1f);
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;
            float spread = Random.Range(-10, 10);
            Quaternion bulletRotation = Quaternion.Euler(new Vector3(0, 0, angle + spread));
            return new Vector2(.5f, -1) * proj.GetComponent<Projectile>().speed;
        }
        else if(i == 2)
        {
            dir = new Vector2(firepoint.position.x - .1f, firepoint.position.y - 1f);
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;
            float spread = Random.Range(-10, 10);
            Quaternion bulletRotation = Quaternion.Euler(new Vector3(0, 0, angle + spread));
            return new Vector2(-.5f, -1) * proj.GetComponent<Projectile>().speed;
        }

        return Vector2.down;
    }
}

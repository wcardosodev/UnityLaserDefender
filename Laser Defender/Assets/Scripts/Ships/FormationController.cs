using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FormationController : MonoBehaviour {

    [SerializeField] GameObject enemyPrefab;

    [SerializeField] float width = 8.5f, height = 7f, padding = 2f, speed = 2f, spawnDelay = 0.75f;

    float distance, minX, maxX;
    bool moveRight = true;
    bool spawn = true;
    

    // Use this for initialization
    void Start () {
        distance = transform.position.z - Camera.main.transform.position.z;
        Vector3 leftWorldPos = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance));
        Vector3 rightWorldPos = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distance));

        minX = leftWorldPos.x;
        maxX = rightWorldPos.x;
    }
	
	// Update is called once per frame
	void Update () {
        MoveShips();

        if (AllMembersDead() && !FindObjectOfType<EnemyBoss>())
        {
            SpawnUntilFull();
        }

        //NextFreePosition();
	}

    void SpawnEnemies()
    {
        foreach (Transform child in transform)
        {     
            GameObject enemy = Instantiate(enemyPrefab, child.transform.position, Quaternion.identity);
            enemy.transform.parent = child;
        }
    }

    void SpawnUntilFull()
    {
        Transform freePosition = NextFreePosition();
        if(freePosition != null)
        {
            GameObject enemy = Instantiate(enemyPrefab, freePosition.position, Quaternion.identity);
            enemy.transform.parent = freePosition;
            Invoke("SpawnUntilFull", spawnDelay);
        }
    }

    public void SetSpawning(bool spawnEnemy)
    {
        spawn = spawnEnemy;
    }

    Transform NextFreePosition()
    {
        foreach(Transform child in transform)
        {
            if(child.childCount == 0)
            {
                return child;
            }
        }
        return null;
    }

    public bool AllMembersDead()
    {
        foreach(Transform child in transform)
        {
            if(child.childCount > 0)
            {
                return false;
            }
        }
        return true;
    }

    void MoveShips()
    {
        float rightEdgeOfFormation = transform.position.x + (0.5f * width);
        float leftEdgeOfFormation = transform.position.x - (0.5f * width);
        if (moveRight)
        {
            transform.position += Vector3.right * (speed * Time.smoothDeltaTime);
        }
        else
        {
            transform.position += Vector3.left * (speed * Time.smoothDeltaTime);
        }

        if (rightEdgeOfFormation >= maxX)
        {
            moveRight = false;
        }
        else if(leftEdgeOfFormation <= minX)
        {
            moveRight = true;
        }

        //float newX = Mathf.Clamp(transform.position.x, minX, maxX);
        //transform.position = new Vector3(newX, transform.position.y, transform.position.z);
    }

    public void SetEnemy(GameObject enemyToSet)
    {
        enemyPrefab = enemyToSet;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(width, height));
    }
}

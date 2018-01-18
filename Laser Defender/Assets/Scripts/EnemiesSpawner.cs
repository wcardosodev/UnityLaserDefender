using UnityEngine;
using System.Collections;

public class EnemiesSpawner : MonoBehaviour
{
    [SerializeField] FormationController enemyFormation;
    [SerializeField] EnemyBoss[] bosses;
    int bossCount = 0;

    int spawnEveryPoints = 7500;
    bool hasSpawned = false;

    private void Start()
    {
        Instantiate(enemyFormation);
    }

    private void Update()
    {
        if (enemyFormation.AllMembersDead())
        {
            //ScoreKeeper.score % spawnEveryPoints == 0 && ScoreKeeper.score != 0)
            if (ScoreKeeper.score % spawnEveryPoints == 0 && ScoreKeeper.score != 0)
            {
                if (bosses[bossCount].bossActivated == false && !FindObjectOfType<EnemyBoss>())
                {
                    Instantiate(bosses[bossCount]);
                    bossCount++;
                    print(bossCount);
                }
            }
        }
    }
}

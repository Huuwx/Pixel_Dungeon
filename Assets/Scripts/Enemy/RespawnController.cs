using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class RespawnController : MonoBehaviour
{
    [SerializeField] List<GameObject> enemies;

    public float timeToRespawn = 3f;

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            if (!enemies[i].activeSelf)
            {
                StartCoroutine(Respawn(enemies[i], timeToRespawn));
            }
        }
    }

    IEnumerator Respawn(GameObject enemy, float timeForThisEnemy)
    {
        EnemyController enemyController = enemy.GetComponent<EnemyController>();
        EnemyHealthController enemyHealthController = enemy.GetComponent<EnemyHealthController>();

        // Chờ thời gian hồi sinh
        while (timeForThisEnemy > 0)
        {
            yield return new WaitForSeconds(1f);
            timeForThisEnemy--;
        }

        // Sau khi thời gian hồi sinh hết, kích hoạt lại enemy
        enemy.transform.position = enemyController.homePos;
        enemyController.SetFalseIsDead();
        enemyHealthController.ResetHealth();
        enemy.SetActive(true);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class LevelController : MonoBehaviour
{
    public List<SpawnPointInfo> spawnPoints;

    List<Enemy> aliveEnemies;
    float timeSpent = 0;

    public void Start() {
        aliveEnemies = new List<Enemy>();
        Dictionary<GameObject, int> enemies = GameController.i.PopEnemies(SceneManager.GetActiveScene().name);
        while (enemies.Count > 0) {
            List<GameObject> keys = enemies.Keys.ToList();
            GameObject enemy = keys[Random.Range(0, keys.Count)];
            enemies[enemy] -= 1;
            if (enemies[enemy] <= 0) enemies.Remove(enemy);
            SpawnPointInfo spawnPointInfo = spawnPoints[Random.Range(0, spawnPoints.Count)];
            Vector2 circle = Random.insideUnitCircle * spawnPointInfo.radius;
            Vector3 spawnVector = spawnPointInfo.transform.position + new Vector3(circle.x, 0, circle.y);
            Enemy spawnedEnemy = Instantiate(enemy, spawnVector, spawnPointInfo.transform.rotation).GetComponent<Enemy>();
            spawnedEnemy.prefab = enemy;
            aliveEnemies.Add(spawnedEnemy);
        }
    }

    public void Update() {
        if (aliveEnemies.All(enemy => enemy == null)) {
            GameController.i.OnLevelClear(SceneManager.GetActiveScene().name, timeSpent);
            GameController.i.LoadDeployScene();
        }
        timeSpent += Time.deltaTime;
    }

    public void OnPlayerDie() {
        GameController.i.OnLevelFailed(SceneManager.GetActiveScene().name, timeSpent, aliveEnemies);
        GameController.i.LoadDeployScene();
    }
}

[Serializable]
public struct SpawnPointInfo {
    public Transform transform;
    public float radius;
}

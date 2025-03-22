using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController i;
    public List<LevelInfo> levels;

    public void Start() {
        if (i) { Destroy(gameObject); return; }
        i = gameObject.GetComponent<GameController>();
        DontDestroyOnLoad(gameObject);
    }

    public Dictionary<Enemy, int> PopEnemies(string sceneName) {
        LevelInfo li = levels.Find(x => x.scene.name == sceneName);
        Dictionary<Enemy, int> enemies = li.enemies;
        li.enemies.Clear();
        return enemies;
    }

    public void AddEnemyInLevel(Enemy enemy, string sceneName) {
        Dictionary<Enemy, int> enemies = levels.Find(x => x.scene.name == sceneName).enemies;
        if (enemies.ContainsKey(enemy)) enemies[enemy] += 1;
        else enemies[enemy] = 1;
    }
}

[Serializable]
public class LevelInfo {
    public SceneAsset scene;
    public Dictionary<Enemy, int> enemies;
}

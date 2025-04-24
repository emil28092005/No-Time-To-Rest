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

    public Dictionary<GameObject, int> PopEnemies(string sceneName) {
        LevelInfo li = levels.Find(x => x.scene.name == sceneName);
        Dictionary<GameObject, int> enemies = li.enemies;
        li.enemies.Clear();
        return enemies;
    }

    public void AddEnemyInLevel(GameObject enemy, string sceneName) {
        Dictionary<GameObject, int> enemies = levels.Find(x => x.scene.name == sceneName).enemies;
        if (enemies.ContainsKey(enemy)) enemies[enemy] += 1;
        else enemies[enemy] = 1;
    }

    public LevelInfo GetLevel(string sceneName) {
        return levels.Find(x => x.scene.name == sceneName);
    }

    public void OnLevelClear(string sceneName, float timeSpent) {

    }
}

[Serializable]
public struct LevelInfo {
    public SceneAsset scene;
    public int hp;
    public int maxHp;
    public FactoryType factory;
    public Dictionary<GameObject, int> enemies;
}

public enum FactoryType {
    None,
    Rifle,
    Rocket,
    Minigun,
    Railgun
}

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameController : MonoBehaviour
{
    public static GameController i;
    public List<LevelInfo> levels;
    public List<EnemyInfo> enemies;

    public void Start() {
        if (i) { Destroy(gameObject); return; }
        i = GetComponent<GameController>();
        DontDestroyOnLoad(gameObject);
        levels = levels.Select(li => {
            li.hp = li.maxHp;
            li.enemies = new();
            return li;
        }).ToList();
        levels.ForEach(li => {
            AddEnemyInLevel(SelectEnemyWeighted().prefab, li.scene.name);
        });

    }

    public Dictionary<GameObject, int> PopEnemies(string sceneName) {
        LevelInfo li = levels.Find(x => x.scene.name == sceneName);
        Dictionary<GameObject, int> levelEnemies = new(li.enemies);
        li.enemies.Clear();
        return levelEnemies;
    }

    public void AddEnemyInLevel(GameObject enemy, string sceneName) {
        Dictionary<GameObject, int> levelEnemies = levels.Find(x => x.scene.name == sceneName).enemies;
        if (levelEnemies.ContainsKey(enemy)) levelEnemies[enemy] += 1;
        else levelEnemies[enemy] = 1;
    }

    public LevelInfo GetLevel(string sceneName) {
        return levels.Find(x => x.scene.name == sceneName);
    }

    public void OnLevelClear(string sceneName, float timeSpent) {

    }

    EnemyInfo SelectEnemyWeighted() {
        float sumWeights = enemies.Sum(enemy => enemy.weight);
        float r = Random.Range(0, sumWeights);
        float curSum = 0;
        foreach (EnemyInfo en in enemies) {
            curSum += en.weight;
            if (curSum >= r) return en;
        }
        return enemies[^1];
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

[Serializable]
public struct EnemyInfo {
    public GameObject prefab;
    public float weight;
}

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class GameController : MonoBehaviour
{
    public static GameController i;
    public string deploySceneName;
    public List<LevelInfo> levels;
    public List<EnemyInfo> enemies;
    public Dictionary<BulletType, int> inventory = new();

    public void Awake() {
        if (i) { Destroy(gameObject); return; }
        i = GetComponent<GameController>();
        DontDestroyOnLoad(gameObject);
        levels = levels.Select(li => {
            li.hp = li.maxHp;
            li.enemies = new();
            return li;
        }).ToList();
        levels.ForEach(li => {
            AddEnemyInLevel(SelectEnemyWeighted().prefab, li.sceneName);
        });
        foreach (BulletType i in Enum.GetValues(typeof(BulletType))) {
            if (i == BulletType.None) continue;
            inventory[i] = 10;
        }
    }

    public Dictionary<GameObject, int> PopEnemies(string sceneName) {
        LevelInfo li = levels.Find(x => x.sceneName == sceneName);
        Dictionary<GameObject, int> levelEnemies = new(li.enemies);
        li.enemies.Clear();
        return levelEnemies;
    }

    public void AddEnemyInLevel(GameObject enemy, string sceneName) {
        Dictionary<GameObject, int> levelEnemies = levels.Find(x => x.sceneName == sceneName).enemies;
        if (levelEnemies.ContainsKey(enemy)) levelEnemies[enemy] += 1;
        else levelEnemies[enemy] = 1;
    }

    public LevelInfo GetLevel(string sceneName) {
        return levels.Find(x => x.sceneName == sceneName);
    }

    void RepairLevel(string sceneName) {
        levels = levels.Select(li => {
            if (li.sceneName == sceneName) {
                li.hp = Math.Clamp(li.hp + 20, 0, li.maxHp);
            }
            return li;
        }).ToList();
    }

    void DamageLevels(string sceneName, float timeSpent) {
        levels = levels.Select(li => {
            if (li.sceneName != sceneName && !IsLevelDestroyed(li.sceneName)) {
                int damage = (int)(li.enemies.Values.Sum(x => x) * timeSpent / 10);
                li.hp = Math.Clamp(li.hp - damage, 0, li.maxHp);
            }
            return li;
        }).ToList();
    }

    void SpawnEnemies(string sceneName, float timeSpent) {
        int enemiesToAdd = (int)(timeSpent / 10) + 1;
        levels.ForEach(li => {
            if (li.sceneName == sceneName || IsLevelDestroyed(li.sceneName)) return;
            for (int i = 0; i < enemiesToAdd; ++i) AddEnemyInLevel(SelectEnemyWeighted().prefab, li.sceneName);
        });
    }

    void AddBullets() {
        levels.ForEach(li => {
            if (!IsLevelDestroyed(li.sceneName) && li.factory != BulletType.None) inventory[li.factory] += li.bulletsGenerate;
        });
    }

    public void OnLevelClear(string sceneName, float timeSpent) {
        RepairLevel(sceneName);
        DamageLevels(sceneName, timeSpent);
        SpawnEnemies(sceneName, timeSpent);
        AddBullets();
    }

    public void OnLevelFailed(string sceneName, float timeSpent, List<Enemy> aliveEnemies) {
        aliveEnemies.ForEach(enemy => AddEnemyInLevel(enemy.prefab, sceneName));
        DamageLevels(sceneName, timeSpent);
        SpawnEnemies(sceneName, timeSpent * 1.5f);
        AddBullets();
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

    public void LoadDeployScene() {
        SceneManager.LoadScene(deploySceneName);
    }

    public static void SetCursorState(bool locked) {
        Cursor.lockState = locked ? CursorLockMode.Locked : CursorLockMode.None;
        Cursor.visible = !locked;
    }

    public int GetBulletCount(BulletType bulletType) {
        return inventory[bulletType];
    }

    public bool ConsumeBullet(BulletType bulletType, int n = 1) {
        if (inventory[bulletType] >= n) {
            inventory[bulletType] -= n;
            return true;
        }
        return false;
    }

    public static string GetBulletTypeString(BulletType bulletType) {
        return bulletType switch {
            BulletType.Rifle => "Rifle",
            BulletType.Minigun => "Minigun",
            BulletType.Rocket => "Rocket",
            BulletType.Railgun => "Railgun",
            _ => "None",
        };
    }

    public bool IsLevelDestroyed(string sceneName) {
        return GetLevel(sceneName).hp <= 0;
    }
}

[Serializable]
public struct LevelInfo {
    public string sceneName;
    public int hp;
    public int maxHp;
    public BulletType factory;
    public int bulletsGenerate;
    public Dictionary<GameObject, int> enemies;
}

public enum BulletType {
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

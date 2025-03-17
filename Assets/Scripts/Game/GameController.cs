using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public List<LevelInfo> levels;
}

[Serializable]
public class LevelInfo {
    public SceneAsset scene;
    public List<EnemyInLevel> enemies;
}

[Serializable]
public class EnemyInLevel {
    public GameObject enemy;
    public int count;
}

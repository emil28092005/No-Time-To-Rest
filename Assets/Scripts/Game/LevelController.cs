using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    public List<Transform> spawnPoints;

    public void Start() {
        Dictionary<GameObject, int> enemies = GameController.i.PopEnemies(SceneManager.GetActiveScene().name);
    }
}

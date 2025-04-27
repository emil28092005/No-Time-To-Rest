using System.Linq;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeployController : MonoBehaviour
{
    public void Start() {
        GameController.SetCursorState(false);
    }

    public void Deploy(string sceneName) {
        LevelInfo level = GameController.i.GetLevel(sceneName);
        SceneManager.LoadScene(level.scene.name);
    }

    public void SetLevelInfoText(TMP_Text textField, SceneAsset level) {
        LevelInfo levelInfo = GameController.i.GetLevel(level.name);
        string s = $"HP: {levelInfo.hp}/{levelInfo.maxHp}\nEnemies: {levelInfo.enemies.Values.Sum(x => x)}\nBullet type: {GameController.GetBulletTypeString(levelInfo.factory)}";
    }
}

using System.Linq;
using TMPro;
using UnityEditor;
using UnityEngine;

public class DeployButton : MonoBehaviour
{
    public TMP_Text textField;
    public SceneAsset level;

    DeployController dc;

    void Start() {
        dc = FindFirstObjectByType<DeployController>();
    }

    public void DeployFromButton() {
        dc.Deploy(level.name);
    }

    public void SetLevelInfoText() {
        LevelInfo levelInfo = GameController.i.GetLevel(level.name);
        string s = $"HP: {levelInfo.hp}/{levelInfo.maxHp}\nEnemies: {levelInfo.enemies.Values.Sum(x => x)}\nBullet type: {GameController.GetBulletTypeString(levelInfo.factory)}";
        textField.text = s;
    }
}

using System.Linq;
using TMPro;
using UnityEngine;

public class DeployButton : MonoBehaviour
{
    public TMP_Text textField;
    public string levelName;

    DeployController dc;

    void Start() {
        dc = FindFirstObjectByType<DeployController>();
    }

    public void DeployFromButton() {
        dc.Deploy(levelName);
    }

    public void SetLevelInfoText() {
        LevelInfo levelInfo = GameController.i.GetLevel(levelName);
        string s = $"HP: {levelInfo.hp}/{levelInfo.maxHp}\nEnemies: {levelInfo.enemies.Values.Sum(x => x)}\nBullet type: {GameController.GetBulletTypeString(levelInfo.factory)}";
        textField.text = s;
    }
}

using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeployButton : MonoBehaviour
{
    public TMP_Text textField;
    public string levelName;

    DeployController dc;
    Button button;

    void Start() {
        dc = FindFirstObjectByType<DeployController>();
        button = GetComponent<Button>();
        if (GameController.i.IsLevelDestroyed(levelName)) button.interactable = false;
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

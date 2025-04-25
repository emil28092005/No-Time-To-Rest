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
}

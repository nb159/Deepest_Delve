using UnityEngine;

public class MenuButtonController : MonoBehaviour
{
    public void StartGame()
    {
        GameManager.instance.ChangeScene(GameScene.InGameScene);
    }

    public void ContinueGame()
    {
        //TODO: save player & items into JSON
        GameManager.instance.ContinueGame(GameScene.InGameScene);
    }

    public void RestartGame()
    {
        GameManager.instance.RestartGame(GameScene.InGameScene);
    }

    public void Settings()
    {
        //GameManager.instance.ChangeScene(GameScene.SettingsScene);
    }

    public void QuitGame()
    {
        // Quitting the game
        Application.Quit();
    }
}
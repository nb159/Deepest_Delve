using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MainMenuManager : MonoBehaviour
{
    public void StartGame()
    {
        GameManager.instance.ChangeScene(GameManager.GameScene.InGameScene);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalManager : MonoBehaviour
{

    public static PortalManager instance;

    private void Awake()
    {
        gameObject.SetActive(false);

        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //switch game Scene
            GameManager.instance.ChangeScene(GameScene.MainMenuScene);  
        }
    }

    public void togglePortal(bool state)
    {
        //TODO: SFX TOGGLEABLE 
        //maybe make it fade instead of setting active
        gameObject.SetActive(state);
        PlayerLocomotion.instance.toggleTargetToLockOn(state);
        PlayerCamera.instance.toggleTargetToLockOn(state);
        
    }
}

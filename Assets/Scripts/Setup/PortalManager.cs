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




    private void OnTriggerEnter(Collider hitInfo)
    {
        if (hitInfo.CompareTag("Boss") || hitInfo.CompareTag("highRangeProjectile") || hitInfo.CompareTag("lowRangeProjectile") || hitInfo.CompareTag("onPotionProjectile"))
        {
            return;
        }

        if (hitInfo.CompareTag("Player"))
        {
            //switch game Scene
            //  GameManager.instance.ChangeScene(GameScene.MainMenuScene);
            GameManager.instance.BossDefeated();
            Debug.Log("hy portal");
            ItemsManager.userSelected = false;
        }
        Destroy(gameObject);
    }


    public void togglePortal(bool state)
    {
        //TODO: SFX TOGGLEABLE 
        //maybe make it fade instead of setting active
        gameObject.SetActive(state);
        PlayerLocomotion.instance.toggleTargetToLockOn(state);
        PlayerCamera.instance.toggleTargetToLockOn(state);
        ItemsManager.instance.togglItemsSelector(state);
        ItemsManager.instance.randomizeItems();


    }
}

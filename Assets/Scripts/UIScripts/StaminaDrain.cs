using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class StaminaDrain : MonoBehaviour
{
    [SerializeField]
    private Image stamBar;
    [SerializeField]
    private int time;
    [SerializeField]
    private float targetScale;
    // private Vector3 _originalScale;
    // private Vector3 _targetScale;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        scaleStamina(1,1);
    }

    void scaleStamina(float targetScale,int time)
    {
        //_originalScale = stamBar.transform.localScale;
        targetScale = GameManager.instance.playerStamina / 100;
        time = 1;
        transform.DOScale(targetScale, time).SetEase(Ease.Linear);
    }
}

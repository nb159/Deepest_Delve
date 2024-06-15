using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class UIScript : MonoBehaviour
{
    [SerializeField]
    private Image button;
    [SerializeField]
    private int time;
    [SerializeField]
    private float targetOppacity;
    // Start is called before the first frame update
    void Start()
    {
        //button.color = new Color(1f, 1f, 1f, 0.9f);

    }

    // Update is called once per frame
    void Update()
    {
        fadeBar(1f, 2);
    }

    void fadeBar(float targetOppacity, int time)
    {
        // targetOppacity = 0.2f;
        if (PlayerAnimatorManager.instance.isDashing != true)
        { targetOppacity = 0.1f; time = 15; }
        button.DOFade(targetOppacity, time).SetEase(Ease.OutBack);
    }

}

using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class ItemDisplaySelector : MonoBehaviour
{
    public PowerUp AssignedItem;
    
    public UnityEngine.UI.Image itemDisplay; 

    
    public float speed = 1f; 
    public float height = 0.5f;

    private Vector3 startPos;     
    private float timeOffset; 
    private GameObject player;



    void Start()
    {
        startPos = transform.position; // Save the starting position
        timeOffset = Random.Range(0f, 2f * Mathf.PI); // Add this line
        player = GameObject.FindGameObjectWithTag("Player");

    }

    void Update(){
        itemDisplay.sprite = AssignedItem.itemImage;

        transform.position = startPos + new Vector3(0, Mathf.Sin((Time.time + timeOffset) * speed) * height, 0);

        

    }
    void OnCollisionEnter(Collision other)
    {
        if (!other.gameObject.CompareTag("Player")) return;
        if (ItemsManager.userSelected) return;
        
        AssignedItem.ApplyStats();
        GameManager.instance.playerSelectedBuffs.Add(AssignedItem);
        ItemsManager.userSelected = true;
        Destroy(gameObject);
    }
}

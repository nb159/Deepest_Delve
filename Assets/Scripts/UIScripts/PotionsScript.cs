using System;
using UnityEngine;
using UnityEngine.UI;

namespace UIScripts
{
    public class PotionsScript : MonoBehaviour
    {
        public static PotionsScript instance;

        [SerializeField] private Image[] image;

        [SerializeField] private bool drinkPotion;

        [SerializeField] private int _potions;
        // public Image PotionBarViz;
        // public int potions;
        //public float PotionsFloat;


        private void Awake()
        {
            if (instance != null && instance != this)
                Destroy(gameObject);
            else
                instance = this;
        }

        private void Start()
        {
            // TODO:  Potions = GameManager.instance.playerPotions;
            // Debug.Log(Potions);
            _potions = 3;
            // PotionsFloat = (float) Potions;
            //Debug.Log(PotionsFloat);
            //PotionBarViz.fillAmount = PotionsFloat;
        }

        private void Update()
        {
            if (_potions > 0)
                if (Input.GetKeyDown(KeyCode.Q))
                {
                    drinkPotion = true;
                    if (drinkPotion)
                    {
                        OnPotionDrink();
                        drinkPotion = false; // Reset drinkPotion to false after drinking
                    }
                }
        }

        public void OnPotionDrink()
        {
            if (_potions > 0)
            {
                _potions--;

                if (image.Length > 0)
                {
                    var color = image[^1].color;
                    color.a = 100f / 255f; // Alpha value is between 0 and 1, so we divide by 255
                    image[^1].color = color;
                    //image[^1].gameObject.SetActive(false); // Deactivate the last image object
                    var newImageArray = new Image[image.Length - 1];
                    Array.Copy(image, newImageArray, newImageArray.Length);
                    image = newImageArray;
                }
            }
        }
    }
}
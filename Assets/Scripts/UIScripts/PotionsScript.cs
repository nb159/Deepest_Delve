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

        private int _potions;
        // public Image PotionBarViz;
        // public int potions;
        //public float PotionsFloat;


                private void Awake()
                {
                    if (instance != null && instance != this)
                    {
                        Destroy(this.gameObject);
                    }
                    else
                    {
                        instance = this;
                    }
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
            if (drinkPotion)
            {
                OnPotionDrink();
                drinkPotion = false; // Reset drinkPotion to false after drinking
            }
        }

        public void OnPotionDrink()
        {
            if (_potions > 0)
            {
                _potions--;

                if (image.Length > 0)
                {
                    image[^1].gameObject.SetActive(false); // Deactivate the last image object
                    var newImageArray = new Image[image.Length - 1];
                    Array.Copy(image, newImageArray, newImageArray.Length);
                    image = newImageArray;
                }
            }
        }
    }
}
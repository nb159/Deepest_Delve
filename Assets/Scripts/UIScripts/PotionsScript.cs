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
            _potions = GameManager.instance.playerPotions;
            // PotionsFloat = (float) Potions;
            //Debug.Log(PotionsFloat);
            //PotionBarViz.fillAmount = PotionsFloat;
                        Debug.Log("i started potions");

            for(int i =3; i>GameManager.instance.playerPotions; i--)
            {
                OnPotionDrink();
            }
        }

        

        private void Update()
        {
            // if (_potions > 0)
            //         drinkPotion = true;
            //         if (drinkPotion)
            //         {
            //             OnPotionDrink();
            //             drinkPotion = false; // Reset drinkPotion to false after drinking
            //         }

            // {
            //     var color = image[i].color;
            //     color.a = 100f / 255f; // Alpha value is between 0 and 1, so we divide by 255
            //     image[i].color = color;
            // }
        }

        public void OnPotionDrink()
        {
            if(GameManager.instance.playerPotions >= 0 && GameManager.instance.playerPotions <= 3)
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
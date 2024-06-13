using UnityEngine;
using TMPro;
using System.IO;

public class ButtonTextScript : MonoBehaviour
{
    [System.Serializable]
    public class Config
    {
        public string StartButton;
        public string EndButton;
    }

    public TMP_Text StartButtonText;
    public TMP_Text EndButtonText;

    private void Start()
    {
        ReadConfig();
    }

    private void ReadConfig()
    {
        // Load the JSON file from the Resources folder
        TextAsset jsonFile = Resources.Load<TextAsset>("ButtonTextConfig");
        //Debug.Log(jsonFile.text);

        if (jsonFile != null)
        {
            // Deserialize the JSON file to Config object
            Config config = JsonUtility.FromJson<Config>(jsonFile.text);
            Debug.Log(config.StartButton);
            // Set the UI elements with the values from the config
            if (StartButtonText != null)
            {
                StartButtonText.text = config.StartButton;
            }

            if (EndButtonText != null)
            {
                EndButtonText.text = config.EndButton;
            }
        }
        else
        {
            Debug.LogError("Config file not found!");
        }
    }
}

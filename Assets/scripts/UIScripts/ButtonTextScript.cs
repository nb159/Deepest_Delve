using UnityEngine;
using TMPro;
using System.IO;

public class ConfigReader : MonoBehaviour
{
    [System.Serializable]
    public class Config
    {
        public string StartButton;
        public string ExitButton;
    }

    public TMP_Text StartButtonText;
    public TMP_Text ExitButtonText;

    private void Start()
    {
        ReadConfig();
    }

    private void ReadConfig()
    {
        // Load the JSON file from the Resources folder
        TextAsset jsonFile = Resources.Load<TextAsset>("config");
        if (jsonFile != null)
        {
            // Deserialize the JSON file to Config object
            Config config = JsonUtility.FromJson<Config>(jsonFile.text);

            // Set the UI elements with the values from the config
            if (StartButtonText != null)
            {
                StartButtonText.text = config.StartButton;
            }

            if (ExitButtonText != null)
            {
                ExitButtonText.text = config.ExitButton;
            }
        }
        else
        {
            Debug.LogError("Config file not found!");
        }
    }
}

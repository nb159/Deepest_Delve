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
        public string OptionsButton;
        public string RestartButton;
        public string ContinueButton;
    }

    public TMP_Text StartButtonText;
    public TMP_Text EndButtonText;
    public TMP_Text OptionsButtonText;
    public TMP_Text RestartButtonText;
    public TMP_Text ContinueButtonText;

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
            // Debug.Log(config.StartButton);
            // Set the UI elements with the values from the config
            if (StartButtonText != null)
            {
                StartButtonText.text = config.StartButton;
            }

            if (EndButtonText != null)
            {
                EndButtonText.text = config.EndButton;
            }

            if (OptionsButtonText != null)
            {
                OptionsButtonText.text = config.OptionsButton;
            }

            if (ContinueButtonText != null)
            {
                ContinueButtonText.text = config.ContinueButton;
            }

            if (RestartButtonText != null)
            {
                RestartButtonText.text = config.RestartButton;
            }
        }
        else
        {
            Debug.LogError("Button Text Config file not found!");
        }
    }
}

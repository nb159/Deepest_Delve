// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.UI;

// public class StageCounter : MonoBehaviour
// {
//     public static int StageCount;
//     Text StageCounterText;

//     // Start is called before the first frame update
//     void Start()
//     {
//         StageCounterText = GetComponent<Text>();
//     }

//     // Update is called once per frame
//     void Update()
//     {
//         StageCounterText.text = "Stage: " + StageCount;
//     }
// }

using UnityEngine;
using TMPro;
using System.IO;
using UnityEngine.UI;

public class StageCounterScript : MonoBehaviour
{
    [System.Serializable]
    public class Config
    {
        public string StageCounter;
    }

    public TMP_Text StageCounterText;

    private void Start()
    {
        ReadConfig();
    }

    private void ReadConfig()
    {
        // Load the JSON file from the Resources folder
        TextAsset jsonFile = Resources.Load<TextAsset>("StageCounterConfig");
        //Debug.Log(jsonFile.text);

        if (jsonFile != null)
        {
            // Deserialize the JSON file to Config object
            Config config = JsonUtility.FromJson<Config>(jsonFile.text);
            // Debug.Log(config.StartButton);
            // Set the UI elements with the values from the config
            if (StageCounterText != null)
            {
                StageCounterText.text = config.StageCounter;
            }
        }
        else
        {
            Debug.LogError("Stage Counter Config file not found!");
        }
    }
}

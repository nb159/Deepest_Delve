using System;
using UnityEngine;
using UnityEngine.UI;

public class TemplateJsonLoader : MonoBehaviour
{
    [SerializeField] public string jsonFileName = "test"; // Name of the JSON file (without extension)
    [SerializeField] private string otherFileName = "test";
    [SerializeField] private Text uiText;

    private void Start()
    {
        try
        {
            // Load the config using the JsonConfigReader
            var jsonConfigReader = new JsonConfigReader(jsonFileName);
            // Read the config into a custom class
            var testValue = jsonConfigReader.ReadConfig<CustomConfig>();
            // Do something with the result (don't just print it like I did)
            Debug.Log(testValue.testValue);

            // If you want to load a different JSON file, you can do it like this:
            // (This unloads the current JSON file and loads a new one)
            jsonConfigReader.UnloadJsonFile();
            jsonConfigReader.LoadJsonFile(otherFileName);

            // If you want to retain the original JSON file, just load a new one:
            var newConfigReader = new JsonConfigReader(otherFileName);
            var otherTestValue = newConfigReader.ReadConfig<CustomConfig>();
            // ...
        }
        catch (Exception ex)
        {
            Debug.LogError($"Failed to load the config: {ex.Message}");
        }
    }

    // Custom class to hold the JSON  (this does not have to contain all fields that are in the json file)
    private class CustomConfig
    {
        public string testValue;
    }
}
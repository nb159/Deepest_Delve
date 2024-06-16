using System;
using System.IO;
using UnityEngine;

/// <summary>
///     Utility class for reading and parsing JSON configuration files from Unity's Resources folder.
/// </summary>
public class JsonConfigReader : IDisposable
{
    private bool _isLoaded; // Flag indicating if the JSON file is loaded
    private TextAsset _jsonFile; // Reference to the loaded JSON file as a TextAsset
    private string _jsonFileName; // Name of the JSON file to load
    private string _jsonText; // Raw text content of the loaded JSON file

    /// <summary>
    ///     Constructor to initialize the JsonConfigReader with the specified JSON file name.
    ///     Automatically loads the JSON file upon initialization.
    /// </summary>
    /// <param name="jsonFileName">Name of the JSON file (without extension) located in Unity's Resources folder.</param>
    public JsonConfigReader(string jsonFileName)
    {
        _jsonFileName = jsonFileName;
        LoadJsonFile();
    }

    /// <summary>
    ///     Implements the IDisposable interface to release resources.
    ///     Unloads the JSON file from memory when disposed.
    /// </summary>
    public void Dispose()
    {
        UnloadJsonFile();
    }

    /// <summary>
    ///     Loads the JSON file with the specified file name.
    /// </summary>
    /// <param name="jsonFileName">Name of the JSON file (without extension) located in Unity's Resources folder.</param>
    public void LoadJsonFile(string jsonFileName)
    {
        _jsonFileName = jsonFileName;
        LoadJsonFile();
    }

    /// <summary>
    ///     Internal method to load the JSON file using Unity's Resources system.
    /// </summary>
    private void LoadJsonFile()
    {
        _jsonFile = Resources.Load<TextAsset>(_jsonFileName);
        if (_jsonFile == null)
        {
            Debug.LogError($"Json file '{_jsonFileName}' not found");
            throw new FileNotFoundException("File not found", _jsonFileName);
        }

        _jsonText = _jsonFile.text;
        _isLoaded = true;
    }

    /// <summary>
    ///     Reads and deserializes the JSON file into an object of type T.
    /// </summary>
    /// <typeparam name="T">Type of the object to deserialize the JSON into.</typeparam>
    /// <returns>The deserialized object of type T.</returns>
    /// <exception cref="InvalidOperationException">Thrown if the JSON file is not loaded.</exception>
    /// <exception cref="Exception">Thrown if there is an error during JSON deserialization.</exception>
    public T ReadConfig<T>() where T : class
    {
        if (!_isLoaded)
        {
            Debug.LogError("Json file not loaded");
            throw new InvalidOperationException("Json file not loaded");
        }

        try
        {
            return JsonUtility.FromJson<T>(_jsonText);
        }
        catch (Exception ex)
        {
            Debug.LogError($"Error parsing JSON file '{_jsonFileName}': {ex.Message}");
            throw;
        }
    }

    /// <summary>
    ///     Unloads the currently loaded JSON file from memory.
    /// </summary>
    public void UnloadJsonFile()
    {
        if (_jsonFile != null)
        {
            Resources.UnloadAsset(_jsonFile);
            _jsonFile = null;
            _isLoaded = false;
        }
    }
}
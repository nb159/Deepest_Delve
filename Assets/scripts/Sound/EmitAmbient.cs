using UnityEngine;
using AK.Wwise;  // Make sure to import the Wwise namespace

public class PlayEvent : MonoBehaviour
{
    public AK.Wwise.Event wwiseEvent;  // Assign your Wwise event in the inspector

    void Start()
    {
        Debug.Log("Start method called.");
        PlayWwiseEvent();
    }

    public void PlayWwiseEvent()
    {
        if (wwiseEvent != null)
        {
            Debug.Log("Posting Wwise event: " + wwiseEvent.Name);
            wwiseEvent.Post(gameObject);
        }
        else
        {
            Debug.LogError("Wwise event is missing");
        }
    }
}
using UnityEngine;
using TMPro;

public class TextDisappear : MonoBehaviour
{
    public float delay = 7f; // Delay in seconds before the text disappears

    private void Start()
    {
        // Call the DisappearText function after 'delay' seconds
        Invoke("DisappearText", delay);
    }

    private void DisappearText()
    {
        // Destroy the game object this script is attached to
        Destroy(gameObject);
    }
}

using UnityEngine;
using UnityEngine.UI;

public class QuitButton : MonoBehaviour
{
    // Reference to the Button component
    private Button button;

    void Start()
    {
        // Get the Button component attached to the same GameObject
        button = GetComponent<Button>();

        // Add a listener to the button to call the QuitGame function when clicked
        if (button != null)
        {
            button.onClick.AddListener(QuitGame);
        }
        else
        {
            Debug.LogError("Button component not found.");
        }
    }

    // Function to quit the game
    void QuitGame()
    {
        #if UNITY_EDITOR
        // Application.Quit() does not work in the editor so
        // UnityEditor.EditorApplication.isPlaying needs to be set to false to end the game
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        // Quit the application
        Application.Quit();
        #endif
    }
}

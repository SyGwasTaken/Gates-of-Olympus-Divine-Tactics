using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayButton : MonoBehaviour
{
    // The name of the scene to load
    public string sceneName;

    // Reference to the Button component
    private Button button;

    void Start()
    {
        // Get the Button component attached to the same GameObject
        button = GetComponent<Button>();

        // Add a listener to the button to call the LoadScene function when clicked
        if (button != null)
        {
            button.onClick.AddListener(LoadScene);
        }
        else
        {
            Debug.LogError("Button component not found.");
        }
    }

    // Function to load the specified scene
    void LoadScene()
    {
        // Load the scene with the specified name
        SceneManager.LoadScene(sceneName);
    }
}

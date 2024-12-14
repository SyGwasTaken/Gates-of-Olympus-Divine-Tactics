using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyNPCController : MonoBehaviour
{
    [SerializeField] Dialog dialog;
    [SerializeField] GameObject player; // Manually reference the player GameObject
    [SerializeField] float interactionRange = 2f;
    [SerializeField] UnityEditor.SceneAsset battleScene; // Expose the scene asset to Unity editor

    private Character character;
    private bool isInteracting = false;

    private void Start()
    {
        // Ensure DialogManager is initialized before subscribing to the event
        if (DialogManager.Instance != null)
        {
            DialogManager.Instance.OnCloseDialog += OnDialogClosed; // Subscribe to event
        }
        else
        {
            Debug.LogError("DialogManager is not assigned in the scene!");
        }

        character = GetComponent<Character>();
    }

    private void Update()
    {
        // Check if the player is close enough to interact
        if (player != null && Vector3.Distance(transform.position, player.transform.position) < interactionRange)
        {
            if (Input.GetKeyDown(KeyCode.F) && !isInteracting) // Assuming 'F' is the interact button
            {
                Interact(); // Start dialog
            }
        }
    }

    public void Interact()
    {
        if (!isInteracting)
        {
            StartCoroutine(DialogManager.Instance.ShowDialog(dialog)); // Show dialog
            isInteracting = true; // Set interacting flag
        }
    }

    private void OnDialogClosed()
    {
        // After the dialog is closed, load the battle scene
        LoadBattleScene();
    }

    private void LoadBattleScene()
    {
        if (battleScene != null)
        {
            string sceneName = battleScene.name; // Get the name of the scene asset
            SceneManager.LoadScene(sceneName); // Load the battle scene
        }
        else
        {
            Debug.LogError("Battle scene not assigned in the inspector!");
        }
    }

    public void EndInteraction()
    {
        // Reset the interaction state after the dialog ends
        isInteracting = false;
    }
}

using UnityEngine;

public class NPCController : MonoBehaviour, Interactables
{
    [SerializeField] Dialog dialog;
    public void Interact()
    {
        StartCoroutine(DialogManager.Instance.ShowDialog(dialog));
    }
}

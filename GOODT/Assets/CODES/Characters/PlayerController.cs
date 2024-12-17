using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Vector2 input;
    
    private Character character;
    public static object Instance { get; internal set; }

    private void Awake()
    {
        character = GetComponent<Character>();
    }
    public void HandleUpdate()
    {
        if (!character.isMoving)
        {
            input.x = Input.GetAxisRaw("Horizontal");
            input.y = Input.GetAxisRaw("Vertical");

            // Remove diagonal movement
            //if (input.x != 0) input.y = 0;

            if (input != Vector2.zero)
            {
                StartCoroutine(character.Move(input));
            }
        }

        character.HandleUpdate();

        if(Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("Interact button pressed");
                       interact();
        }
    }

    void interact()
    {
        var faceDir = new Vector3(character.Animator.MoveX, character.Animator.MoveY);
        var interactpos = transform.position + faceDir;

        //Debug.DrawLine(transform.position, interactpos, Color.green, 0.5f);

        var collider = Physics2D.OverlapCircle(interactpos, 0.3f, GameLayers.i.interactablesLayer);
        if (collider != null)
        {
            collider.GetComponent<Interactables>()?.Interact();
        }
    }
}
using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public LayerMask SolidObjectsLayer;
    public LayerMask InteractablesLayer;
    private bool isMoving;
    private Vector2 input;
    
    private Animator animator;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        if (!isMoving)
        {
            input.x = Input.GetAxisRaw("Horizontal");
            input.y = Input.GetAxisRaw("Vertical");

            // Remove diagonal movement
            if (input.x != 0) input.y = 0;

            if (input != Vector2.zero)
            {
                animator.SetFloat("moveX", input.x);
                animator.SetFloat("moveY", input.y);
                
                var targetPos = transform.position;
                targetPos.x += input.x;
                targetPos.y += input.y;

                if(IsWalkable(targetPos))
                StartCoroutine(Move(targetPos));
            }
        }
                animator.SetBool("isMoving", isMoving);

                if(Input.GetKeyDown(KeyCode.F))
                    interact();
    }

    void interact()
    {
        var faceDir = new Vector3(animator.GetFloat("moveX"), animator.GetFloat("moveY"));
        var interactpos = transform.position + faceDir;

        Debug.DrawLine(transform.position, interactpos, Color.green, 0.5f);

        var collider = Physics2D.OverlapCircle(interactpos, 0.5f, InteractablesLayer);
        if (collider != null)
        {
            collider.GetComponent<Interactables>()?.Interact();
        }
    }

    IEnumerator Move(Vector3 targetPos)
    {
        isMoving = true;


        while ((targetPos - transform.position).sqrMagnitude > Mathf.Epsilon)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
            yield return null;
        }
        transform.position = targetPos;

        isMoving = false;
    }

    private bool IsWalkable(Vector3 targetPos)
    {
        if(Physics2D.OverlapCircle(targetPos, 0.1f, SolidObjectsLayer | InteractablesLayer) != null)
        {
            return false;
        }

        return true;
    } 
}
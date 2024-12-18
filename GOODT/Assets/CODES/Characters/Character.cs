using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Character : MonoBehaviour
{
    public float moveSpeed;
    CharacterAnimator animator;
    public bool isMoving {get; private set; }
    

    private void Awake()
    {
        animator = GetComponent<CharacterAnimator>();
    }

    public IEnumerator Move(Vector2 moveVec)
    {
                animator.MoveX = Mathf.Clamp(moveVec.x, -1f, 1f);
                animator.MoveY = Mathf.Clamp(moveVec.y, -1f, 1f);
                
                var targetPos = transform.position;
                targetPos.x += moveVec.x;
                targetPos.y += moveVec.y;
        
        if (!IsWalkable(targetPos))
            yield break;

    isMoving = true;


        while ((targetPos - transform.position).sqrMagnitude > Mathf.Epsilon)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
            yield return null;
        }
        transform.position = targetPos;

    isMoving = false;
    }

    public void HandleUpdate()
    {
        animator.isMoving = isMoving;
    }

    private bool IsWalkable(Vector3 targetPos)
    {
        if(Physics2D.OverlapCircle(targetPos, 0.1f, GameLayers.i.SolidLayer | GameLayers.i.interactablesLayer) != null)
        {
            return false;
        }

        return true;
    }

    public CharacterAnimator Animator
    {
        get => animator;
    }
}

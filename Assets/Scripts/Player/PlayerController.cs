using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

//script to move the player 
public class PlayerController : MonoBehaviour
{
    public SwordAttack swordAttack;
    bool canMove = true;
    SpriteRenderer spriteRenderer;
    Vector2 movementInput;
    public float moveSpeed = 1f;
    public float collisionOffset = 0.02f;
    
    public ContactFilter2D movementFilter;

    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();

    Animator animator;
    [SerializeField] private AudioSource soundMovement;

    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        if(canMove){
            if(movementInput != Vector2.zero)//If there is a movement input, try to move the player
            {
                bool success = TryToMove(movementInput);//try to move with all input

                if (!success){

                    success = TryToMove(new Vector2(movementInput.x, 0));//try to move only with horizontal axis

                    if(!success)
                    {
                        success = TryToMove(new Vector2(0, movementInput.y));//try to move only with vertical axi
                    }
                }
                animator.SetBool("IsMoving", success);//call the animator to animate the player
            }
            else{
                animator.SetBool("IsMoving", false);
            }

            //Set the direction of the sprite
            if(movementInput.x < 0)
            {
                spriteRenderer.flipX = true;
            }
            else if(movementInput.x > 0){
                spriteRenderer.flipX = false;
            }
        }
    }

    private bool TryToMove(Vector2 direction)
    {
        if(direction != Vector2.zero)//if the player move in a good direction
        {
            //Cast to see if the movement is valid. If there is nothing on the way who block the player, count = 0
            int count = rb.Cast(direction,movementFilter,castCollisions,moveSpeed * Time.fixedDeltaTime + collisionOffset);

            if(count == 0)
            {
                if(!soundMovement.isPlaying){
                    soundMovement.Play();
                }
                rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);
                return true;
            }
            else{
                return false;
            }
        }
        else{// the player can't move so stop the moving animation and stay idle
            return false;
        }
        
    } 
    //when there is an input, get the vector of the input
    void OnMove(InputValue movementValue)
    {
        movementInput = movementValue.Get<Vector2>();
    }

    //when there is an input for fire, set animation "AttackSword" who call the Swing function
    void OnFire(){
        animator.SetTrigger("AttackSword");
    }

    //attack with the sword in the right direction
    public void Swing()
    {
        LockMovement();
        if(spriteRenderer.flipX == true)
        {
        swordAttack.AttackLeft();
        }
        else{
        swordAttack.AttackRight();
        }
    }

    //function call at the end of the attack animation
    public void EndAttack()
    {
        swordAttack.StopAttack();
        UnlockMovement();
    }

    //lock the movement of the player
    void LockMovement()
    {
        canMove =false;
    }
    //unlock the movement of the player
    void UnlockMovement()
    {
        canMove = true;
    }
}


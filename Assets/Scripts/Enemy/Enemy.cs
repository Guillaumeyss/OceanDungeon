using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    Animator animator;

    public float moveSpeed = 500f; //set to 500 by default but can be modified in the Inspector in Unity for each enemy

    public DetectionZone detectionZone;//if the player is inside, the enemy move to him

    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;
    public float damage = 1f;

    public float knockbackPower = 300f;


    private void Start() {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

     void FixedUpdate() {
        //if the player is in range, the enemy move to the player with the run animation
        if(detectionZone.detectedObjs.Count > 0)
        {
            Vector2 direction = (detectionZone.detectedObjs[0].transform.position - transform.position).normalized;
            animator.SetBool("IsMoving", true);

            if(direction.x < 0)//flip the enemy to watch in the player direction
            {
                spriteRenderer.flipX = true;
            }
            else if(direction.x > 0)
            {
                spriteRenderer.flipX = false;
            }
            rb.AddForce(direction * moveSpeed * Time.deltaTime);
            //rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime); //try another way to move to compare
        }
        //If the player is not in range, the enemy stay with the idle animation 
        if(detectionZone.detectedObjs.Count == 0)
        {
            animator.SetBool("IsMoving", false);
        }

    }

    //if the enemy touch a damageable characters, calcul the vector to inflict a knockback and hit him
    void OnCollisionEnter2D(Collision2D other)
    {
        IDamageable damageable = other.collider.GetComponent<IDamageable>();

        if(damageable != null)
        {
            Vector3 parentPosition = gameObject.GetComponentInParent<Transform>().position;
            Vector2 direction = (Vector2) (other.gameObject.transform.position - gameObject.GetComponentInParent<Transform>().position).normalized;
            Vector2 knockback = direction * knockbackPower;
            damageable.TakeDamage(damage, knockback);
        }
    }
}

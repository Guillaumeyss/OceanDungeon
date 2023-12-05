using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//class for every characters who can take damages
public class DamageableCharacters : MonoBehaviour, IDamageable
{
    //get the animator of the object to apply some animations during the program
    Animator animator;
    Rigidbody2D rb;

    //object of the class healthBar to create a slider on the top of the character to see his life
    public HealthBarBehavior healthBarBehavior;

    //max health of the character
    public float maxHealth;

    //object who show the damage created when the character is hit
    [SerializeField] GameObject damageText;

    public float Health{

        set{
        if (value < _health)
        {
            animator.SetTrigger("hit");
            healthBarBehavior.SetHealth(value, maxHealth);
            RectTransform txtTransform = Instantiate(damageText).GetComponent<RectTransform>();
            txtTransform.transform.position = Camera.main.WorldToScreenPoint(gameObject.transform.position);
            Canvas canvas = GameObject.FindObjectOfType<Canvas>();
            txtTransform.SetParent(canvas.transform);
        }

        _health=value;

        if(_health <= 0)
        {
            if(animator.gameObject.tag == "Player")
                animator.SetTrigger("death");
            else
                Die();
        }}
        get{
            return _health;
        }
    }

    public float _health = 1;


    private void Start() {
        maxHealth = _health;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        healthBarBehavior.SetHealth(_health, maxHealth);
    }

    //function to call to apply damage without knockback to the character
    public void TakeDamage(float damage)
    {
        Health -= damage;
    }

    //function to call to apply damage with knockback to the character
    public void TakeDamage(float damage, Vector2 knockback)
    {
        Debug.Log("TakeDamage function is called");
        Health -= damage;
        rb.AddForce(knockback);
    }  

    //function to call when the life of the character is < 0 to destroy the object
    public void Die()
    {
        Destroy(gameObject);
    }

}

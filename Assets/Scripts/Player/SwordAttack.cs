using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttack : MonoBehaviour
{   
    //collider of the sword
    public Collider2D swordCollider;
    Vector2 rightAttackOffset;
    public float swordDamage = 2;
    public float knockbackPower = 1500f;

    // Start is called before the first frame update
    void Start()
    {
        swordCollider.enabled = false;
        rightAttackOffset = transform.position;//get the default position of the sword (right of the player)
    }

    public void AttackRight()
    {   //activate the collider and attack to the right
        swordCollider.enabled = true;
        transform.localPosition = rightAttackOffset;
    }
    public void AttackLeft()
    {
        //activate the collider and attack to the left (same as right but negative x value)
        swordCollider.enabled = true;
        transform.localPosition = new Vector2(rightAttackOffset.x * -1, rightAttackOffset.y);
    }

    public void StopAttack()
    {
        //function call at the end of the attack
        swordCollider.enabled = false;
    }

    //call when the collider of the sword touch something
    private void OnTriggerEnter2D(Collider2D other) {
        //can attack all things declared as "Damageable" with the tag
        if (other.tag == "Damageable")
        {
            IDamageable damageable = other.GetComponent<IDamageable>();

            if(damageable != null)
            {
                Vector3 parentPosition = gameObject.GetComponentInParent<Transform>().position;
                Vector2 direction = (Vector2) (other.gameObject.transform.position - gameObject.GetComponentInParent<Transform>().position).normalized;
                Vector2 knockback = direction * knockbackPower;
                //call takedamage function with a knockback
                damageable.TakeDamage(swordDamage, knockback);
            }
            else{
                Debug.LogWarning("Does not implement IDamageable");
            }
        }
        
        //Old script to attack without knockback and only enemy not all damageable characters
        /*  if(other.tag == "Enemy")
         {
            //deal damage to the enemy only if he has the tag
            Enemy enemy = other.GetComponent<Enemy>();
            if(enemy!=null)
            {
                
                enemy.TakeDamage(swordDamage);
            }
        } */
         
    }

}

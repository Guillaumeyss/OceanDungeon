using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttack : MonoBehaviour
{
    private static SwordAttack _instance;
    [SerializeField] private AudioSource soundSwordAttack;

    public static SwordAttack Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<SwordAttack>();
                if (_instance == null)
                {
                    GameObject go = new GameObject("SwordAttack");
                    _instance = go.AddComponent<SwordAttack>();
                }
            }
            return _instance;
        }
    }

    // Collider of the sword
    public Collider2D swordCollider;
    private Vector2 rightAttackOffset;
    public float swordDamage = 2f;
    public float knockbackPower = 1500f;

    // Ensure that there is only one SwordAttack instance in the game
    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        swordCollider.enabled = false;
        rightAttackOffset = transform.position; // Default position of the sword (right of the player)
    }

    public void AttackRight()
    {
        swordCollider.enabled = true;
        
        soundSwordAttack.Play();
        transform.localPosition = rightAttackOffset;
    }

    public void AttackLeft()
    {
        swordCollider.enabled = true;
        
        soundSwordAttack.Play();
        transform.localPosition = new Vector2(-rightAttackOffset.x, rightAttackOffset.y);
    }

    public void StopAttack()
    {
        swordCollider.enabled = false;
    }

    // Call when the collider of the sword touches something
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Damageable"))
        {
            IDamageable damageable = other.GetComponent<IDamageable>();
            if (damageable != null)
            {
                Vector2 direction = ((Vector2)(other.transform.position - transform.position)).normalized;
                Vector2 knockback = direction * knockbackPower;
                damageable.TakeDamage(swordDamage, knockback);
            }
            else
            {
                Debug.LogWarning("Does not implement IDamageable");
            }
        }
    }
}

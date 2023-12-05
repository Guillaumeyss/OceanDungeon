using UnityEngine;

public interface IDamageable{
    public float Health{set;get;}
    public void TakeDamage(float damage, Vector2 knockback);
    public void TakeDamage(float damage);
    public void Die();
}
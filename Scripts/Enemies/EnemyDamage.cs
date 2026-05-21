using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [SerializeField] protected float damage;

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Health h = collision.GetComponent<Health>();
            // REVISI: Hanya jalankan TakeDamage jika darah masih ada
            if (h != null && h.currentHealth > 0)
            {
                h.TakeDamage(damage);
            }
        }
    }
}
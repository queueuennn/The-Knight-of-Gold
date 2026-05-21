using UnityEngine;
using System.Collections;

public class Firetrap : MonoBehaviour
{
    [SerializeField] private float damage;

    [Header("Firetrap Timers")]
    [SerializeField] private float activationDelay;
    [SerializeField] private float activeTime;
    private Animator anim;
    private SpriteRenderer spriteRend;

    private bool triggered;
    private bool active;

    private Health playerHealth;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        // REVISI: Hanya beri damage jika HP masih di atas 0
        if (playerHealth != null && active && playerHealth.currentHealth > 0)
            playerHealth.TakeDamage(damage);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerHealth = collision.GetComponent<Health>();

            if (!triggered)
                StartCoroutine(ActivateFiretrap());

            // REVISI: Hanya beri damage jika HP masih di atas 0
            if (active && playerHealth != null && playerHealth.currentHealth > 0)
                playerHealth.TakeDamage(damage);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            playerHealth = null;
    }

    private IEnumerator ActivateFiretrap()
    {
        triggered = true;
        spriteRend.color = Color.red;

        yield return new WaitForSeconds(activationDelay);
        spriteRend.color = Color.white;
        active = true;
        anim.SetBool("activated", true);

        yield return new WaitForSeconds(activeTime);
        active = false;
        triggered = false;
        anim.SetBool("activated", false);
    }

    private void OnEnable()
    {
        triggered = false;
        active = false;

        if (spriteRend == null)
            spriteRend = GetComponent<SpriteRenderer>();

        if (anim == null)
            anim = GetComponent<Animator>();

        spriteRend.color = Color.white;
        anim.SetBool("activated", false);

        StopAllCoroutines();
    }
}
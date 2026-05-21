using System.Collections;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private float startingHealth = 3;
    public float currentHealth { get; private set; }

    private Animator anim;
    private SpriteRenderer spriteRend;
    private bool dead;

    [Header("Invulnerability Frames")]
    [SerializeField] private float iFramesDuration = 1f;
    [SerializeField] private int numberOfFlashes = 5;

    [Header("Components To Disable On Death")]
    [SerializeField] private Behaviour[] components;
    private bool invulnerable;

    [Header("Audio")]
    [SerializeField] private AudioClip deathSound;
    [SerializeField] private AudioClip hurtSound;

    private void Awake()
    {
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
    }

    public void TakeDamage(float damage)
    {
        // Jika sudah mati atau sedang invulnerable, langsung keluar agar tidak mengganggu Animator
        if (dead || invulnerable) return;

        currentHealth = Mathf.Clamp(currentHealth - damage, 0, startingHealth);

        if (currentHealth > 0)
        {
            anim.SetTrigger("Hurt");
            if (SoundManager.instance != null)
                SoundManager.instance.PlaySound(hurtSound);
            StartCoroutine(Invulnerability());
        }
        else
        {
            if (!dead) Die();
        }
    }

    private void Die()
    {
        dead = true;

        // REVISI FINAL: Memaksa Animator keluar dari "Freeze"
        // 1. Reset semua trigger yang mungkin nyangkut
        anim.ResetTrigger("Hurt");

        // 2. Gunakan Play alih-alih SetTrigger untuk memotong semua Any State
        // Parameter kedua (0) adalah layer, parameter ketiga (0f) adalah start time
        anim.Play("Die", 0, 0f);

        if (SoundManager.instance != null)
            SoundManager.instance.PlaySound(deathSound);

        // Matikan komponen agar tidak mengirim input/data baru ke Animator
        foreach (Behaviour component in components)
        {
            if (component != anim && component != this)
                component.enabled = false;
        }

        // Munculkan Game Over
        PlayerRespawn respawn = GetComponent<PlayerRespawn>();

        if (respawn != null)
        {
            respawn.CheckRespawn();
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    public void AddHealth(float value)
    {
        if (dead) return;
        currentHealth = Mathf.Clamp(currentHealth + value, 0, startingHealth);
    }

    public void Respawn()
    {
        dead = false;
        currentHealth = startingHealth;
        anim.enabled = true;
        anim.ResetTrigger("Die");
        anim.ResetTrigger("Hurt");
        anim.Play("Idle");

        foreach (Behaviour component in components)
            component.enabled = true;
    }

    private IEnumerator Invulnerability()
    {
        invulnerable = true;
        Physics2D.IgnoreLayerCollision(10, 11, true);
        for (int i = 0; i < numberOfFlashes; i++)
        {
            spriteRend.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
            spriteRend.color = Color.white;
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
        }
        Physics2D.IgnoreLayerCollision(10, 11, false);
        invulnerable = false;
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
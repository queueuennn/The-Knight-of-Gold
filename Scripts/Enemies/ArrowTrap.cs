using UnityEngine;

public class ArrowTrap : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] arrows;
    private float cooldownTimer;
    [SerializeField] private AudioClip arrowSound;

    private void Attack()
    {
        SoundManager.instance.PlaySound(arrowSound);
        cooldownTimer = 0;

        // REVISI: Ambil indeks panah sekali saja agar tidak terjadi ketidaksinkronan objek
        int arrowIndex = FindArrow();

        arrows[arrowIndex].transform.position = firePoint.position;
        arrows[arrowIndex].GetComponent<EnemyProjectile>().ActivateProjectile();
    }

    private int FindArrow()
    {
        for (int i = 0; i < arrows.Length; i++)
        {
            if (!arrows[i].activeInHierarchy)
                return i;
        }
        return 0;
    }

    private void Update()
    {
        cooldownTimer += Time.deltaTime;
        if (cooldownTimer >= attackCooldown)
        {
            Attack();
        }
    }
}
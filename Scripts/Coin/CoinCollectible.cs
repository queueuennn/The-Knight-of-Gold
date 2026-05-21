using UnityEngine;

public class CoinCollectible : MonoBehaviour
{
    [SerializeField] private int coinValue = 1;
    [SerializeField] private AudioClip collectSound;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SoundManager.instance.PlaySound(collectSound);

            CoinManager.instance.AddCoins(coinValue);

            gameObject.SetActive(false);
        }
    }
}
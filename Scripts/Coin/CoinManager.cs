using UnityEngine;
using UnityEngine.UI;

public class CoinManager : MonoBehaviour
{
    public static CoinManager instance;

    [SerializeField] private Text coinText;

    private int coins;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    private void Start()
    {
        UpdateCoinUI();
    }

    public void AddCoins(int amount)
    {
        coins += amount;
        UpdateCoinUI();
    }

    private void UpdateCoinUI()
    {
        coinText.text = "Coins: " + coins;
    }
}
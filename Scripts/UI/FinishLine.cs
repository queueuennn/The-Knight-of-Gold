using UnityEngine;

public class FinishLine : MonoBehaviour
{
    private bool finished;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !finished)
        {
            finished = true;

            FindObjectOfType<UIManager>().FinishLevel();
        }
    }
}
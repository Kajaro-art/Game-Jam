using System.Threading.Tasks;
using UnityEngine;

public class DamageGiver : MonoBehaviour
{
    [SerializeField] int scoreToDecrease;

    public static event System.Action<int> OnPlayerHit;
    public static event System.Action<int> OnPlayerTrapped;
    public static event System.Action OnPlayerInstakill;

    async void Wait(int time)
    {
        await Task.Delay(time);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            OnPlayerHit?.Invoke(scoreToDecrease);
        }

        else if (collision.transform.CompareTag("Player") && transform.CompareTag("Hole"))
        {
            OnPlayerInstakill?.Invoke();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player") && transform.CompareTag("Cage"))
        {
            OnPlayerTrapped?.Invoke(scoreToDecrease);
            Wait(1001);
            Destroy(gameObject);
        }
    }
}

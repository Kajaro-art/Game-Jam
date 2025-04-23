using Unity.Properties;
using UnityEngine;
using static Unity.VisualScripting.Member;

public class EggController : MonoBehaviour
{
    [SerializeField] int scoreToGive;
    [SerializeField] bool isLargeEgg, isHouse, isBalloonEgg;
    [SerializeField] System.Collections.Generic.List<Sprite> smallEggSprites;
    [SerializeField] System.Collections.Generic.List<Sprite> largeEggSprite;

    [SerializeField] SpriteRenderer spriteRenderer;

    [SerializeField] AudioSource source;
    [SerializeField] AudioClip smallEgg, bigEgg, delivery;

    public static event System.Action<int> IncreaseScore;
    public static event System.Action AddLargeEgg;


    private void Awake()
    {
        if (isHouse) return;

        if (isBalloonEgg && isLargeEgg)
        {
            SetBigEgg();
            return;
        }
        else if (isBalloonEgg)
        {
            SetSmallEgg();
            return;
        }

        if (Random.value < 0.6)
        {
            SetSmallEgg();
        }
        else
        {
            SetBigEgg();
        }
    }

    void SetSmallEgg()
    {
        spriteRenderer.sprite = smallEggSprites[Random.Range(0, smallEggSprites.Count - 1)];
        scoreToGive = 10;
    }
    void SetBigEgg()
    {
        spriteRenderer.sprite = largeEggSprite[Random.Range(0, largeEggSprite.Count - 1)];
        isLargeEgg = true;
        scoreToGive = 50;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            if (isHouse)
            {
                var playerEggs = collision.transform.GetComponentInParent<PlayerEggController>();
                var totalEggs = playerEggs.GetCurrentEggs();
                GranScoreHouse(totalEggs, 100);
                playerEggs.ResetEggs();
                return;
            }

            if (isLargeEgg)
            {
                AddLargeEgg?.Invoke();
                source.PlayOneShot(bigEgg);
                IncreaseScore?.Invoke(scoreToGive);
                Destroy(gameObject);
                return;
            }
            source.PlayOneShot(smallEgg);
            IncreaseScore?.Invoke(scoreToGive);
            Destroy(gameObject);
        }
    }

    void GranScoreHouse(int val, int egg)
    {
        for (int i = 0; i < val; i++)
        {
            source.PlayOneShot(delivery);
            IncreaseScore?.Invoke(egg);
        }
    }
}

using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public PlayerReferenceManager manager;
    [SerializeField] int currentHealth, maxHealth;
    [SerializeField] bool canBeHit;

    [SerializeField] AudioSource source;
    [SerializeField] AudioClip damage;

    public static event System.Action OnPlayerDeath;
    public static event System.Action OnHealthLost;

    private void OnEnable()
    {
        DamageGiver.OnPlayerHit += LowerHealth;
        DamageGiver.OnPlayerInstakill += InstaKill;
    }

    private void Awake()
    {
        currentHealth = maxHealth;
        manager = GetComponent<PlayerReferenceManager>();
    }

    private void Update()
    {
        if(transform.position.y< -12f)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(2);
        }
    }
    void LowerHealth(int a)
    {
        if(canBeHit && manager.eggController.GetCurrentEggs() == 0)
        {
            currentHealth--;
            source.PlayOneShot(damage);
            OnHealthLost?.Invoke();
        }
        else if(canBeHit && manager.eggController.GetCurrentEggs() > 0)
        {
            source.PlayOneShot(damage);
            manager.eggController.LoseLargeEgg();
        }

        manager.anim.SetTrigger("WasHit");
        DeathCheck();
        ImmunityFrames(1001);
    }

    async void ImmunityFrames(int duration)
    {
        manager.playerMovement.SetHealthCollider(false);
        canBeHit = false;
        await Task.Delay(duration);
        canBeHit = true;
        manager.playerMovement.SetHealthCollider(true);
    }

    void DeathCheck()
    {
        if(currentHealth <= 0)
        {
            PlayerDeath();
        }
    }

    public void InstaKill()
    {
        PlayerDeath();
    }

    void PlayerDeath()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(2);
        OnPlayerDeath?.Invoke();
    }

    private void OnDisable()
    {
        DamageGiver.OnPlayerHit -= LowerHealth;
        DamageGiver.OnPlayerInstakill -= InstaKill;
    }
}

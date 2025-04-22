using UnityEngine;

public class PlayerReferenceManager : MonoBehaviour
{
    public PlayerEggController eggController;
    public PlayerHealth playerHealth;
    public PlayerMovement playerMovement;

    public Animator anim;
    private void Awake()
    {
        playerHealth = GetComponent<PlayerHealth>();
        eggController = GetComponent<PlayerEggController>();
        playerMovement = GetComponent<PlayerMovement>();
        anim = GetComponent<Animator>();
    }

}

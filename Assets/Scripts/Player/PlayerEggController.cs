using UnityEngine;

public class PlayerEggController : MonoBehaviour
{
    [SerializeField] int currentEggs, maxEggs;

    public static event System.Action<int> OnLargeEggAdded;
    public static event System.Action<int> OnLargeEggLost;

    private void OnEnable()
    {
        EggController.AddLargeEgg += AddEgg;
    }

    public void AddEgg()
    {
        if(currentEggs >= maxEggs)
        {
            return;
        }

        if(currentEggs < maxEggs)
        {
            OnLargeEggAdded?.Invoke(+1);
            currentEggs++;
        }
    }

    public int GetCurrentEggs()
    {
        return currentEggs;
    }

    public void LoseLargeEgg()
    {
        OnLargeEggLost?.Invoke(-1);
        currentEggs--;
    }

    public void ResetEggs()
    {
        OnLargeEggLost?.Invoke(-currentEggs);
        currentEggs -= currentEggs;
    }

    private void OnDisable()
    {
        EggController.AddLargeEgg -= AddEgg;
    }
}
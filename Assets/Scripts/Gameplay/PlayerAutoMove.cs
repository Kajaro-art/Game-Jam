using UnityEngine;
using System.Threading.Tasks;

public class PlayerAutoMove : MonoBehaviour
{
    [SerializeField] float mapMoveSpeed;
    [SerializeField] bool canWorldMove;

    private void OnEnable()
    {
        DamageGiver.OnPlayerTrapped += StopWorldMovement;
    }

    async void StopWorldMovement(int a)
    {
        canWorldMove = false;
        await Task.Delay(1501);
        canWorldMove = true;
    }


    async void StopWorldMovementDelivery(int waitTime)
    {
        canWorldMove = false;
        await Task.Delay(waitTime);
        canWorldMove = true;
    }

    private void Update()
    {
        if (canWorldMove)
        {
            mapMoveSpeed = Mathf.Clamp(mapMoveSpeed += Time.deltaTime / 2, 0, 7);
            transform.position += new Vector3(mapMoveSpeed, 0) * Time.deltaTime;
        }
        else
        {
            mapMoveSpeed = 0;
        }
    }

    private void OnDisable()
    {
        DamageGiver.OnPlayerTrapped -= StopWorldMovement;
    }
}

using Unity.VisualScripting;
using UnityEngine;

public class RatController : MonoBehaviour
{
    [SerializeField] float ratSpeed;
    [SerializeField] int currentPatrolPoint;
    [SerializeField] System.Collections.Generic.List<Transform> patrolPoints = new();

    private void Update()
    {
        Patrol();
        SetPatrol();
    }

    void Patrol()
    {
        Vector2 modDestination = new(
            x: patrolPoints[currentPatrolPoint].localPosition.x,
            y: transform.localPosition.y);

        transform.localPosition = Vector2.MoveTowards(
            current: transform.localPosition,
            target: modDestination,
            maxDistanceDelta: ratSpeed * Time.deltaTime);
    }

    void SetPatrol()
    {
        float distance = Vector2.Distance(transform.localPosition, patrolPoints[currentPatrolPoint].localPosition);
        if (distance < .5f && currentPatrolPoint < patrolPoints.Count - 1)
        {
            currentPatrolPoint++;
            FlipAsset();
        }
        else if (distance < .5f && currentPatrolPoint >= patrolPoints.Count - 1)
        {
            currentPatrolPoint = 0;
            FlipAsset();
        }
    }

    void FlipAsset()
    {
        if (transform.localPosition.x - patrolPoints[currentPatrolPoint].localPosition.x < 0)
        {
            Debug.Log("Looking Right");
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        {
            Debug.Log("Looking Left");
            GetComponent<SpriteRenderer>().flipX = false;
        }
    }
}

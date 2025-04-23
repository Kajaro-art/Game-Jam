using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform player;           // Assign your player here
    public float xOffset = 5f;         // How far ahead of the player the camera looks

    private float fixedY;
    private float fixedZ;

    void Start()
    {
        // Lock Y and Z axes at the start
        fixedY = transform.position.y;
        fixedZ = transform.position.z;
    }

    void LateUpdate()
    {
        if (player != null)
        {
            transform.position = new Vector3(player.position.x + xOffset, fixedY, fixedZ);
        }
    }
}


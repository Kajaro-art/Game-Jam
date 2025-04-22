using UnityEngine;

public class CameraSwitch : MonoBehaviour
{
    public GameObject cam1, cam2;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            cam1.SetActive(false);
            cam2.SetActive(true);
            Destroy(gameObject);
        }
    }
}

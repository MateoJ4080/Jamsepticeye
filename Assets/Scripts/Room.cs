using UnityEngine;

public class Room : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collided");
        if (other.CompareTag("Player"))
        {
            CameraController.Instance.HandleRoomChanged(this);
        }
    }
}

using UnityEngine;

public class Room : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CameraController.Instance.HandleRoomChanged(this);
        }
    }
}

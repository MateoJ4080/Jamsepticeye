using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private Transform player;
    [SerializeField] private Transform hallwayRoom;
    [SerializeField] private Transform targetPoint;

    public void Interact()
    {
        Vector3 playerPos = player.transform.position;

        if (gameObject.CompareTag("DoorToHallway") && GameManager.Instance.r1_hasKeyToHallway == true)
        {
            // CLEAN LATER IF POSSIBLE (method in PlayerController doing exactly the same)
            CharacterController cc = player.GetComponent<CharacterController>();
            cc.enabled = false;
            player.position = new Vector3(targetPoint.position.x, player.position.y, targetPoint.position.z);
            cc.enabled = true;

            CameraController.Instance.TeleportToX(hallwayRoom.position.x);

            player.position = new(targetPoint.position.x, playerPos.y, targetPoint.position.z);
        }


        if (gameObject.CompareTag("DoorToOutside") && GameManager.Instance.r3_hasKeyToOutside)
        {
            if (GameManager.Instance.r3_hasKeyToOutside) SceneManager.LoadScene("Credits");
        }
    }
}

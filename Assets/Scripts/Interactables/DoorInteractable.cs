using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private Transform player;
    [SerializeField] private Transform hallwayRoom;
    [SerializeField] private Transform targetPoint;

    public void Interact()
    {
        if (gameObject.CompareTag("DoorToFirstHallway") || gameObject.CompareTag("DoorToSecondHallway") && GameManager.Instance.r1_hasKeyToHallway)
        {
            AudioManager.Instance.PlaySFX(AudioManager.Instance.SFX_DoorUnlock);

            CharacterController cc = player.GetComponent<CharacterController>();
            cc.enabled = false;
            player.position = new Vector3(targetPoint.position.x, player.position.y, targetPoint.position.z);
            cc.enabled = true;

            CameraController.Instance.TeleportToX(hallwayRoom.position.x);
        }


        if (gameObject.CompareTag("DoorToOutside"))
        {
            if (GameManager.Instance.r3_hasKeyToOutside)
            {
                SceneManager.LoadScene("Credits");
            }
            else Debug.Log("You don't have the key!");
        }
    }
}

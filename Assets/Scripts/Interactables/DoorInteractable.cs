using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorInteractable : MonoBehaviour, IInteractable
{
    private Transform player;
    [SerializeField] private Transform targetPoint;
    [SerializeField] private bool isLocked;

    void Awake()
    {
        if (player == null) player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public void Interact()
    {
        if (isLocked && !GameManager.Instance.r3_hasKeyToOutside)
        {
            AudioManager.Instance.PlaySFX(AudioManager.Instance.SFX_DoorLocked);
            return;
        }

        AudioManager.Instance.PlaySFX(AudioManager.Instance.SFX_DoorUnlock);

        if (gameObject.CompareTag("DoorToOutside"))
        {
            StartCoroutine(SceneLoader.Instance.LoadSceneFade("Credits"));
            return;
        }

        isLocked = false;


        CharacterController cc = player.GetComponent<CharacterController>();
        cc.enabled = false;
        player.position = new Vector3(targetPoint.position.x, player.position.y, player.position.z);
        cc.enabled = true;
    }
}

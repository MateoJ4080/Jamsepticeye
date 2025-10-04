using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private float interactRadius = 2f;
    [SerializeField] private LayerMask interactableLayer;

    private IInteractable closestInteractable;
    private InputSystem_Actions controls;

    private void Awake()
    {
        controls = new();
        controls.Enable();
    }

    private void OnEnable()
    {
        controls.Player.Interact.performed += OnInteract;
    }

    private void OnDisable()
    {
        controls.Player.Interact.performed -= OnInteract;
    }

    private void Update()
    {
        DetectInteractables();
    }

    private void OnInteract(InputAction.CallbackContext callbackContext) => closestInteractable?.Interact();

    private void DetectInteractables()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, interactRadius, interactableLayer);
        IInteractable nearest = null;
        float minDistance = float.MaxValue;

        foreach (var hit in hits)
        {
            if (hit.TryGetComponent<IInteractable>(out var interactable))
            {
                float distance = Vector3.Distance(transform.position, hit.transform.position);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    nearest = interactable;
                }
            }
        }

        closestInteractable = nearest;

        // This is where the outline enabling should be placed (after iterating all objects and defining the closest to the player)

        // Debug
        // MonoBehaviour mb = closestInteractable as MonoBehaviour;
        // if (mb != null) Debug.Log("Closest interactable: " + mb.gameObject.name);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactRadius);
    }
}

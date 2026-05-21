using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction2D : MonoBehaviour
{
    [SerializeField] private PlayerState playerState;
    [SerializeField] private InteractionPromptUI promptUI;

    private IInteractable currentInteractable;

    private void Awake()
    {
        if (playerState == null)
            playerState = GetComponent<PlayerState>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<IInteractable>(out var interactable))
        {
            currentInteractable = interactable;
            UpdatePrompt();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent<IInteractable>(out var interactable))
        {
            if (currentInteractable == interactable)
            {
                currentInteractable = null;
                promptUI.Hide();
            }
        }
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        if (currentInteractable == null) return;

        if (!currentInteractable.CanInteract(playerState))
        {
            promptUI.Show(currentInteractable.GetPromptText(playerState));
            return;
        }

        currentInteractable.Interact(playerState);
        UpdatePrompt();
    }

    private void UpdatePrompt()
    {
        if (currentInteractable == null)
        {
            promptUI.Hide();
            return;
        }

        promptUI.Show(currentInteractable.GetPromptText(playerState));
    }

    public void RefreshPrompt()
    {
        UpdatePrompt();
    }
}
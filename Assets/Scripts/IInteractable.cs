public interface IInteractable
{
    string GetPromptText(PlayerState playerState);
    bool CanInteract(PlayerState playerState);
    void Interact(PlayerState playerState);
}
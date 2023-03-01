public interface IInteractable
{
    void Interact();
    public string InteractMessage { get; set; }
    public bool IsInteractable { get; set; }
}
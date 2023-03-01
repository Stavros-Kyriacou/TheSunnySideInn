using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    void Interact();
    public string InteractMessage { get; set; }
    public bool IsInteractable { get; set; }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    // Displayed message of when player is looking at an interactable
    public string promptMessage;

    public void BaseInteract() {
        Interact();
    }

    protected virtual void Interact() {

    }
}

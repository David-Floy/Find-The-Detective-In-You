
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class DialogueTrigger : MonoBehaviour
{
    public bool active = false;
    public static bool inTrigger { get; private set; } = false;
    public static GameObject Trigger { get; private set; } 
    
    public static IInteractable Interactable { get; private set; }

    private void OnTriggerStay(Collider other)
    {
        if (active)
        {
            inTrigger = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (active)
        {
            Trigger = this.GameObject();
            PlayerMovement.lockMovement = true;
            GetInteractable();
            Interactable.Interact();
        }
    }

    private static void GetInteractable()
    { 
        Trigger.TryGetComponent(out IInteractable interactable);
        Interactable = interactable;
    }
    

    private void OnTriggerExit(Collider other)
    {
        inTrigger = false;
    }
}

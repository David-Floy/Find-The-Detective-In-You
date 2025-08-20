
using DIALOGUE;
using UnityEngine;

public interface IInteractable
{
    public void Interact();
}


public class PlayerInteractable : MonoBehaviour
{
    
    private bool noteVis = false;
    public static bool inMenu = false;
    public CanvasGroup setting;
    public CanvasGroup options;

    public Transform InteractorSource; 
    public static float interactRange = 1.5f;
    
    private PlayerInputs interact = null;

    [Header("Images")] 
    public BigImage image1;
    public BigImage image2;
    public BigImage image3;
    public BigImage image4;
    public BigImage image5;
    public BigImage image6;
    public BigImage image7;
    
    

    public static bool InStartMenu = false;

    private AudioClip openNotebook;
    private AudioClip closeNotebook;
    private void Awake()
    {
        interact = new PlayerInputs();
        openNotebook = Resources.Load<AudioClip>("Audio/openNote");
        closeNotebook = Resources.Load<AudioClip>("Audio/closingNote");
    }

    private void OnEnable()
    {
        interact.Enable();
    }

    private void OnDisable()
    {
        interact.Disable();
    }
    void Update()
    {
        Ray r = new Ray(InteractorSource.position, InteractorSource.forward);
        
        SelectHighlight.HoverHighlight(r);
        
        if (!InStartMenu)
        {


            if (interact.Player.Interact.triggered && !inMenu && !noteVis)
            {
                PlayerInteract(r);
            }
            else if (interact.Player.Notebook.triggered && !noteVis)
            {
                    SoundManager.instance.PlaySound(openNotebook);
                    NoteManager.instance.notebookContainer.Show();
                    PlayerMovement.LockPlayerCursorVisable();
                    noteVis = true;
                
            }
            else if ((interact.Player.Notebook.triggered || interact.Player.Escape.triggered) && noteVis)
            {
                CloseAll();
                    SoundManager.instance.PlaySound(closeNotebook);
                    NoteManager.instance.notebookContainer.Hide();
                    PlayerMovement.UnLockPlayer();
                    noteVis = false;
                
                
            }
            else if (interact.Player.Escape.triggered && (DialogueSystem.instance.isRunningConversation || Lupe.instance.InLupe))
            {
                Lupe.instance.ExitLupe();
                DialogueSystem.instance.conversationManager.StopConversation();
                ChoicePanel.instance.HideByEsc();
                InputPanel.instance.Hide();

                NPCInteractable.isRunning = false;
                PlayerMovement.UnLockPlayer();
            }
            else if (interact.Player.Escape.triggered)
            {


                if (setting.alpha > 0 || options.alpha > 0)
                {
                    PlayerMovement.UnLockPlayer();
                    inMenu = false;
                    CanvasConrol.Hide(setting);
                    CanvasConrol.Hide(options);
                }
                else
                {
                    PlayerMovement.LockPlayerCursorVisable();
                    inMenu = true;
                    CanvasConrol.Show(setting);
                }
            }
        }

    }
    

    private void PlayerInteract(Ray r)
    {
        
        if (Physics.Raycast(r, out RaycastHit hitInfo, interactRange))
        {
            if (hitInfo.collider.gameObject.TryGetComponent(out IInteractable interactObj))
            {
                interactObj.Interact();
            }
            else if (DialogueTrigger.inTrigger)
            {
                DialogueTrigger.Interactable.Interact();
            
            }
            
        }else if (DialogueTrigger.inTrigger)
        {
            DialogueTrigger.Interactable.Interact();
        }
    }

    public void InMenuFalse()
    {
        inMenu = false;
    }


    private void CloseAll()
    {
        image1.Hide();
        image2.Hide();
        image3.Hide();
        image4.Hide();
        image5.Hide();
        image6.Hide();
        image7.Hide();
    }
    
    
    
    
    
    
    
    
    //Old Interactor
   /* private void InteractNpc()
    {
        Collider[] colliderArray = Physics.OverlapSphere(transform.position, interactRange);
            foreach (Collider collider in colliderArray) 
            {
                if (collider.TryGetComponent(out NPCInteractable npcInteractable))
                {
                    npcInteractable.Interact();
                    //Invoke(nameof(PlayerInteract.restInteract), 3f);
                }
            }
    }

    private void InteractProp()
    {
        Collider[] colliderArray = Physics.OverlapSphere(transform.position, interactRange);
        foreach (Collider collider in colliderArray) 
        {
            if (collider.TryGetComponent(out PropInteractable propInteractable))
            {
                propInteractable.Interact();
                //Invoke(nameof(PlayerInteract.restInteract), 3f);
            }
        }
    }*/
}


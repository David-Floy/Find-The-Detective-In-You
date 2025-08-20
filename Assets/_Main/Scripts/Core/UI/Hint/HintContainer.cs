using UnityEngine;

/// <summary>
/// Indexing of HintContainer
/// </summary>
public class HintContainer : MonoBehaviour
{
    public static HintContainerManager HintBox = null;

    public void Awake()
    {
        HintBox = Utility.FindByTagName(GameObject.FindGameObjectsWithTag("Manager"), "HintManager")
            .GetComponent<HintContainerManager>();
        Debug.Log(HintBox.name);
        
    }
    
}

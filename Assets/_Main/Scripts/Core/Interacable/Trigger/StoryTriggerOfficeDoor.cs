

using DIALOGUE;
using Unity.VisualScripting;
using UnityEngine;

public class StoryTriggerOfficeDoor : MonoBehaviour
{

    public  void OnTriggerEnter(Collider other)
    {
        if (ItemList.AllItemsTrue())
        {
            Utility.FindByTagName(TagArray.manager, "ChapterManager").GetComponent<ChapterMannager>().AdvanceStory(1);
        }

    }




}

using System.Threading.Tasks;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;


public class HintContainerManager : MonoBehaviour
{

        public GameObject root;
       
        

        
        public void ShowHint(string hint)
        {
           root.GetComponentInChildren<TextMeshProUGUI>().text = hint;
            root.GetComponent<CanvasGroup>().alpha = 1;
        }

        public void Hide()
        {
            root.GetComponent<CanvasGroup>().alpha = 0;
        }

        public async Task ShowHintFor(string hint, int time)
        {
            ShowHint(hint);
            await Task.Delay(time);
            Hide();
            
        }


}

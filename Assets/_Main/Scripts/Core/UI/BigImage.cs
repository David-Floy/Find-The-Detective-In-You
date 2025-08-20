
using UnityEngine;

public class BigImage : MonoBehaviour
{
    public static bool isVis;

    public void Show()
    {
        isVis = true;
        CanvasConrol.Show(gameObject.GetComponent<CanvasGroup>());
    }

    public void Hide()
    {
        CanvasConrol.Hide(gameObject.GetComponent<CanvasGroup>());
        isVis = false;
    }
    
}

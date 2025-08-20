using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndSequence : MonoBehaviour
{
    public GameObject Light1;
    public GameObject Light2;
    public GameObject Light3;
    public GameObject Light4;
    public GameObject Light5;
    public GameObject Light6;


    public void CountControl(int count)
    {
        switch (count)
        {
            case 1:
                Light1.SetActive(true);
                Light2.SetActive(false);
                Light3.SetActive(false);
                Light4.SetActive(false);
                Light5.SetActive(false);
                Light6.SetActive(false);
                break;
            case 2:
                Light1.SetActive(false);
                Light2.SetActive(true);
                Light3.SetActive(false);
                Light4.SetActive(false);
                Light5.SetActive(false);
                Light6.SetActive(false);
                break;
            case 3:
                Light1.SetActive(false);
                Light2.SetActive(false);
                Light3.SetActive(true);
                Light4.SetActive(false);
                Light5.SetActive(false);
                Light6.SetActive(false);
                break;
            case 4:
                Light1.SetActive(false);
                Light2.SetActive(false);
                Light3.SetActive(false);
                Light4.SetActive(true);
                Light5.SetActive(false);
                Light6.SetActive(false);
                break;
            case 5:
                Light1.SetActive(false);
                Light2.SetActive(false);
                Light3.SetActive(false);
                Light4.SetActive(false);
                Light5.SetActive(true);
                Light6.SetActive(false);
                break;
            case 6:
                Light1.SetActive(false);
                Light2.SetActive(false);
                Light3.SetActive(false);
                Light4.SetActive(false);
                Light5.SetActive(false);
                Light6.SetActive(true);
                break;
        }

    }
    
}

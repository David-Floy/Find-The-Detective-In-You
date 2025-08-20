
using UnityEngine;


public class Teleporter : MonoBehaviour
{
    public bool active = true;
    public GameObject player;
    public Transform teleportDestination;
    
    private void OnTriggerStay(Collider other)
    {
        if (active)
        {
            player.transform.position = teleportDestination.position;
        }
       
    }

    public void Aktivate(bool state)
    {
        active = state;
        gameObject.GetComponent<MeshRenderer>().enabled = state;

    }
    
    
}

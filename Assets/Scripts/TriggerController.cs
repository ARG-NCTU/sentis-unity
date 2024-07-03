using UnityEngine;
using System.Collections.Generic;

public class TriggerController : MonoBehaviour
{
    public MonoBehaviour scriptToControl;
    public bool iscarrot = false;

    void Start()
    {
        if (scriptToControl != null)
        {
            scriptToControl.enabled = false;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && scriptToControl != null)
        {
            scriptToControl.enabled = true;
            Debug.Log("Player entered the trigger zone. NPC script enabled.");
        }

        // if (other.CompareTag("Carrot"))
        // {
        //     iscarrot = true;
        //     Debug.Log("Carrot entered the trigger zone.");
        // }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && scriptToControl != null)
        {
            scriptToControl.enabled = false;
            Debug.Log("Player exited the trigger zone. NPC script disabled.");
        }
    }
}
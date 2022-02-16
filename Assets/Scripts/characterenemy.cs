using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class characterenemy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    { 
    }

    // Update is called once per frame
    void Update()
    {
    }

    // OnTriggerEnter is called whenever an object enters the trigger zone.
    void OnTriggerEnter(Collider item)
    {
        switch (item.name) {
            case "Character_Player":
            Debug.Log("Player has entered the zone!");
            break;
        default:
            Debug.Log("Something unimportant has entered the zone.");
            break;
        }
    }
    void OnTriggerExit(Collider item)
    {
        switch (item.name)
        {
            case "Character_Player":
                Debug.Log("Player has exited the zone!");
                break;
            default:
                Debug.Log("Something unimportant has exited the zone.");
                break;
        }
    }
}
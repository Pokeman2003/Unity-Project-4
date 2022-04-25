using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class characterenemy : MonoBehaviour
{
    public int patrolroutecount = 1;
    // Start is called before the first frame update
    void Start()
    {
        name = "Character_Enemy"; //Renames to a generic enemy, so the enemy can't distinguish them. Rockets don't have eyes!
        initPatrol();
    }

    void initPatrol() //Picks a patrol and goes with it.
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
            case "Explosion":
                Debug.Log("There was an explosion somewhere around me!");
                break;
            case "Rocket":
                Debug.Log("A rocket has entered my hearing range!");
                break;
            case "Bullet":
                Debug.Log("A bullet whizzed past me!");
                break;
            default:
                Debug.Log("Something unimportant has entered the zone. " + item.name);
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
            case "Rocket":
                Debug.Log("Relaxing, the rocket has left my range.");
                break;
            case "Bullet":
                Debug.Log("Where did it go?");
                break;
            default:
                Debug.Log("Something unimportant has exited the zone. " + item.name);
                break;
        }
    }

    void OnCollisionEnter(Collision item)
    {
        switch (item.gameObject.name)
        {
            case "Explosion":
                Debug.Log("I'm DEAD!");
                Destroy(this);
                break;
            case "Bullet":
                Debug.Log("I've been wounded!");
                break;
            default:
                Debug.Log("Someone touched me but I'm not dead. " + item.gameObject.name);
                break;
        }
    }
}

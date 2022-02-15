using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class characterplayer : MonoBehaviour
{
    //Speeds.
    public float movementSpeed = 16f; //A little call back, ROBLOXians move at 16 studs a second.
    public float rotationSpeed = 48f; //For now, a slow, but manageable speed. I don't want a cap, I want mouse control in the future. Very important given the verticality of my game.

    //Horizontal and Vertical inputs. This is a new way to handle that, and explorercam.cs could probably use it, but explorercam.cs is just a generic placeholder.
    private float verticalInput;
    private float horizontalInput;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Intellisense really screwed me over. I'm really considering refactoring verticalInput and horizontalInput to drop INPUT because it kept trying to autocomplete to that instead of my intended Input.GetAxis.
        verticalInput = Input.GetAxis("Vertical") * movementSpeed * Time.deltaTime;
        horizontalInput = Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime;

        //Being the "smart" guy I am, I get to drop Time.deltaTime from this calculation by doing it sooner.
        transform.Translate(Vector3.forward * verticalInput);
        transform.Rotate(Vector3.up * horizontalInput);
    }
}

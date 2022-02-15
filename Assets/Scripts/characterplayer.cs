using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class characterplayer : MonoBehaviour
{
    //Speeds.
    public float movementSpeed = 16f; //A little call back, ROBLOXians move at 16 studs a second.
    public float rotationSpeed = 96f; //For now, a slow, but manageable speed. I don't want a cap, I want mouse control in the future. Very important given the verticality of my game.
    private float speedLimiter = .5f;

    //Horizontal and Vertical inputs. This is a new way to handle that, and explorercam.cs could probably use it, but explorercam.cs is just a generic placeholder.
    private float verticalIn;
    private float horizontalIn;

    private Rigidbody rB;


    // Start is called before the first frame update
    void Start()
    {
        rB = GetComponent<Rigidbody>(); //Grabs the Rigidbody component and puts it into rB.
    }

    // Update is called once per frame
    void Update() 
    {
        //Intellisense really screwed me over. I'm really considering refactoring verticalInput and horizontalInput to drop INPUT because it kept trying to autocomplete to that instead of my intended Input.GetAxis.
        verticalIn = Input.GetAxis("Vertical") * movementSpeed * speedLimiter; //* Time.deltaTime;
        horizontalIn = Input.GetAxis("Horizontal") * rotationSpeed * speedLimiter; //* Time.deltaTime;

        /*//No longer necessary, in lieu of the new movement system.
        //Being the "smart" guy I am, I get to drop Time.deltaTime from this calculation by doing it sooner.
        transform.Translate(Vector3.forward * verticalInput);
        transform.Rotate(Vector3.up * horizontalInput);*/
    }
    void FixedUpdate() // Run at every physics update. AND SUDDENLY IT STOPS WORKING GOD FUCKING DAMN IT. THANK YOU UNITY.
    {
        //Rotational vector.
        Vector3 rotVec = Vector3.up * horizontalIn;
        
        //Quaternions, my biggest enemy. Applies a angle to the character(?).
        Quaternion angleRot = Quaternion.Euler(rotVec * Time.fixedDeltaTime);

        //Moves and rotates the character based on key inputs.
        rB.MovePosition(transform.position + transform.forward * verticalIn* Time.fixedDeltaTime);
        rB.MoveRotation(rB.rotation * angleRot);

        //Direct copies from the book for comparison.
        //Vector3 rotation = Vector3.up * horizontalIn;
        //Quaternion angleRot = Quaternion.Euler(rotation * Time.fixedDeltaTime);
        //_rB.MovePosition(this.transform.position + this.transform.forward * verticalIn* Time.fixedDeltaTime);
        //_rB.MoveRotation(_rB.rotation * angleRot);
    }
}

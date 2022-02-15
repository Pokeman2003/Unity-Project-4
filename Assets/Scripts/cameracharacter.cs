using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameracharacter : MonoBehaviour
{
    //The offset and the target.
    public Vector3 viewOffset = new Vector3(0f, 1.6f, -8f);
    private Transform targetObj;

    // Start is called before the first frame update
    void Start()
    {
        //Find the object. Secure its transformation. Store in targetObj.
        targetObj = GameObject.Find("Character_Player").transform;
    }

    // LastUpdate is called once per frame, at the end. Unfortunately, Fixed and LastUpdate do not work for me.
    void Update()
    {
        //Transform the position to behind the target, and then rotate the camera to look at the object itself.
        transform.position = targetObj.TransformPoint(viewOffset);
        transform.LookAt(targetObj);
    }
}

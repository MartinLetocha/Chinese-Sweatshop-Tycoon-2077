using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCameraScript : MonoBehaviour
{
    public Transform camera;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Quaternion rotation = Quaternion.LookRotation(camera.position - transform.position, Vector3.up);
        transform.eulerAngles = new Vector3(camera.eulerAngles.x, rotation.eulerAngles.y - 180, rotation.eulerAngles.z);
        //Debug.Log(rotation + " " + transform.rotation);
    }
}

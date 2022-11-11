using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ConveyorBelt : MonoBehaviour
{
    public GameObject buttonPopup;
    public float range;
    public KeyCode interact = KeyCode.E;
    private float lastRad;// remove after debug
    void Start()
    {
        buttonPopup.SetActive(false);
        lastRad = GetComponent<SphereCollider>().radius;
    }
    
    void Update()
    {
        if (Input.GetKeyDown(interact))
        {
            //start the mini-game ig
        }
        
        
        
        
        
        
        //move to start
        GetComponent<SphereCollider>().radius = range + lastRad;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            buttonPopup.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            buttonPopup.SetActive(false);
        }
    }
}

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
    public MinigamePlayer mini;
    void Start()
    {
        buttonPopup.SetActive(false);
    }
    
    void Update()
    {

        if (buttonPopup.activeSelf == true && Input.GetKeyDown(interact) && mini.gameUi.activeSelf == false)
        {
            mini.StartMinigame(true);
        }
        else if (Input.GetKeyDown(interact) && mini.gameUi.activeSelf == true)
        {
            mini.StopMinigame();
        }
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

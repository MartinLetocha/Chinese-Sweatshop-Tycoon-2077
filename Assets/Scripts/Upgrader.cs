using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Upgrader : MonoBehaviour
{
    public GameObject buttonPopup;
    public float range;
    public KeyCode interact = KeyCode.E;
    public CanvasGroup upgraderUI;
    private float lastRad;// remove after debug
    private int skillLevel = 0;
    public TMP_Text _btnText;
    private int maxLevel = 1; //change depending on highest level in scriptable objects
    void Start()
    {
        buttonPopup.SetActive(false);
        lastRad = GetComponent<SphereCollider>().radius;
        PlayerPrefs.SetInt("skill", 0);
        skillLevel = 0;
        _btnText.text = $"Skill level: {skillLevel}";
    }
    
    void Update()
    {

        if (buttonPopup.activeSelf && Input.GetKeyDown(interact) && upgraderUI.alpha == 0)
        {
            upgraderUI.alpha = 1f;
        }
        else if (buttonPopup.activeSelf && Input.GetKeyDown(interact) && upgraderUI.alpha == 1f)
        {
            upgraderUI.alpha = 0;
        }
        //move to start later
        GetComponent<SphereCollider>().radius = range + lastRad;
    }

    public void IncreaseSkillLevel()
    {
        if (skillLevel + 1 > maxLevel)
        {
            _btnText.text = $"You've reached the max level ({maxLevel})";
            return;
        }
        PlayerPrefs.SetInt("skill", skillLevel+1);
        skillLevel++;
        _btnText.text = $"Skill level: {skillLevel}";
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

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseScript : MonoBehaviour
{
    public CanvasGroup menu;
    public Slider slider;
    public KeyCode open = KeyCode.Escape;
    public KeyCode debug = KeyCode.BackQuote;
    public TimePass forDebug;
    public void Start()
    {
        menu.alpha = 0;
        menu.blocksRaycasts = false;
    }

    public void Resume()
    {
        Time.timeScale = 1;
        menu.alpha = 0;
        menu.blocksRaycasts = false;
    }

    public void Quit()
    {
        Application.Quit();
    }
    public void OnChangeSlider()
    {
        forDebug.markiplier = slider.value;
    }
    public void Update()
    {
        if (Input.GetKeyDown(open) && menu.alpha != 1f)
        {
            Time.timeScale = 0;
            menu.alpha = 1;
            menu.blocksRaycasts = true;
            slider.gameObject.SetActive(false);
        }
        else if (Input.GetKeyDown(debug) && menu.alpha != 1f)
        {
            Time.timeScale = 0;
            menu.alpha = 1;
            menu.blocksRaycasts = true;
            slider.gameObject.SetActive(true);
        }
        else if (Input.GetKeyDown(open) || Input.GetKeyDown(debug))
        {
            Resume();
        }
    }
}

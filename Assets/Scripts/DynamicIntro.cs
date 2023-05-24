using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DynamicIntro : MonoBehaviour
{
    public Canvas canvas;

    public TMP_Text text;

    public Animator animator;

    public MinigamePlayer player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RealStart()
    {
        player.RealStart();
    }
    public void StartIntro(string textP)
    {
        text.text = textP;
        animator.SetTrigger("Activate");
    }
}

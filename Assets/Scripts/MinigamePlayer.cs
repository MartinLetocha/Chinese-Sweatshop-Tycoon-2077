using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MinigamePlayer : MonoBehaviour
{
    public GameObject player;
    public Sprite upKey;
    public Sprite downKey;
    public Sprite rightKey;
    public Sprite leftKey;
    public Transform skeleton;
    public GameObject gameUi;
    public GameObject emptyKey;
    public MinigameCreatorBase settings;
    public List<Sprite> sprites;
    public List<GameObject> arrows;
    public float spaceBetween;
    public List<KeyCode> inputs;
    public KeyCode upArrow = KeyCode.UpArrow;
    public KeyCode downArrow = KeyCode.DownArrow;
    public KeyCode leftArrow = KeyCode.LeftArrow;
    public KeyCode rightArrow = KeyCode.RightArrow;

    void Start()
    {
        
    }
    
    void Update()
    {
        if (inputs.Count == 0)
        {
            StopMinigame();
        }
        else if (gameUi.activeSelf == true && Input.GetKeyDown(inputs[0]))
        {
            inputs.Remove(inputs[0]);
            Destroy(arrows[0]);
            arrows.Remove(arrows[0]);
            if (arrows.Count != 0)
            {
                float arrowZeroPos = arrows[0].transform.localPosition.x;
                for (int i = 0; i < arrows.Count; i++)
                {
                    arrows[i].transform.localPosition =
                        new Vector3(
                            arrows[i].transform.localPosition.x - arrowZeroPos,
                            arrows[i].transform.localPosition.y, arrows[i].transform.localPosition.z);
                }
            }
        }
    }

    public void StartMinigame()
    {
        player.GetComponent<PlayerMovement>().enabled = false;
        gameUi.SetActive(true);
        LoadPlan();
        for (int i = 0; i < sprites.Count; i++)
        {
            var position = skeleton.position + new Vector3(skeleton.GetComponent<RectTransform>().rect.width * i + spaceBetween * i, 0, 0);
            var a = Instantiate(emptyKey, position, Quaternion.identity, gameUi.transform);
            a.GetComponent<Image>().sprite = sprites[i];
            a.name = sprites[i].name;
            a.transform.localScale = new Vector3(a.GetComponent<Image>().sprite.rect.width / 100, a.GetComponent<Image>().sprite.rect.height / 100, a.transform.localScale.z);
            arrows.Add(a);
        }
    }

    public void StopMinigame()
    {
        foreach (var item in arrows)
        {
            Destroy(item);
        }
        inputs.Clear();
        arrows.Clear();
        sprites.Clear();
        player.GetComponent<PlayerMovement>().enabled = true;
        gameUi.SetActive(false);
    }

    private void LoadPlan()
    {
        string[] plan = settings.plan.text.Split(',');
        foreach (var item in plan)
        {
            switch (item)
            {
                case "down":
                    sprites.Add(downKey);
                    inputs.Add(downArrow);
                    break;
                case "up":
                    sprites.Add(upKey);
                    inputs.Add(upArrow);
                    break;
                case "left":
                    sprites.Add(leftKey);
                    inputs.Add(leftArrow);
                    break;
                case "right":
                    sprites.Add(rightKey);
                    inputs.Add(rightArrow);
                    break;
            }
        }
    }
}

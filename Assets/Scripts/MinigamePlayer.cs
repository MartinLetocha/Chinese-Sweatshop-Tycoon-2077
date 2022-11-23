using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinigamePlayer : MonoBehaviour
{
    public Sprite upKey;
    public Sprite downKey;
    public Sprite rightKey;
    public Sprite leftKey;
    public Transform skeleton;
    public GameObject gameUi;
    public GameObject emptyKey;
    public MinigameCreatorBase settings;
    public List<Sprite> sprites;
    void Start()
    {
        
    }
    
    void Update()
    {
        
    }

    public void StartMinigame()
    {
        gameUi.SetActive(true);
        LoadPlan();
        for (int i = 0; i < sprites.Count; i++)
        {
            var position = skeleton.position + new Vector3(skeleton.GetComponent<RectTransform>().rect.width * i, 0, 0);
            var a = Instantiate(emptyKey, position , Quaternion.identity, gameUi.transform);
            a.GetComponent<Image>().sprite = sprites[i];
            a.name = sprites[i].name;
            a.transform.localScale = new Vector3(a.GetComponent<Image>().sprite.rect.width / 250, a.GetComponent<Image>().sprite.rect.height / 250, a.transform.localScale.z);
        }
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
                    break;
                case "up":
                    sprites.Add(upKey);
                    break;
                case "left":
                    sprites.Add(leftKey);
                    break;
                case "right":
                    sprites.Add(rightKey);
                    break;
            }
        }
    }
}

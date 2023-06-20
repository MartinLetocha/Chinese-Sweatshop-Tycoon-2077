using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

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
    public List<Arrow> arrows = new List<Arrow>();
    public float spaceBetween;
    public float positionOffset;
    public KeyCode Up = KeyCode.UpArrow;
    public KeyCode Down = KeyCode.DownArrow;
    public KeyCode Left = KeyCode.LeftArrow;
    public KeyCode Right = KeyCode.RightArrow;

    public TMP_Text money;
    public Animator _playerAnim;

    private Random rd = new();

    public DynamicIntro intro;
    public Image sprite;
    private int spriteIndex = 0;
    private int fakes = 0;
    public class Arrow
    {
        public GameObject GameObject;
        public KeyCode Input;
        public Arrow(GameObject gameObject, KeyCode input)
        {
            GameObject = gameObject;
            Input = input;
        }
    }

    void Start()
    {
        _playerAnim = player.GetComponent<Animator>();
    }
    void Update()
    {
        if (arrows.Count == 0 && gameUi.activeSelf == true)
        {
            StopMinigame();
            float.TryParse(money.text.Replace("yuan", ""), out float cash);
            cash += settings.reward;
            money.text = cash.ToString() + "yuan";
            //money.text = (Convert.ToInt32(money.text.Replace("yuan", "")) + settings.reward).ToString() + "yuan";
            StartMinigame(false); //for sake of loop
        }
        else if (gameUi.activeSelf == true && Input.GetKeyDown(arrows[0].Input))
        {
            Destroy(arrows[0].GameObject);
            arrows.Remove(arrows[0]);
            //Debug.Log(arrows.Count);
            MoveArrows(0, -1 * positionOffset);
            if (settings.images.Count != 0 && fakes == 0)
            {
                spriteIndex++;
                sprite.sprite = settings.images[spriteIndex];
            }
            else if (fakes > 0)
            {
                fakes--;
            }
        }
        else if (gameUi.activeSelf == true && !Input.GetKeyDown(arrows[0].Input) && Input.anyKeyDown)
        {
            if (Input.GetKeyDown(Left))
                InsertArrowAtPosition(MakeArrow(rightKey, Right), 0);
            else if (Input.GetKeyDown(Right))
                InsertArrowAtPosition(MakeArrow(leftKey, Left), 0);
            else if (Input.GetKeyDown(Up))
                InsertArrowAtPosition(MakeArrow(downKey, Down), 0);
            else if (Input.GetKeyDown(Down))
                InsertArrowAtPosition(MakeArrow(upKey, Up), 0);
        }

        if (gameUi.activeSelf == false)
        {
            return;
        }
        if (Input.GetKeyDown(Up))
        {
            _playerAnim.SetTrigger("BeltUp");
        }
        else if (Input.GetKeyDown(Down))
        {
            _playerAnim.SetTrigger("BeltDown");
        }
        else if (Input.GetKeyDown(Right))
        {
            _playerAnim.SetTrigger("BeltRight");
        }
        else if (Input.GetKeyDown(Left))
        {
            _playerAnim.SetTrigger("BeltLeft");
        }
    }

    public void StartMinigame(bool intr)
    {
        sprite.gameObject.SetActive(false);
        spriteIndex = 0;
        int playerlvl = PlayerPrefs.GetInt("skill");
        List<MinigameCreatorBase> settingss = new List<MinigameCreatorBase>();
        for (int i = 0; i <= playerlvl; i++)
        {
            settingss.AddRange(Resources.LoadAll<MinigameCreatorBase>("ScriptableObjects").ToList().Where(x=>x.level == i).ToList());
        }
        
        settings = settingss[rd.Next(0, settingss.Count)];
        if (settings.images.Count != 0)
        {
            sprite.gameObject.SetActive(true);
            sprite.sprite = settings.images[spriteIndex];
        }

        if (intr)
        {
            intro.StartIntro("Work start!");
        }
        else
        {
            RealStart();
        }
    }

    public void RealStart()
    {
        _playerAnim.SetBool("isWalking", false);
        player.GetComponent<PlayerMovement>().enabled = false;
        gameUi.SetActive(true);
        LoadPlan();
    }
    public void StopMinigame()
    {
        foreach (var item in arrows)
        {
            Destroy(item.GameObject);
        }
        arrows.Clear();
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
                    arrows.Add(MakeArrow(downKey, Down));
                    break;
                case "up":
                    arrows.Add(MakeArrow(upKey, Up));
                    break;
                case "left":
                    arrows.Add(MakeArrow(leftKey, Left));
                    break;
                case "right":
                    arrows.Add(MakeArrow(rightKey, Right));
                    break;
            }
        }
        positionOffset = arrows[1].GameObject.transform.localPosition.x;
    }

    private Arrow MakeArrow(Sprite sprite, KeyCode input)
    {
        var position = skeleton.position + new Vector3(skeleton.GetComponent<RectTransform>().rect.width * arrows.Count + spaceBetween * arrows.Count, 0, 0);
        var a = Instantiate(emptyKey, position, Quaternion.identity, gameUi.transform);
        a.GetComponent<Image>().sprite = sprite;
        a.name = sprite.name;
        a.transform.localScale = new Vector3(a.GetComponent<Image>().sprite.rect.width / 100, a.GetComponent<Image>().sprite.rect.height / 100, a.transform.localScale.z);
        return new Arrow(a, input);
    }

    /// <summary>
    /// Moves all arrows starting with the specified index by a certain distance
    /// </summary>
    /// <param name="startingIndex"></param>
    /// <param name="distance">Distance to move by </param>
    private void MoveArrows(int startingIndex, float distance)
    {
        int start = startingIndex % arrows.Count;
        for (int i = startingIndex; i < arrows.Count; i++)
        {
            arrows[i].GameObject.transform.localPosition =
                new Vector3(
                    arrows[i].GameObject.transform.localPosition.x + distance,
                    arrows[i].GameObject.transform.localPosition.y,
                    arrows[i].GameObject.transform.localPosition.z);
        }
    }
    
    private void InsertArrowAtPosition(Arrow arrow, int index)
    {
        fakes++;
        foreach (var item in arrows)
        {
            //Debug.Log($"{item.GameObject.transform.localPosition.x}");
        }
        arrow.GameObject.transform.localPosition = arrows[0].GameObject.transform.localPosition;
        int pos = index % arrows.Count;
        MoveArrows(pos, positionOffset);
        arrows.Insert(pos, arrow);
    }
}


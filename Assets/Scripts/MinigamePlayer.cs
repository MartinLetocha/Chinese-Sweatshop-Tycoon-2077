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
     public List<Arrow> arrows = new List<Arrow>();
     public float spaceBetween;
     public KeyCode upArrow = KeyCode.UpArrow;
     public KeyCode downArrow = KeyCode.DownArrow;
     public KeyCode leftArrow = KeyCode.LeftArrow;
     public KeyCode rightArrow = KeyCode.RightArrow;
 
     public class Arrow
     {
         public GameObject _gameObject;
         public KeyCode _input;
 
         public Arrow(GameObject gameObject, KeyCode input)
         {
             _gameObject = gameObject;
             _input = input;
         }

         public Arrow(GameObject gameObject, KeyCode input, Arrow reverse)
         {
             _gameObject = reverse._gameObject;
             _input = reverse._input;
         }
     }
     
     void Update()
     {
         if (arrows.Count == 0)
         {
             StopMinigame();
         }
         else if (gameUi.activeSelf == true && Input.GetKeyDown(arrows[0]._input))
         {
             Destroy(arrows[0]._gameObject);
             arrows.Remove(arrows[0]);
             if (arrows.Count != 0)
             {
                 float arrowZeroPos = arrows[0]._gameObject.transform.localPosition.x;
                 for (int i = 0; i < arrows.Count; i++)
                 {
                     arrows[i]._gameObject.transform.localPosition =
                         new Vector3(
                             arrows[i]._gameObject.transform.localPosition.x - arrowZeroPos,
                             arrows[i]._gameObject.transform.localPosition.y, arrows[i]._gameObject.transform.localPosition.z);
                 }
             }
         }
         else if (gameUi.activeSelf == true && !Input.GetKeyDown(arrows[0]._input) && Input.anyKeyDown)
         {
             Debug.Log("here");
         }
     }
 
     public void StartMinigame()
     {
         player.GetComponent<PlayerMovement>().enabled = false;
         gameUi.SetActive(true);
         LoadPlan();
     }

     public void StopMinigame()
     {
         foreach (var item in arrows)
         {
             //Destroy(item);
             Destroy(item._gameObject);
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
                     MakeArrow(downKey, downArrow);
                     break;
                 case "up":
                     MakeArrow(upKey, upArrow);
                     break;
                 case "left":
                     MakeArrow(leftKey, leftArrow);
                     break;
                 case "right":
                     MakeArrow(rightKey, rightArrow);
                     break;
             }
         }
     }
 
     private void MakeArrow(Sprite sprite, KeyCode input)
     {
         var position = skeleton.position + new Vector3(skeleton.GetComponent<RectTransform>().rect.width * arrows.Count + spaceBetween * arrows.Count, 0, 0);
         var a = Instantiate(emptyKey, position, Quaternion.identity, gameUi.transform);
         a.GetComponent<Image>().sprite = sprite;
         a.name = sprite.name;
         a.transform.localScale = new Vector3(a.GetComponent<Image>().sprite.rect.width / 100, a.GetComponent<Image>().sprite.rect.height / 100, a.transform.localScale.z);
         arrows.Add(new Arrow(a, input));
     }
 }

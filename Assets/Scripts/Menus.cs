using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menus : MonoBehaviour
{
    void Start()
    {
        
    }
    void Update()
    {
        
    }
    public void Play()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        //loads a scene with a build index above this one. Main Menu should always be 0.
    }

    public void Quit()
    {
        Application.Quit();
        //Quits the app when BUILT, doesn't close the editor.
    }
}

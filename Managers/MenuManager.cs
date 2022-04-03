using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{

    public void Start()
    {
        Time.timeScale = 1f;
    }
    public void Exit()
    {
        Application.Quit();
    }

    public void Play()
    {
        FindObjectOfType<LevelManager>().PlayAnimation();
    }
}

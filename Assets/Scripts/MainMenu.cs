using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    private GameObject rules;
    private void Start()
    {
        rules = GameObject.Find("Rules");
        rules.SetActive(false);
    }
    public void StartGame()
    {
        LevelLoader.instance.LoadLevel(1);
    }
    public void Exit()
    {
        Application.Quit();
    }
    public void ShowRules()
    {
        rules.SetActive(true);
    }
}

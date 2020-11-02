using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndScreen : MonoBehaviour
{
    public void OpenLink(string link)
    {
        Application.OpenURL(link);
    }
    public void ReturnToMainMenu()
    {
        LevelLoader.instance.LoadMainMenu();
    }
}

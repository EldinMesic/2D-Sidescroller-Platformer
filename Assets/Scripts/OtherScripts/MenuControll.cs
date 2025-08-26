using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuControll : MonoBehaviour
{
    public void OpenLevel(string level)
    {
        SceneManager.LoadScene(level);
    }

    public void CloseGame()
    {
        Application.Quit();
    }

}

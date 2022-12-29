using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu_Bttns : MonoBehaviour
{
    public void LoadScene(string sCene)
    {
        SceneManager.LoadScene(sCene);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}

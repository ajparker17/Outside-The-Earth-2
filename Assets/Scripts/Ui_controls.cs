using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Ui_controls : MonoBehaviour
{
 
    public void QuitGame()
    {
        Application.Quit();
    }

    public void LevelSelector(int levelIndex)
    {
        SceneManager.LoadScene(levelIndex);
    }
}

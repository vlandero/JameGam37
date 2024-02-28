using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinLossMenu : MonoBehaviour
{
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void Exit()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void NextLevel()
    {
        string actualScene = SceneManager.GetActiveScene().name.ToString();
        string sceneName = "Level" + ((int)char.GetNumericValue(actualScene[5]) + 1);
        if (SceneManager.GetSceneByName(sceneName).IsValid())
            SceneManager.LoadScene(sceneName);
        else
            SceneManager.LoadScene("MainMenu");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private Button[] levelButtons;
    [SerializeField]
    private SavedData savedData;

    private void Awake()
    {
        for (int i = 0; i < levelButtons.Length; i++)
        {
            levelButtons[i].interactable = savedData.unlockedLevels[i+1];
        }
        this.transform.GetChild(1).gameObject.SetActive(false);
        this.transform.GetChild(0).gameObject.SetActive(true);
    }

    public void ShowLevels()
    {
        this.transform.GetChild(0).gameObject.SetActive(false);
        this.transform.GetChild(1).gameObject.SetActive(true);
    }
    public void ShowStart()
    {
        this.transform.GetChild(1).gameObject.SetActive(false);
        this.transform.GetChild(0).gameObject.SetActive(true);
    }
    public void PlayLevel(int levelNumber)
    {
        string levelName = "Level" + levelNumber;
        SceneManager.LoadScene(levelName);
    }
    public void Quit()
    {
        EditorApplication.ExitPlaymode();
        Application.Quit();
    }
}

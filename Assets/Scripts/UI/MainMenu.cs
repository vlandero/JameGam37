using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private Button[] levelButtons;
    [SerializeField] private TextMeshProUGUI congratsText;

    private void Awake()
    {
        PlayerPrefs.SetInt("Level0", 1);
        PlayerPrefs.SetInt("Level1", 1);
        if (PlayerPrefs.GetInt("Level2") == 0)
        {
            PlayerPrefs.SetInt("Level2", 0);
        }
        if (PlayerPrefs.GetInt("Level3") == 0)
        {
            PlayerPrefs.SetInt("Level3", 0);
        }
        PlayerPrefs.Save();
        congratsText.gameObject.SetActive(false);
        for (int i = 0; i < levelButtons.Length; i++)
        {
            levelButtons[i].interactable = PlayerPrefs.GetInt("Level" + (i+1)) == 1;
        }
        if (PlayerPrefs.GetInt("Level3") == 1)
        {
            congratsText.gameObject.SetActive(true);
        }
        transform.GetChild(1).gameObject.SetActive(false);
        transform.GetChild(0).gameObject.SetActive(true);
    }

    public void ShowLevels()
    {
        transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(1).gameObject.SetActive(true);
    }
    public void ShowStart()
    {
        transform.GetChild(1).gameObject.SetActive(false);
        transform.GetChild(0).gameObject.SetActive(true);
    }
    public void PlayLevel(int levelNumber)
    {
        string levelName = "Level" + levelNumber;
        SceneManager.LoadScene(levelName);
    }
    public void Quit()
    {
        Application.Quit();
    }
}

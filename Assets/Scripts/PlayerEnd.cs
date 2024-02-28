using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerEnd : MonoBehaviour
{
    [SerializeField]
    private GameObject winPanel;
    [SerializeField]
    private GameObject lossPanel;
    [SerializeField]
    private InitializeBackgrounds panel;
    [SerializeField]
    private SavedData data;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("loss"))
        {
            lossPanel.SetActive(true);
            StopTime();
        }
        else if (collision.CompareTag("end"))
        {
            string actualScene = SceneManager.GetActiveScene().name.ToString();
            int nr = (int)char.GetNumericValue(actualScene[5]) + 1;
            string sceneName = "Level" + nr;
            if (SceneManager.GetSceneByName(sceneName).IsValid())
                data.unlockedLevels[nr] = true;
            winPanel.SetActive(true);
            StopTime();
        }
    }
    public void StopTime()
    {
        panel.rollingSpeed = 0;
        this.gameObject.SetActive(false);
    }
}

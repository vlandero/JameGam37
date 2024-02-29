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
    [SerializeField]
    private ParticleSystem deathParticleSystem;

    private PlayerController playerController;
    private Color spriteColor;
    private void Awake()
    {
        deathParticleSystem.Stop();
    }
    private void Start()
    {
        playerController = GetComponent<PlayerController>();
        spriteColor = playerController.spriteRenderer.color;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("loss"))
        {
            Lose();
        }
        else if (collision.CompareTag("end"))
        {
            string actualScene = SceneManager.GetActiveScene().name.ToString();
            int nr = (int)char.GetNumericValue(actualScene[5]) + 1;
            string sceneName = "Level" + nr;
            if (SceneManager.GetSceneByName(sceneName).IsValid())
            {
                data.unlockedLevels[nr] = true;
            }
            Win();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("enemy"))
        {
            collision.gameObject.GetComponent<Enemy>().animator.SetTrigger("Attack");
            Invoke(nameof(PlayKillAnimation), .2f);
            Invoke(nameof(Lose), .5f);
        }
    }
    private void PlayKillAnimation()
    {
        playerController.spriteRenderer.color = new Color(spriteColor.r, spriteColor.g, spriteColor.b, 0);
        deathParticleSystem.Play();
    }
    public void Win()
    {
        winPanel.SetActive(true);
        StopTime();
    }

    public void Lose()
    {
        lossPanel.SetActive(true);
        StopTime();
    }
    public void StopTime()
    {
        playerController.StopTire();
        gameObject.SetActive(false);
        playerController.obstacleManager.DeactivateAllObjecets();
    }
}

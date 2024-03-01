using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;

public class TimeLineSkip : MonoBehaviour
{
    private PlayableDirector playableDirector;
    void Start()
    {
        playableDirector = GetComponent<PlayableDirector>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.KeypadEnter) ||
            Input.GetKeyDown(KeyCode.Space) ||
            Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Level1");
        }

        // Verific?m dac? timeline-ul s-a oprit (s-a terminat)
        if (playableDirector.state != PlayState.Playing)
        {
            SceneManager.LoadScene("Level1");
        }
    }
}

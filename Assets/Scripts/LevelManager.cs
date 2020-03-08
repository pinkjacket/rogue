using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    public float transferDelay = 3f;
    public string nextLevelName;
    public bool isPaused;

    private void Awake() {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            PauseUnpause();
        }
    }

    public IEnumerator LevelEnd() {
        AudioManager.instance.PlayVictoryMusic();
        PlayerController.instance.canMove = false;
        UIController.instance.StartFadeToBlack();
        yield return new WaitForSeconds(transferDelay);
        SceneManager.LoadScene(nextLevelName);
    }

    public void PauseUnpause() {
        if (!isPaused) {
            UIController.instance.pauseMenu.SetActive(true);
            isPaused = true;
            Time.timeScale = 0f;
        } else {
            UIController.instance.pauseMenu.SetActive(false);
            isPaused = false;
            Time.timeScale = 1f;
        }
    }
}

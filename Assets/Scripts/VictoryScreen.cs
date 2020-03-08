using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryScreen : MonoBehaviour
{
    public float waitForInput = 2f;
    public GameObject anyKeyText;
    public string mainMenuScene;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(waitForInput > 0) {
            waitForInput -= Time.deltaTime;
            if(waitForInput <= 0) {
                anyKeyText.SetActive(true);
            }
        } else {
            if (Input.anyKeyDown) {
                SceneManager.LoadScene(mainMenuScene);
            }
        }
    }
}

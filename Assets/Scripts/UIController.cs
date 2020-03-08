﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public static UIController instance;
    public Slider healthSlider;
    public Text healthText;
    public GameObject deathScreen;
    public Image fadeScreen;
    public float fadeSpeed;
    private bool fadeToBlack, fadeFromBlack;
    public string newGameScene, mainMenuScene;

    private void Awake() {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        fadeFromBlack = true;
        fadeToBlack = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (fadeFromBlack) {
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b, Mathf.MoveTowards(fadeScreen.color.a, 0f, fadeSpeed * Time.deltaTime));
            if (fadeScreen.color.a == 0f) {
                fadeFromBlack = false;
            }
        }

        if (fadeToBlack) {
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b, Mathf.MoveTowards(fadeScreen.color.a, 1f, fadeSpeed * Time.deltaTime));
            if (fadeScreen.color.a == 1f) {
                fadeToBlack = false;
            }
        }
    }
    public void StartFadeToBlack() {
        fadeToBlack = true;
        fadeFromBlack = false;
    }

    public void NewGame() {
        SceneManager.LoadScene(newGameScene);
    }

    public void BackToMenu() {
        SceneManager.LoadScene(mainMenuScene);
    }
}

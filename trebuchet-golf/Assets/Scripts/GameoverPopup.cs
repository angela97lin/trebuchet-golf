﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameoverPopup : MonoBehaviour
{
    Transform ball, flag;
    public TMP_Text distanceText;
    Button nextLevelButton;
    Button backToMainMenuButton;

    // Start is called before the first frame update
    void Start()
    {
        this.distanceText = GetComponentInChildren<TMP_Text>();
        foreach (Button button in GetComponentsInChildren<Button>())
        {
            if (button.tag == "NextLevel")
            {
                this.nextLevelButton = button;
                this.nextLevelButton.onClick.AddListener(NavigateToNextLevel);
            }

            else if (button.tag == "BackToMainMenu")
            {
                this.backToMainMenuButton = button;
                this.backToMainMenuButton.onClick.AddListener(NavigateBackToMainMenu);
            }
        }
    }


    public void Instantiate(Transform ball, Transform flag)
    {
        this.ball = ball;
        this.flag = flag;

        float distance = Vector3.Distance(this.ball.position, this.flag.position);

        this.distanceText.text = "Ayy lmao your distance to the hole was: " + distance.ToString("F1") + "m.";
    }

    void NavigateBackToMainMenu()
    {
        SceneManager.LoadScene("StartScene");
    }

    void NavigateToNextLevel()
    {
        // this is kinda jank but for now,
        // since we only have two scenes, this is hardcoded :)
        if (SceneManager.GetActiveScene().name == "MiniMap")
        {
            SceneManager.LoadScene("HillyCourse");
        }
        else if (SceneManager.GetActiveScene().name == "HillyCourse")
        {
            // SceneManager.LoadScene("HillyCourse");
            nextLevelButton.enabled = false;
        }
    }
    void BackToMainMenu()
    {
        SceneManager.LoadScene("StartScreen");
    }

    public void SetText(string text)
    {
        distanceText.text = text;
    }
}

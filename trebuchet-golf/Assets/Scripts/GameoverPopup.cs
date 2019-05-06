using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameoverPopup : MonoBehaviour
{
    Transform ball, flag;
    public TMP_Text distanceText;
    Button restartButton;
    Button backToMainMenuButton;
    
    // Start is called before the first frame update
    void Start()
    {
        this.distanceText = GetComponentInChildren<TMP_Text>();
        this.restartButton = GetComponentInChildren<Button>();
        this.restartButton.onClick.AddListener(RestartGame);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Instantiate(Transform ball, Transform flag)
    {
        this.ball = ball;
        this.flag = flag;

        float distance = Vector3.Distance(this.ball.position, this.flag.position);

        this.distanceText.text = "Ayy lmao your distance to the hole was: " + distance.ToString("F1") + "m.";
    }

    void BackToMainMenu()
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
            SceneManager.LoadScene("HillyCourse");
        }
    }
    void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void SetText(string text)
    {
        distanceText.text = text;
    }
}

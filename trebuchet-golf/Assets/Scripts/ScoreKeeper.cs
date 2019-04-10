using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreKeeper : MonoBehaviour
{
    public TextMeshProUGUI scoreText;

    public int numLaunches = 0;
    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = "# Launches: 0";

    }

    // Update is called once per frame
    void Update()
    {
    }
    
    
    public void addLaunch()
    {
        numLaunches += 1;
        UpdateScore();
    }
    
    
    void UpdateScore()
    {
        if (scoreText != null)
        {
            scoreText.text = "# Launches: " + numLaunches;
        }
    }
    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayScore : MonoBehaviour
{
    TextMeshProUGUI scoreText;
    GameManager gm;
    int actuaPlayerScore;
    // Start is called before the first frame update
    void Start()
    {
        scoreText = GetComponent<TextMeshProUGUI>();
        gm = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        actuaPlayerScore = gm.GetPlayerScore();
        if (actuaPlayerScore > 0)
        {
            if (actuaPlayerScore < 1000)
                scoreText.text = "00" + gm.GetPlayerScore().ToString();
            else if (actuaPlayerScore < 9000)
                scoreText.text = "0" + gm.GetPlayerScore().ToString();
            else
                scoreText.text = gm.GetPlayerScore().ToString();
        }
    }
}

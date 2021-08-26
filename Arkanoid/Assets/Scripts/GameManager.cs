using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//писала и настраивала 10 минут
public class GameManager : MonoBehaviour
{
    [SerializeField] private Ball ball;
    [SerializeField] private Player player;

    [SerializeField] private Text ScoreText;
    [SerializeField] private Text LoseText;

    [SerializeField] private int score = 0;
    [SerializeField] private int lose = 0;
    // Start is called before the first frame update
    void Start()
    {
        ScoreText.text = score.ToString();
        LoseText.text = lose.ToString();
        ball.Lose += Ball_Lose;
        ball.Score += Ball_Score;
    }

    private void Ball_Score()
    {
        score++;
        ScoreText.text = score.ToString();
    }
    private void Ball_Lose()
    {
        lose++;
        LoseText.text = lose.ToString();
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}

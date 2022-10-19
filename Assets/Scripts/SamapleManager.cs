using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SamapleManager : MonoBehaviour
{
    public GameObject mainImage;
    public GameObject panel;

    public GameObject scoreText;    //スコアテキスト
    public static int totalScore;   //合計スコア
    public int stageScore = 0;        //ステージスコア


    // Start is called before the first frame update
    void Start()
    {
        //++スコア追加++
        UpdateScore();
    }

    // Update is called once per frame
    void Update()
    {

        if (PlayerController.gameState == "gameclear")
        {
            totalScore += stageScore;
            stageScore = 0;
            UpdateScore();
        }

        else if(PlayerController.gameState  ==  "gameover")
        {

        }

        else if (PlayerController.gameState == "playing")
        {
            //ゲーム中
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            PlayerController playerCnt = player.GetComponent<PlayerController>();

            //***スコア追加***
            if (playerCnt.score != 0)
            {
                stageScore += playerCnt.score;
                playerCnt.score = 0;
                UpdateScore();
            }
        }

    }

    //+++スコア追加+++
    void UpdateScore()
    {
        int score = stageScore + totalScore;
        scoreText.GetComponent<Text>().text = score.ToString();
    }
}
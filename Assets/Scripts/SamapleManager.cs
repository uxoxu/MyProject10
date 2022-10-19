using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SamapleManager : MonoBehaviour
{
    public GameObject mainImage;
    public GameObject panel;

    public GameObject scoreText;    //�X�R�A�e�L�X�g
    public static int totalScore;   //���v�X�R�A
    public int stageScore = 0;        //�X�e�[�W�X�R�A


    // Start is called before the first frame update
    void Start()
    {
        //++�X�R�A�ǉ�++
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
            //�Q�[����
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            PlayerController playerCnt = player.GetComponent<PlayerController>();

            //***�X�R�A�ǉ�***
            if (playerCnt.score != 0)
            {
                stageScore += playerCnt.score;
                playerCnt.score = 0;
                UpdateScore();
            }
        }

    }

    //+++�X�R�A�ǉ�+++
    void UpdateScore()
    {
        int score = stageScore + totalScore;
        scoreText.GetComponent<Text>().text = score.ToString();
    }
}
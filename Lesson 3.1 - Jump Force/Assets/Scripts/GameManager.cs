using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Transform startingPoint;

    public float lerpSpeed;
    public float highScore = 0;


    PlayerController playerControllerScript;
    //MoveLeft moveLeft;
    //MoveLeftPrefab1 moveLeftPrefab1;
    //MoveLeftPrefab2 moveLeftPrefab2;
    //MoveLeftPrefab3 moveLeftPrefab3;
    //MoveLeftCoin moveLeftCoin;

    public float score = 0;
    public TextMeshProUGUI TotalScore;
    public TextMeshProUGUI HighScoreText;

    public void Awake()
    {
        //moveLeftPrefab1 = GameObject.Find("1").GetComponent<MoveLeftPrefab1>();
        //moveLeftPrefab2 = GameObject.Find("2").GetComponent<MoveLeftPrefab2>();
        //moveLeftPrefab3 = GameObject.Find("3").GetComponent<MoveLeftPrefab3>();
        //moveLeftCoin = GameObject.Find("Prop_Barrier01(Clone)").GetComponent<MoveLeftCoin>();
        //moveLeftCoin = GameObject.FindGameObjectWithTag("Coin").GetComponent<MoveLeftCoin>();
    }
    void Start()
    {
        //moveLeft=GameObject.Find("Background").GetComponent<MoveLeft>();
        
        highScore = PlayerPrefs.GetFloat("HighScore", 0);
        TotalScore.text = "SCORE: " + score;
        HighScoreText.text = "HIGHSCORE: " + highScore;
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
        playerControllerScript.gameover = false;
        StartCoroutine(PlayIntro());
    }


    void Update()
    {
       
        HighScoreText.text = "High Score: " + highScore.ToString();
    }

    IEnumerator PlayIntro()
    {
        Vector3 startPos = playerControllerScript.transform.position;
        Vector3 endPos = startingPoint.position;
        float journeyLength = Vector3.Distance(startPos, endPos);
        float startTime = Time.time;
        float distanceCovered = (Time.time - startTime) * lerpSpeed;
        float fractionOfJourney = distanceCovered / journeyLength;
        playerControllerScript.GetComponent<Animator>().SetFloat("Speed_Multiplier",
        0.5f);
        while (fractionOfJourney < 1)
        {
            distanceCovered = (Time.time - startTime) * lerpSpeed;
            fractionOfJourney = distanceCovered / journeyLength;
            playerControllerScript.transform.position = Vector3.Lerp(startPos, endPos,
            fractionOfJourney);
            yield return null;
        }
        playerControllerScript.GetComponent<Animator>().SetFloat("Speed_Multiplier",
        1.0f);
        playerControllerScript.gameover = false;
    }

    public void AddScore(int value)
    {
        score += value;
        TotalScore.text = "SCORE: " + score;
        if (score >= highScore)
        {
            highScore = score;
            HighScoreText.text = "HIGHSCORE: " + highScore;
            PlayerPrefs.SetFloat("HighScore", highScore);
        }
        else
        {
            HighScoreText.text = "HIGHSCORE: " + highScore;
        }

    }
    public void RestartGame()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
        //SceneManager.LoadScene("Prototype 3");
        //playerControllerScript.gameover = false;
        //playerControllerScript.playerAnim.SetBool("Death_b", false);
        playerControllerScript.gameover = false;
        playerControllerScript.doubleJumpUsed = false;
        playerControllerScript.isGrounded = true;
        //Time.timeScale = 1.0f;
        
        

        playerControllerScript.JumpControl();
        playerControllerScript.hardSpeed();

        playerControllerScript.rg.SetActive(false);

    }


   
    








}

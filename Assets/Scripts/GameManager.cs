using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] GameObject startButton;
    [SerializeField] CastleManager castleManager;
    [SerializeField] private float blueScore = 0;
    [SerializeField] private float goldScore = 0;
    [SerializeField] private TextMeshProUGUI goldText;
    [SerializeField] private TextMeshProUGUI blueText;

    private void Awake()
    {
        instance = this;
    }
    
    public void gameStart()
    {
        castleManager.activateCastle();
    }

    public void nextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void addGoldScore(float addScore)
    {
        goldScore += addScore;
        goldText.text = goldScore.ToString();
    }

    public void addBlueScore(float addScore)
    {
        blueScore += addScore;
        blueText.text = blueScore.ToString();
    }
}

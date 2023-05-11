using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] GameObject startButton;
    [SerializeField] GameObject finishPanel;
    [SerializeField] GameObject continuePanel;
    [SerializeField] GameObject winPanel;
    [SerializeField] CastleManager castleManager;
    [SerializeField] private float blueScore = 0;
    [SerializeField] private float goldScore = 0;
    [SerializeField] private TextMeshProUGUI goldText;
    [SerializeField] private TextMeshProUGUI blueText;
    [SerializeField] private TextMeshProUGUI loot_goldText;
    [SerializeField] private TextMeshProUGUI loot_blueText;
    [SerializeField] private TextMeshProUGUI loot_goldText2;
    [SerializeField] private TextMeshProUGUI loot_blueText2;
    float timeScaling;
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
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;
        int totalSceneCount = SceneManager.sceneCountInBuildSettings;
        if (sceneIndex < totalSceneCount - 1) // Bir sonraki sahne var ise.
        {
            SceneManager.LoadScene(sceneIndex + 1);
        }
        else
        {
            SceneManager.LoadScene(0);
        }
    }

    public void nextRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void setScores()
    {
        loot_blueText.text = blueScore.ToString();
        loot_goldText.text = goldScore.ToString();
        loot_blueText2.text = blueScore.ToString();
        loot_goldText2.text = goldScore.ToString();
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
    public void continueBut()
    {
        continuePanel.SetActive(false);
        finishPanel.SetActive(true);
    }

    public void winGame()
    {
        FindAnyObjectByType<Door>().setBool(false);
        setScores();
        winPanel.SetActive(true);
        winPanel.transform.DOMove(new Vector3(Screen.width / 2, Screen.height / 2, 0), 2f);
        DOTween.To(() => winPanel.GetComponent<CanvasGroup>().alpha, x => winPanel.GetComponent<CanvasGroup>().alpha = x, 1f, 2f).OnComplete(()=> { winPanel.transform.GetChild(1).gameObject.SetActive(true); });
    }
    public void finishGame()
    {
        FindAnyObjectByType<Door>().setBool(false);
        timeScaling = Time.timeScale;
        DOTween.To(() => Time.timeScale, x => Time.timeScale = x, 0.2f, 1f).OnComplete(() => { DOTween.To(() => Time.timeScale, x => Time.timeScale = x, 1, 1f); });
        castleManager.inactiveCastle();
        Clone[] clones = GameObject.FindObjectsOfType<Clone>();
        for (int i = 0; i < clones.Length; i++)
            Destroy(clones[i].gameObject);
        continuePanel.SetActive(true);
    }
}

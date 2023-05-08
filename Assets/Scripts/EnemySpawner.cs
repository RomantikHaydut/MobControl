using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] enemyPrefabList;
    [SerializeField] private float health = 100;
    [SerializeField] private float castleScore = 200;
    // [SerializeField] Text healthText;
    public bool isActive = false;
    private touchControls touchControls;

    private void Start()
    {
        touchControls = FindAnyObjectByType<touchControls>();
    }

    private void Update()
    {
        //healthText.text = health.ToString();
    }
    private void OnEnable()
    {
        InvokeRepeating("Spawn", 1f, 4f);
    }

    private void Spawn()
    {
        if (isActive)
        {
            int randomIndex = Random.Range(0, enemyPrefabList.Length);
            Instantiate(enemyPrefabList[randomIndex], transform.GetChild(0).position, transform.rotation);
        }

    }

    public void takeDamage()
    {
        health -= 1;
        if (health <= 0)
        {
            isActive = false;
            Clone[] clones = GameObject.FindObjectsOfType<Clone>();
            for (int i = 0; i < clones.Length; i++)
                Destroy(clones[i].gameObject);
            EnemyClone[] enemyClone = GameObject.FindObjectsOfType<EnemyClone>();
            for (int i = 0; i < enemyClone.Length; i++)
                Destroy(enemyClone[i].gameObject);
            GameManager.instance.addGoldScore(castleScore);
            if (FindAnyObjectByType<CastleManager>().index == 3)
            {
                GameManager.instance.winGame();
            }
            else
            {
                touchControls.moveWithCamera(transform);
                this.gameObject.SetActive(false);
            }
        }
    }
}

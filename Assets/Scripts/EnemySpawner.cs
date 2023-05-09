using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] enemyPrefabList;
    [SerializeField] private float health = 100;
    [SerializeField] private float castleScore = 200;
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private Animator shaking;
    [SerializeField] private AudioSource damage;
    // [SerializeField] Text healthText;
    public bool isActive = false;
    private touchControls touchControls;

    private void Start()
    {
        healthText.text = health.ToString();
        touchControls = FindAnyObjectByType<touchControls>();
        shaking = GetComponent<Animator>();
        damage = GetComponent<AudioSource>();
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
        shaking.SetBool("isDamage", true);
        damage.Play();
        healthText.text = health.ToString();
        if (health <= 0)
        {
            isActive = false;
            Multiplier[] multiplier = FindObjectsOfType<Multiplier>();
            for (int i = 0; i < multiplier.Length; i++)
                multiplier[i].canMultiplier = false;
            Clone[] clones = GameObject.FindObjectsOfType<Clone>();
            for (int i = 0; i < clones.Length; i++)
                Destroy(clones[i].gameObject, 1f);
            EnemyClone[] enemyClone = GameObject.FindObjectsOfType<EnemyClone>();
            for (int i = 0; i < enemyClone.Length; i++)
                Destroy(enemyClone[i].gameObject, 1f);
            GameManager.instance.addGoldScore(castleScore);
            if (FindAnyObjectByType<CastleManager>().index == 3)
            {
                shaking.SetBool("isDamage", false);
                GameManager.instance.winGame();
            }
            else
            {
                shaking.SetBool("isDamage", false);
                touchControls.moveWithCamera(transform);
                this.gameObject.SetActive(false);
            }
        }
        Invoke("set_bool", 1f);

    }


    public void set_bool()
    {
        shaking.SetBool("isDamage", false);
    }
}

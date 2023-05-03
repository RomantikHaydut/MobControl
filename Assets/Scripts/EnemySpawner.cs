using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private float health = 100;
   // [SerializeField] Text healthText;
    public bool isActive = false;

    private void Update()
    {
        //healthText.text = health.ToString();
    }
    private void OnEnable()
    {
        if (isActive)
        {
            InvokeRepeating("Spawn", 1f, .5f);
        }
    }

    private void Spawn()
    {
        Instantiate(enemyPrefab, transform.position, transform.rotation);
    }

    public void takeDmage(float damage)
    {
        health -= damage;
    }
}

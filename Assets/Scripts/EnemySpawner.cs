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
            InvokeRepeating("Spawn", 1f, .5f);
    }

    private void Spawn()
    {
        if (isActive)
        {
            Instantiate(enemyPrefab, transform.GetChild(0).position, transform.rotation);
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

            FindAnyObjectByType<touchControls>().moveWithCamera(transform);
            this.gameObject.SetActive(false);
        }
    }
}

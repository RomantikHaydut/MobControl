using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;

    private void OnEnable()
    {
        InvokeRepeating("Spawn", 1f, .5f);
    }
    private void Spawn()
    {
        Instantiate(enemyPrefab, transform.position, transform.rotation);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager Instance;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private GameObject projectilePrefabGiant;
    [SerializeField] private float spawnScore = 0;
    public float maxSpawnScore = 10;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    public void SpawnProjectile(Transform spawnCenter, bool canGiantSpawn) // Tekli spawn. Player yap�yor ve h�zl� ba�lang�c� var.
    {
        if (checkSpawnScore(canGiantSpawn) && canGiantSpawn)
        {
            GameObject projectileClone = Instantiate(projectilePrefabGiant, spawnCenter.position, spawnCenter.rotation); // devlerin �o�almas�
            projectileClone.GetComponent<Clone>().FastStart();
        }
        else
        {
            GameObject projectileClone = Instantiate(projectilePrefab, spawnCenter.position, spawnCenter.rotation);
            spawnScore++;
            projectileClone.GetComponent<Clone>().FastStart();
        }
    }

    public void SpawnProjectile(Transform spawnCenter, GameObject spawnObject, int spawnCount, Multiplier multiplier) // �oklu spawn. Multiplier kap�lar� yap�yor.
    {
        float radius = projectilePrefab.transform.localScale.x;
        float angleStep = 360f / spawnCount; // a�� ad�m� hesapla
        float angle = 0f; // ba�lang�� a��s�
        for (int i = 0; i < spawnCount; i++)
        {
            Vector3 spawnPosition = spawnCenter.position + Random.onUnitSphere * 0.1f; // nesnenin yarat�laca�� konum
            spawnPosition.y = spawnObject.transform.position.y;
            Clone clone = Instantiate(spawnObject, spawnPosition, spawnCenter.rotation).GetComponent<Clone>(); // nesneyi yarat
            multiplier.AddCloneToList(clone);
            angle += angleStep; // a��y� artt�r
        }
    }
    bool checkSpawnScore(bool canGiant)
    {
        if (spawnScore >= maxSpawnScore && canGiant)
        {
            spawnScore = 0;
            return true;
        }
        return false;
    }
}

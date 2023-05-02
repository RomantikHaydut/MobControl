using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager Instance;
    [SerializeField] private GameObject projectilePrefab;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    public void SpawnProjectile(Transform spawnCenter) // Tekli spawn. Player yap�yor ve h�zl� ba�lang�c� var.
    {
        GameObject projectileClone = Instantiate(projectilePrefab, spawnCenter.position, spawnCenter.rotation);
        projectileClone.GetComponent<Clone>().FastStart();
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
}

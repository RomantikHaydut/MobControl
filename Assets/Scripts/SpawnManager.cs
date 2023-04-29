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
        projectileClone.GetComponent<ProjectileController>().FastStart();
    }

    public void SpawnProjectile(Transform spawnCenter, GameObject spawnObject, int spawnCount, Multiplier multiplier) // �oklu spawn. Multiplier kap�lar� yap�yor.
    {
        float radius = projectilePrefab.transform.localScale.x;
        float angleStep = 360f / spawnCount; // a�� ad�m� hesapla
        float angle = 0f; // ba�lang�� a��s�
        for (int i = 0; i < spawnCount; i++)
        {
            /*
            float x = Mathf.Cos(Mathf.Deg2Rad * angle) * radius; // x koordinat�n� hesapla
            float z = Mathf.Sin(Mathf.Deg2Rad * angle) * radius; // y koordinat�n� hesapla
            Vector3 spawnPosition = spawnCenter.position + new Vector3(x, 0, z); // nesnenin yarat�laca�� konum
            */
            Vector3 spawnPosition = spawnCenter.position + Random.onUnitSphere; // nesnenin yarat�laca�� konum
            spawnPosition.y = spawnObject.transform.position.y;
            GameObject projectileClone = Instantiate(projectilePrefab, spawnPosition, spawnCenter.rotation); // nesneyi yarat
            projectileClone.GetComponent<ProjectileController>().AddMultiplierDoor(multiplier);  // Spawn olan objenin tekrar kendini spawnlamamas� i�in o objeye spawn kap�s�n� ekliyoruz.
            angle += angleStep; // a��y� artt�r
        }
    }
}

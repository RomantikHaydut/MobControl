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
    public void SpawnProjectile(Transform spawnCenter) // Tekli spawn. Player yapýyor ve hýzlý baþlangýcý var.
    {
        GameObject projectileClone = Instantiate(projectilePrefab, spawnCenter.position, spawnCenter.rotation);
        projectileClone.GetComponent<ProjectileController>().FastStart();
    }

    public void SpawnProjectile(Transform spawnCenter, GameObject spawnObject, int spawnCount, Multiplier multiplier) // Çoklu spawn. Multiplier kapýlarý yapýyor.
    {
        float radius = projectilePrefab.transform.localScale.x;
        float angleStep = 360f / spawnCount; // açý adýmý hesapla
        float angle = 0f; // baþlangýç açýsý
        for (int i = 0; i < spawnCount; i++)
        {
            /*
            float x = Mathf.Cos(Mathf.Deg2Rad * angle) * radius; // x koordinatýný hesapla
            float z = Mathf.Sin(Mathf.Deg2Rad * angle) * radius; // y koordinatýný hesapla
            Vector3 spawnPosition = spawnCenter.position + new Vector3(x, 0, z); // nesnenin yaratýlacaðý konum
            */
            Vector3 spawnPosition = spawnCenter.position + Random.onUnitSphere; // nesnenin yaratýlacaðý konum
            spawnPosition.y = spawnObject.transform.position.y;
            GameObject projectileClone = Instantiate(projectilePrefab, spawnPosition, spawnCenter.rotation); // nesneyi yarat
            projectileClone.GetComponent<ProjectileController>().AddMultiplierDoor(multiplier);  // Spawn olan objenin tekrar kendini spawnlamamasý için o objeye spawn kapýsýný ekliyoruz.
            angle += angleStep; // açýyý arttýr
        }
    }
}

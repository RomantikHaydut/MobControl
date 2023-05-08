using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager Instance;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private GameObject projectilePrefabGiant;
    [SerializeField] public float spawnScore = 0;
    public float maxSpawnScore = 10;
    [SerializeField] private Image chargeIcon;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    public void SpawnProjectile(Transform spawnCenter, bool canGiantSpawn) // Tekli spawn. Player yapýyor ve hýzlý baþlangýcý var.
    {
        if (checkSpawnScore(canGiantSpawn) && canGiantSpawn)
        {
            GameObject projectileClone = Instantiate(projectilePrefabGiant, spawnCenter.position, spawnCenter.rotation); // devlerin çoðalmasý
            projectileClone.GetComponent<Clone>().FastStart();
            FindObjectOfType<touchControls>().canGiantSpawn = false;
        }
        else
        {
            GameObject projectileClone = Instantiate(projectilePrefab, spawnCenter.position, spawnCenter.rotation);
            spawnScore++;
            projectileClone.GetComponentInChildren<Clone>().FastStart();
        }

        float chargeValue = Mathf.Clamp(spawnScore / maxSpawnScore, 0, 1f);
        if (chargeValue == 1)
        {
            chargeIcon.color = Color.green;
        }
        else
        {
            chargeIcon.color = Color.white;
        }
        chargeIcon.fillAmount = chargeValue;
    }

    public void SpawnProjectile(Transform spawnCenter, GameObject spawnObject, int spawnCount, Multiplier multiplier) // Çoklu spawn. Multiplier kapýlarý yapýyor.
    {
        float radius = projectilePrefab.transform.localScale.x;
        float angleStep = 360f / spawnCount; // açý adýmý hesapla
        float angle = 0f; // baþlangýç açýsý
        for (int i = 0; i < spawnCount; i++)
        {
            Vector3 spawnPosition = spawnCenter.position + Random.onUnitSphere * 0.1f; // nesnenin yaratýlacaðý konum
            spawnPosition.y = spawnObject.transform.position.y;
            Clone clone = Instantiate(spawnObject, spawnPosition, spawnCenter.rotation).GetComponent<Clone>(); // nesneyi yarat
            multiplier.AddCloneToList(clone);
            angle += angleStep; // açýyý arttýr
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

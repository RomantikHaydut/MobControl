using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastleManager : MonoBehaviour
{
    public List<GameObject> castles;
    public int index = 0;
    public void activateCastle()
    {
        castles[index].GetComponent<EnemySpawner>().isActive = true;
        index++;
        Multiplier[] multiplier = FindObjectsOfType<Multiplier>();
        for (int i = 0; i < multiplier.Length; i++)
            multiplier[i].canMultiplier = true;
    }

    public Transform nextCastle()
    {
        return castles[index].transform;
    }

    public void inactiveCastle()
    {
        castles[index - 1].GetComponent<EnemySpawner>().isActive = false;
    }
}

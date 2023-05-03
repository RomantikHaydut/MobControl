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
    }

    public Transform nextCastle()
    {
        return castles[index].transform;
    }
}

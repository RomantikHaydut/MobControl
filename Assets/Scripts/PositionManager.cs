using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionManager : MonoBehaviour
{
    public List<Transform> castles;
    [SerializeField] public List<Transform> subPoints;
    [SerializeField] public List<float> boundValues;
    int index = 0;
    private void Awake()
    {
        foreach (var point in FindAnyObjectByType<CastleManager>().castles)
        {
            castles.Add(point.transform);
        }
    }

    public Transform getSubPoint()
    {
        return subPoints[index].transform;
    }
    public float getBoundPoint()
    {
        return boundValues[index];
    }
    public Transform getSubPointIncrease()
    {
        return subPoints[index++].transform;
    }
}

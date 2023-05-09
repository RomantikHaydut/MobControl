using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Multiplier : MonoBehaviour
{
    [SerializeField] private int multiplierFactor = 1;
    private List<Clone> cloneList = new List<Clone>();
    public bool canMultiplier = true;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Clone clone))
        {
            if (canMultiplier)
            {
                if (!IsCloneCloned(clone))
                {
                    AddCloneToList(clone);
                    SpawnManager.Instance.SpawnProjectile(other.gameObject.transform, other.gameObject, multiplierFactor - 1, this);
                }
            }
        }
    }

    public void AddCloneToList(Clone clone)
    {
        if (!cloneList.Contains(clone))
        {
            cloneList.Add(clone);
        }
    }

    private bool IsCloneCloned(Clone clone)
    {
        return cloneList.Contains(clone);
    }


}

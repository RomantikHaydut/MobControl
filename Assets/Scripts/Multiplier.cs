using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Multiplier : MonoBehaviour
{
    [SerializeField] private int multiplierFactor = 1;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out ProjectileController projectileController))
        {
            if (projectileController.CanClone())
            {
                if (!projectileController.IsMultipliedUsedBefore(this))
                {
                    projectileController.AddMultiplierDoor(this);
                    SpawnManager.Instance.SpawnProjectile(other.gameObject.transform, other.gameObject, multiplierFactor, this);
                }

            }
        }
    }
}

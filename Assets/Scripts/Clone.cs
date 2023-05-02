using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clone : MonoBehaviour
{
    [SerializeField] private ProjectileTypeSO projectileType;
    private float health;
    private float speed;
    private float damage;
    private List<Multiplier> multipliedMultipliers = new List<Multiplier>();

    void Awake()
    {
        health = projectileType.health;
        speed = projectileType.speed;
        damage = projectileType.damage;
    }

    private void FixedUpdate()
    {
        Movement();
    }
    protected virtual void Movement()
    {
        transform.position += transform.forward * Time.deltaTime * speed;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            // Die
        }
    }

    public void FastStart()
    {
        StartCoroutine(SlowDown_Coroutine());
    }
    private IEnumerator SlowDown_Coroutine()
    {
        float startSpeed = 5f;
        speed *= startSpeed;
        while (true)
        {
            yield return new WaitForFixedUpdate();
            speed -= Time.deltaTime * startSpeed;
            if (speed <= projectileType.speed)
            {
                speed = projectileType.speed;
                yield break;
            }
        }
    }

    public bool CanClone()
    {
        if (projectileType.type == ProjectileTypeSO.Type.Friend)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void AddMultiplierDoor(Multiplier multiplier)
    {
        multipliedMultipliers.Add(multiplier);
    }

    public bool IsMultipliedUsedBefore(Multiplier multiplier)
    {
        return (multipliedMultipliers.Contains(multiplier));
    }
}

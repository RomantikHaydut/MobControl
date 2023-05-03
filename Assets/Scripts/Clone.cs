using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clone : MonoBehaviour
{
    [SerializeField] private ProjectileTypeSO projectileType;
    private float health;
    private float speed;
    private float damage;

    private bool isFighting = false;

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

    public void TakeDamage(float damage, out bool isDead)
    {
        isDead = false;
        health -= damage;
        if (health <= 0)
        {
            isDead = true;
            Destroy(gameObject);
        }
    }

    public float GetDamage()
    {
        return damage;
    }

    public void StartFight()
    {
        isFighting = true;
    }
    public void FinishFight()
    {
        isFighting = false;
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


}

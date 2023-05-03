using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Clone : MonoBehaviour
{
    [SerializeField] private ProjectileTypeSO projectileType;
    private float health;
    private float speed;
    private float damage;
    private bool isFighting = false;
    [SerializeField] private Material myMaterial;
    [SerializeField] private Color normalColor;
    [SerializeField] private Color popupColor;
    void Awake()
    {
        health = projectileType.health;
        speed = projectileType.speed;
        damage = projectileType.damage;

        myMaterial = (GetComponentInChildren<MeshRenderer>()).material;
        normalColor = projectileType.normalColor;
        GetComponentInChildren<MeshRenderer>().material.color = normalColor;
        popupColor = projectileType.popupColor;
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
        Popup();
        isDead = false;
        health -= damage;
        if (health <= 0)
        {
            isDead = true;
            Destroy(gameObject);
        }
    }

    private void Popup()
    {
        myMaterial.color = normalColor;
        float popupTime = 0.1f;
        myMaterial.DOColor(popupColor, popupTime).OnComplete(() => { myMaterial.DOColor(normalColor, popupTime); });
        GetComponentInChildren<MeshRenderer>().material = myMaterial;
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

}

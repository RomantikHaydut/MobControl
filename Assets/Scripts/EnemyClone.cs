using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyClone : MonoBehaviour
{
    [SerializeField] private ProjectileTypeSO projectileType;
    private float health;
    private float speed;
    private float damage;
    private bool isFighting = false;

    private Material myMaterial;
    private Color normalColor;
    private Color popupColor;

    void Awake()
    {
        health = projectileType.health;
        speed = projectileType.speed;
        damage = projectileType.damage;

        popupColor = projectileType.popupColor;
        myMaterial = (GetComponentInChildren<SkinnedMeshRenderer>()).material;
        normalColor = myMaterial.color;
    }

    private void FixedUpdate()
    {
        Movement();
    }
    protected virtual void Movement()
    {
        transform.position += transform.forward * Time.deltaTime * speed;
    }

    private void Popup()
    {
        myMaterial.color = normalColor;
        float popupTime = 0.1f;
        myMaterial.DOColor(popupColor, popupTime).OnComplete(() => { myMaterial.DOColor(normalColor, popupTime); });
    }

    public void TakeDamage(float damage, Clone clone)
    {
        Popup();
        health -= damage;
        if (health <= 0)
        {
            clone.FinishFight();
            Destroy(gameObject);
        }
    }

    private void StartFight(Clone clone)
    {
        isFighting = true;
        clone.StartFight();
        StartCoroutine(Fight_Coroutine(clone));
    }

    private void FinishFight(Clone clone)
    {
        isFighting = false;
        clone.FinishFight();
    }

    private IEnumerator Fight_Coroutine(Clone clone)
    {
        float fightDelay = 0.25f;
        bool isEnemyDead;
        while (true)
        {
            if (clone == null)
            {
                isFighting = false;
                yield break;
            }
            if (!isFighting)
            {
                clone.FinishFight();
                yield break;
            }
            clone.TakeDamage(damage, out isEnemyDead);
            TakeDamage(clone.GetDamage(), clone);

            if (isEnemyDead)
            {
                isFighting = false;
                yield break;
            }
            yield return new WaitForSecondsRealtime(fightDelay);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isFighting)
        {
            if (other.gameObject.TryGetComponent(out Clone clone))
            {
                StartFight(clone);
            }
        }
    }

    //private void OnTriggerStay(Collider other)
    //{
    //    if (!isFighting)
    //    {
    //        if (other.gameObject.TryGetComponent(out Clone clone))
    //        {
    //            StartFight(clone);
    //        }
    //    }
    //}

    private void OnTriggerExit(Collider other)
    {
        if (isFighting)
        {
            if (other.gameObject.TryGetComponent(out Clone clone))
            {
                FinishFight(clone);
            }
        }
    }
}

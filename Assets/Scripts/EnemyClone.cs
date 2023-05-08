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
    [SerializeField] List<SkinnedMeshRenderer> skinnedMeshRendererList = new List<SkinnedMeshRenderer>();
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

    private void Start()
    {
        transform.parent = null;
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
        DOTween.Kill(this);
        foreach (SkinnedMeshRenderer renderer in skinnedMeshRendererList)
        {
            renderer.material.color = Color.white;
        }

        float popupTime = 0.1f;

        foreach (SkinnedMeshRenderer renderer in skinnedMeshRendererList)
        {
            renderer.material.DOColor(Color.blue, popupTime).OnComplete(() => { renderer.material.DOColor(Color.white, popupTime); });
        }
    }

    public void TakeDamage(float damage, Clone clone)
    {
        Popup();
        health -= damage;
        if (health <= 0)
        {
            clone.FinishFight();
            gameObject.SetActive(false);
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
        float fightDelay = 0.15f;
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
        if (other.gameObject.TryGetComponent(out touchControls tc))
        {
            speed = 0;
            Destroy(this.gameObject, 1f);
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

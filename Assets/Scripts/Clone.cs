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
    private Vector3 direction;
    [SerializeField] List<SkinnedMeshRenderer>  skinnedMeshRendererList = new List<SkinnedMeshRenderer>();
    MeshRenderer meshRenderer;
    public bool isGiant;
    void Awake()
    {
        health = projectileType.health;
        speed = projectileType.speed;
        damage = projectileType.damage;
        
        direction = transform.forward;

        popupColor = projectileType.popupColor;
    }


    private void FixedUpdate()
    {
        Movement();
    }
    protected virtual void Movement()
    {
        transform.position += direction * Time.deltaTime * speed;
    }

    public void TakeDamage(float damage, out bool isDead)
    {
        Popup();
        isDead = false;
        health -= damage;
        if (health <= 0)
        {
            isDead = true;
            DOTween.Kill(this);
            Destroy(gameObject);
        }
    }

    private void Popup()
    {
        if (isGiant)
        {
            DOTween.Kill(this);
            skinnedMeshRendererList[0].material.color = normalColor;
            skinnedMeshRendererList[1].material.color = normalColor;
            float popupTime = 0.1f;
            skinnedMeshRendererList[0].material.DOColor(Color.red, popupTime).OnComplete(() => { skinnedMeshRendererList[0].material.DOColor(normalColor, popupTime); });
            skinnedMeshRendererList[1].material.DOColor(Color.red, popupTime).OnComplete(() => { skinnedMeshRendererList[1].material.DOColor(normalColor, popupTime); });
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

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.TryGetComponent(out EnemySpawner enemySpawner))
        {
            direction = (enemySpawner.transform.position - transform.position);
            direction.y = 0;
            direction = direction.normalized;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out EnemySpawner enemySpawner))
        {
            enemySpawner.takeDamage();
            Destroy(this.gameObject);
        }
    }
}

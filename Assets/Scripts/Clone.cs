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

    [SerializeField] private Color popupColor;
    [SerializeField] private float blueScore = 1;
    [SerializeField] private GameManager gm;
    private Vector3 direction;
    [SerializeField] List<SkinnedMeshRenderer> skinnedMeshRendererList = new List<SkinnedMeshRenderer>();
    [SerializeField] private List<Color> normalColorList = new List<Color>();
    MeshRenderer meshRenderer;
    void Awake()
    {
        health = projectileType.health;
        speed = projectileType.speed;
        damage = projectileType.damage;

        direction = transform.forward;

        popupColor = projectileType.popupColor;
        gm = FindObjectOfType<GameManager>();
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
            gameObject.SetActive(false);
        }
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
            renderer.material.DOColor(Color.green, popupTime).OnComplete(() => { renderer.material.DOColor(Color.white, popupTime); });
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
            gm.addBlueScore(blueScore);
            Destroy(this.gameObject);
        }
    }
}

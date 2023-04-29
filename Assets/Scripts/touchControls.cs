using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class touchControls : MonoBehaviour
{
    #region Spawn Projectile
    [SerializeField] private float spawnCooldown = 0f;
    [SerializeField] private float spawnCooldownMax = 0.5f;
    [SerializeField] private Transform spawnPosition;
    #endregion
    #region Touch
    private Touch theTouch;
    private float timeTouchEnded;
    public float sensitivity = 13;
    #endregion

    private void Start()
    {
        spawnCooldown = 0f;
    }
    private void Update()
    {
        Movement();
        CheckPosition();
    }

    private void Movement()
    {
        if (Input.touchCount > 0)
        {
            theTouch = Input.GetTouch(0);
            if (theTouch.phase == TouchPhase.Began)
            {
                SpawnProjectile();
            }
            if (theTouch.phase == TouchPhase.Stationary || theTouch.phase == TouchPhase.Moved)
            {
                if (theTouch.deltaPosition.x < 0)
                {
                    if (transform.position.x > -4.75)
                    {
                        transform.position += new Vector3((theTouch.deltaPosition.x / Screen.width * sensitivity), 0, 0);
                    }
                    else
                    {
                        transform.position = new Vector3(-4.75f, transform.position.y, transform.position.z);
                    }
                }
                else
                {
                    if (transform.position.x < 4.75f)
                    {
                        transform.position += new Vector3((theTouch.deltaPosition.x / Screen.width * sensitivity), 0, 0);
                    }
                    else
                    {
                        transform.position = new Vector3(4.75f, transform.position.y, transform.position.z);
                    }
                }
                SpawnProjectile();
            }
            if (theTouch.phase == TouchPhase.Ended)
            {
                //print("dokunma bitti");
            }
        }
    }

    void CheckPosition()
    {
        if (transform.position.x > 4.75f)
        {
            transform.position = new Vector3(4.75f, transform.position.y, transform.position.z);
        }
        else if (transform.position.x < -4.75f)
        {
            transform.position = new Vector3(-4.75f, transform.position.y, transform.position.z);
        }
    }

    private void SpawnProjectile()
    {
        if (spawnCooldown <= 0)
        {
            SpawnManager.Instance.SpawnProjectile(spawnPosition);
            StartCoroutine(SpawnCooldownTimer_Corotine());
        }
    }

    private IEnumerator SpawnCooldownTimer_Corotine()
    {
        spawnCooldown = spawnCooldownMax;
        while (true)
        {
            yield return new WaitForFixedUpdate();
            spawnCooldown -= Time.deltaTime;
            if (spawnCooldown <= 0)
            {
                yield break;
            }
        }
    }
}
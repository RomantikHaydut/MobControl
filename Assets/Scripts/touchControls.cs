using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


public class touchControls : MonoBehaviour
{
    #region Spawn Projectile
    [SerializeField] private float spawnCooldown = 0f;
    [SerializeField] private float spawnCooldownMax = 0.5f;
    [SerializeField] private Transform spawnPosition;
    private PositionManager pm;
    #endregion
    #region Touch
    [SerializeField] private float rightBound;
    [SerializeField] private float leftBound;
    private Touch theTouch;
    private float timeTouchEnded;
    public float sensitivity = 13;
    #endregion
    public bool isBound;
    public bool isMoving;
    public bool isFinished;
    public bool canGiantSpawn;
    [SerializeField] private Transform vehicle;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void Start()
    {
        spawnCooldown = 0f;
        pm = FindAnyObjectByType<PositionManager>();
        rightBound = pm.getSubPoint().position.x + pm.getBoundPoint();
        leftBound = pm.getSubPoint().position.x - pm.getBoundPoint();
    }
    private void Update()
    {
        if (!isMoving && !isFinished)
        {
            Movement();
        }
    }

    private void Movement()
    {

        if (Input.touchCount > 0)
        {
            theTouch = Input.GetTouch(0);
            if (theTouch.phase == TouchPhase.Began)
            {
                StartFire();
            }
            if (theTouch.phase == TouchPhase.Stationary || theTouch.phase == TouchPhase.Moved)
            {
                float coefficient = (theTouch.deltaPosition.x / Screen.width * sensitivity);
                if ((transform.position + transform.right * coefficient).x < rightBound)
                {
                    if ((transform.position + transform.right * coefficient).x > leftBound)
                    {
                        transform.position += transform.right * (theTouch.deltaPosition.x / Screen.width * sensitivity);
                    }
                }
            }
            if (theTouch.phase == TouchPhase.Ended)
            {
                if (FindObjectOfType<SpawnManager>().spawnScore >= FindObjectOfType<SpawnManager>().maxSpawnScore)
                    canGiantSpawn = true;
                StopFire();
            }
        }
    }

    private void StartFire()
    {
        animator.SetBool("Fire", true);
    }
    private void StopFire()
    {
        animator.SetBool("Fire", false);
    }

    private void SpawnProjectile()
    {
        if (!isMoving)
        {
            SpawnManager.Instance.SpawnProjectile(spawnPosition, canGiantSpawn);
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
    public void setSubPoint()
    {
        transform.position = pm.getSubPointIncrease().position;
    }
    public void moveWithCamera(Transform point)
    {
        StopFire();
        setSubPoint();
        isMoving = true;
        Transform point2 = pm.getSubPoint();
        Transform nextTransform = FindAnyObjectByType<CastleManager>().nextCastle();
        Camera.main.transform.parent = transform;

        vehicle.DORotate(new Vector3(vehicle.transform.eulerAngles.x, vehicle.transform.eulerAngles.y + 90, vehicle.transform.eulerAngles.z), 1f).OnComplete(() => {
            transform.DOMove(point.position, 3).OnComplete(() =>
            {
                transform.DORotateQuaternion(Quaternion.LookRotation(nextTransform.position - transform.position), 1).OnComplete(() =>
                {
                    vehicle.DORotate(new Vector3(vehicle.transform.eulerAngles.x, vehicle.transform.eulerAngles.y - 90, vehicle.transform.eulerAngles.z), 1f);
                    transform.DOMove(point2.position, 3).OnComplete(() =>
                    {
                        
                        Camera.main.transform.parent = null;
                        FindAnyObjectByType<CastleManager>().activateCastle();
                        rightBound = pm.getSubPoint().position.x + pm.getBoundPoint();
                        leftBound = pm.getSubPoint().position.x - pm.getBoundPoint();
                        isMoving = false;
                    });
                });
            });
        }); 
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out EnemyClone enemy))
        {
            if (!isFinished)
            {
                isFinished = true;
                GameManager.instance.setScores();
                GameManager.instance.finishGame();
            }
        }
    }
}
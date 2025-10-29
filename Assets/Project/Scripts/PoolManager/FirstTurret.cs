using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstTurret : MonoBehaviour
{
    [SerializeField] protected string bulletTag = "Bullet";
    [SerializeField] protected Transform spawnPoint;
    [SerializeField] protected float fireInterval = 1f;
    [SerializeField] protected VisionCone visionCone;

    protected float timer;
    void Awake()
    { 
        visionCone = GetComponentInChildren<VisionCone>(); 
    }

    protected void Start()
    {
        if (spawnPoint == null) spawnPoint = transform;
        timer = fireInterval;

    }

    protected virtual void Update()
    {
        timer -= Time.deltaTime;
        if(visionCone.isVisible == true)
        {
            if (timer <= 0f)
            {
                AudioManager.Instance.PlayLaserSound();
                Shoot();
                timer = fireInterval;
            }
        }
        
    }

    protected virtual void Shoot()
    {
        PoolManager.Instance.SpawnFromPool(bulletTag, spawnPoint.position, spawnPoint.rotation);
    }


}


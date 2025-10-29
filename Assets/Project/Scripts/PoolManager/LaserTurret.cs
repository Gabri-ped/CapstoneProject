using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserTurret : MonoBehaviour
{
    public float fireDuration = 1f;
    public float fireInterval = 2f; 
    public string poolTag = "Laser"; 
    public Transform firePoint;
    public VisionCone visionCone;
    private float time;

    void Start()
    {
        visionCone = GetComponentInChildren<VisionCone>();
    }
     void Update()
    {
        time -= Time.deltaTime;
        if (visionCone.isVisible == true)
        {
            if (time <= 0f)
            {
                FireLaser();
                AudioManager.Instance.PlayLaserSound();
                time = fireInterval;
            }
        }
    }

    void FireLaser()
    {
        GameObject Laser = PoolManager.Instance.SpawnFromPool(poolTag, firePoint.position, firePoint.rotation);
        Laser.GetComponent<Laser>().SetDurationn(fireDuration);
    }
}

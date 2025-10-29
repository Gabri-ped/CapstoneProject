using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Secondturret : FirstTurret
{
    [SerializeField] private Transform spawnpoint2;
    protected override void Shoot()
    {
        base.Shoot();
        PoolManager.Instance.SpawnFromPool(bulletTag, spawnpoint2.position, spawnpoint2.rotation);
    }
}

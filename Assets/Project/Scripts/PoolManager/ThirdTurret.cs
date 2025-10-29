using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdTurret : FirstTurret
{
    [SerializeField] private float maxDistance = 10f;
    protected override void Update()
    {
        timer -= Time.deltaTime;
        if (visionCone.isVisible == true)
        {
            Vector3 distance = PlayerController.instance.transform.position - transform.position;
            if (distance.magnitude <= maxDistance)
            {
                if (timer <= 0f)
                {
                    spawnPoint.LookAt(PlayerController.instance.transform);
                    AudioManager.Instance.PlayLaserSound();
                    Shoot();
                    timer = fireInterval;
                }
            }
        }
    }
}

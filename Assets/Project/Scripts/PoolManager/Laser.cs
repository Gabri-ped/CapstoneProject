using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    private ParticleSystem ps;
    public float duration = 0.5f; 

    void Awake()
    {
        ps = GetComponent<ParticleSystem>();
    }

    void OnEnable()
    {
        if (ps != null)
        {
            ps.Play();
            Invoke(nameof(DisableLaser), duration);
        }
    }

    void DisableLaser()
    {
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            LifeController.Instance?.LoseLife();
        }
    }

    public void SetDurationn(float duration)
    {
        this.duration = duration;
        CancelInvoke();
        Invoke(nameof(DisableLaser), duration);
        

    }
}

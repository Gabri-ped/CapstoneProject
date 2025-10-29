using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Bomb : MonoBehaviour
{
   
    public GameObject explosionPrefab;   
    public AudioClip explosionSound;
    public bool destroyAfterExplode = true;

    bool exploded = false;

    private void OnTriggerEnter(Collider other)
    {
        if (exploded) return;

        if (other.CompareTag("Player"))
        {
            exploded = true;
            AudioManager.Instance.PlayBombSound();
            LifeController.Instance.LoseLife();

           
            if (explosionPrefab != null)
            {
                GameObject fx = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
                ParticleSystem ps = fx.GetComponent<ParticleSystem>();
                if (ps != null)
                {
                    ps.Play();
                    Destroy(fx, ps.main.duration + ps.main.startLifetime.constantMax);
                }
            
            }

            // 3) Suono
            if (explosionSound != null)
                AudioSource.PlayClipAtPoint(explosionSound, transform.position);

            // 4) Distruzione bomba
            if (destroyAfterExplode)
                Destroy(gameObject);
            else
            {
                Collider c = GetComponent<Collider>();
                if (c != null) c.enabled = false;
                MeshRenderer mr = GetComponentInChildren<MeshRenderer>();
                if (mr != null) mr.enabled = false;
            }
        }
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 20f;
    [SerializeField] private float lifeTime = 5f;

    private float timer;

    private void OnEnable()
    {
        timer = lifeTime;
    }

    private void Update()
    {
        transform.position += transform.forward * (speed * Time.deltaTime);

        timer -= Time.deltaTime;
        if (timer <= 0f)
            gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            LifeController.Instance.LoseLife();
        }

        gameObject.SetActive(false); 
    }
}



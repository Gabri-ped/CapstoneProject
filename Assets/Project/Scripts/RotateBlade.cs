using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class RotateBlade : MonoBehaviour
{
    [SerializeField] private Vector3 rotationAxis = Vector3.up;
    [SerializeField] private float rotationSpeed = 200f;

    void Update()
    {

        transform.Rotate(rotationAxis * rotationSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            AudioManager.Instance.PlayKnifeSound();
            LifeController.Instance?.LoseLife();
        }
    }
}

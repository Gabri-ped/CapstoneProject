using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlat2 : MonoBehaviour
{
    [Header("Movimento piattaforma")]
    [SerializeField] private float distance = 5f;      // quanto si muove su/giù
    [SerializeField] private float speed = 2f;         // velocità del movimento
    [SerializeField] private float stopDuration = 2f;  // pausa agli estremi

    private Vector3 startPos;
    private Vector3 endPos;
    private bool movingUp = true;
    private bool isWaiting = false;

    private void Start()
    {
        startPos = transform.position;
        endPos = startPos + Vector3.up * distance; // 👈 movimento verticale
        StartCoroutine(MovePlatform());
    }

    private IEnumerator MovePlatform()
    {
        while (true)
        {
            if (!isWaiting)
            {
                Vector3 target = movingUp ? endPos : startPos;
                transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

                // Se raggiunge un’estremità → ferma e poi inverte
                if (Vector3.Distance(transform.position, target) < 0.01f)
                {
                    isWaiting = true;
                    yield return new WaitForSeconds(stopDuration);
                    movingUp = !movingUp;
                    isWaiting = false;
                }
            }
            yield return null;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // 🔹 Il player si muove insieme alla piattaforma
            collision.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // 🔹 Quando scende, non segue più la piattaforma
            collision.transform.SetParent(null);
        }
    }
}

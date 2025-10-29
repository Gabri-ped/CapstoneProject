using UnityEngine;

public class MovingSaw : MonoBehaviour
{
    [SerializeField] private Vector3 direction = Vector3.right; // direzione del binario (x=destra/sinistra, y=su/giù, z=avanti/indietro)
    [SerializeField] private  float distance = 5f;               // distanza massima di movimento
    [SerializeField] private float speed = 2f;                  // velocità avanti/indietro
    [SerializeField] private float rotationSpeed = 360f;       
    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        float offset = Mathf.PingPong(Time.time * speed, distance);
        transform.position = startPos + direction.normalized * offset;

        transform.Rotate(Vector3.right * rotationSpeed * Time.deltaTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            LifeController.Instance ?.LoseLife();
        }
    }
}

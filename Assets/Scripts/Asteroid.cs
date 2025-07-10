using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public float speed = 2f;

    [Header("Životy")]
    [Tooltip("Kolik poškození asteroid přežije")]
    public int health = 5;

    public int damage = 1;
    private float rotationSpeed;


    [Header("Rotace")]
    private int maxRPM = 30;


    void Start()
    {
        rotationSpeed = Random.Range(-GameMath.SetRPM(maxRPM), GameMath.SetRPM(maxRPM));

    }

    void Update()
    {
        transform.position += Vector3.down * speed * Time.deltaTime;
        AsteroidRotation();
    }



    //Vyvolání kolize & následků    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            Debug.Log("[ASTEROID] Collision with projectile");

            // Zjisti damage ze střely 
            projectileScript bullet = other.GetComponent<projectileScript>();
            if (bullet != null)
            {
                TakeDamage(bullet.damage);
            }

            // Znič střelu
            Destroy(other.gameObject);
        }
    }

    //Obržení poškození
    void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject); // znič asteroid
        }
    }

    void AsteroidRotation()
    {
        transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);
    }
    
}
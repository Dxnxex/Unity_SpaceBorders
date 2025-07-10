using UnityEngine;

public class Asteroid : MonoBehaviour
{

    [Header("Pohyb")]
    [Tooltip("Jakou rychlostí se bude asteroid pohybovat")]
    public float speed = 2f;

    [Header("Životy")]
    [Tooltip("Kolik poškození asteroid přežije")]
    public int health = 5;


    [Header("Rotace")]
    [Tooltip("Kolik je maximálních otáček za minutu")]
    private int maxRPM = 60;
    private float rotationSpeed;

    [Header("Sprites")]
    [Tooltip("Obrázky určené k náhodnému určení")]
    public Sprite[] possibleSprites;


    void Start()
    {

        //Nastaví náhodný sprite
        RandomSprite();


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


    void RandomSprite()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (possibleSprites != null && possibleSprites.Length > 0)
        {
            int index = Random.Range(0, possibleSprites.Length);
            sr.sprite = possibleSprites[index];
        }
    }
}
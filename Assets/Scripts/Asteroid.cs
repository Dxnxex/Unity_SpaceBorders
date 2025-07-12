using UnityEngine;

public class Asteroid : MonoBehaviour
{

    [Header("Pohyb")]
    [Tooltip("Jakou rychlostí se bude asteroid pohybovat")]
    public float speed = 4f;
    private float originalSpeed;
    private float slowTimer = 0f;

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
        //Pre-set virables
        originalSpeed = speed;

        //Nastaví náhodný sprite
        RandomSprite();


        rotationSpeed = Random.Range(-GameMath.SetRPM(maxRPM), GameMath.SetRPM(maxRPM));

    }

    void Update()
    {
        transform.position += Vector3.down * speed * Time.deltaTime;
        AsteroidRotation();
        resetCooldowns();
    }



    //Vyvolání kolize & následků    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {

            // Zjisti damage ze střely 
            projectileScript bullet = other.GetComponent<projectileScript>();
            if (bullet != null)
            {
                TakeDamage(bullet.damage);
            }

            #region IcyCore
            if (RelicManager.Instance.isRelicEnabled<IcyCoreRelic>())
            {
                var icyCore = RelicManager.Instance.GetRelic<IcyCoreRelic>();
                ApplySlow(icyCore.slowFactor, icyCore.slowTimer);
            }
            #endregion

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

    public void ApplySlow(float slowFactor, float duration)
    {
        speed = originalSpeed * (1f - slowFactor / 100f);
        slowTimer = duration;
    }


    private void resetCooldowns()
    {

        //Slow
        if (slowTimer > 0)
        {
            slowTimer -= Time.deltaTime;
            if (slowTimer <= 0) { speed = originalSpeed; }
        }
        
        //Other
        
    }


}
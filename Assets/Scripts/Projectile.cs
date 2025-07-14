using UnityEngine;

public class projectileScript : MonoBehaviour
{
    public float speed = 60f;
    public float lifeTime = 3f;
    public int damage = 1;
    private static int shotCounter = 0;

    void Start()
    {
        //Vystřelení střely & Počítání střel
        shotCounter++;

        #region Empower

        if (RelicManager.Instance.isRelicEnabled<Empower>())
        {
            damage += RelicManager.Instance.GetRelic<Empower>().damage;
        }

        #endregion

        #region DoublePower

        if (RelicManager.Instance.isRelicEnabled<DoublePower>())
        {
            if (shotCounter == RelicManager.Instance.GetRelic<DoublePower>().shotCooldown)
            {
                damage *= 2;
                shotCounter = 0;
                Debug.Log("Double Power Relic activated! Damage doubled to: " + damage);
  
            }
        }

        #endregion

        #region Debugs

            Debug.Log("Projectile damage: " + damage);
            Debug.Log("Projectile counter: " + shotCounter);

        #endregion
        
        //Zničení střely po určité době
        Destroy(gameObject, lifeTime);
        
        }

        void Update()
        {
            //Let kupředu
             transform.position += transform.right * speed * Time.deltaTime;
        }
}

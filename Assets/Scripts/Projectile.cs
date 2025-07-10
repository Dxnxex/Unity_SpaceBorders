using UnityEngine;

public class projectileScript : MonoBehaviour
{
    public float speed = 60f;
    public float lifeTime = 3f;
    public int damage = 1; //
    
        void Start()
        {
            //Zničení střely po určité době
            Destroy(gameObject, lifeTime);
        }

        void Update()
        {
            //Let kupředu
             transform.position += transform.right * speed * Time.deltaTime;
        }
}

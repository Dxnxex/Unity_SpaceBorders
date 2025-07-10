using UnityEngine;
using UnityEngine.InputSystem;

public class shipScript : MonoBehaviour
{

    public float speed = 5f;
    public float rotationSpeed = 3f;
    public float acceleration = 15f;
    public float deceleration = 1.0f;
    public float maxSpeed = 35.0f;
    private float currentSpeed = 0f;

    public GameObject ProjectilePrefab;  // Prefab střely
    public Transform firePoint;      // Místo odkud střela vyletí
    public float shootCooldown = 0.2f;
    private float lastShotTime;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameObject.name = "Dxnxex";
    }

    // Update is called once per frame
    void Update()
    {

        MoveToRight();
        MoveToLeft();
        LookAtMouse();


        if (Keyboard.current.spaceKey.isPressed && Time.time >= lastShotTime + shootCooldown)
        {
            Shoot();
            lastShotTime = Time.time;
        }



        bool accelerating = Keyboard.current.wKey.isPressed;

        if (accelerating)
        {
            currentSpeed += acceleration * Time.deltaTime;
        }
        else
        {
            currentSpeed -= deceleration * Time.deltaTime;
        }

        //Zastavení & Maximální rychlost
        currentSpeed = Mathf.Clamp(currentSpeed, 0f, maxSpeed);

        //Pohyb
        transform.position += transform.right * currentSpeed * Time.deltaTime;

    }




    private void MoveToLeft()
    {

        bool keyCheck = Keyboard.current.aKey.isPressed;

        if (keyCheck)
        {
            transform.Translate(Vector3.up * speed * Time.deltaTime);
        }

    }

    private void MoveToRight()
    {
        bool keyCheck = Keyboard.current.dKey.isPressed;

        if (keyCheck)
        {
            transform.Translate(Vector3.down * speed * Time.deltaTime);
        }
    }



    private void LookAtMouse()
    {
        Vector2 mousePos = Mouse.current.position.ReadValue();
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 0f));
        worldPos.z = 0f;

        Vector2 direction = worldPos - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        //Cílová rotace & Pomalé otáčení za ní
        Quaternion targetRotation = Quaternion.Euler(0f, 0f, angle);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }


    private void Shoot()
    {
        Instantiate(ProjectilePrefab, firePoint.position, transform.rotation);
    }



}

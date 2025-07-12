using UnityEngine;
using UnityEngine.InputSystem;

public class shipScript : MonoBehaviour
{
    [Header("Rychlost")]
    public float speed = 5f;
    public float acceleration = 15f;
    public float deceleration = 1.0f;
    public float maxSpeed = 35.0f;
    private float currentSpeed = 0f;

    [Header("Rotace")]
    public float rotationSpeed = 3f;

    [Header("Střelba")]
    public GameObject ProjectilePrefab;  // Prefab střely
    public Transform firePoint;      // Místo odkud střela vyletí
    private int magazine = 0;
    public int magazineMax = 25;
    public float projectileSpeed = 55f;
    public int ammo = 120;
    public int ammoMax = 900;
    public float precision = 14f;
    public float fireSpeedCooldown = 0.2f;
    private float lastShotTime;

    [Header("Obrana")]
    public float lives = 100;
    public float shiled = 200;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //První nabití
        magazine = magazineMax;

        #region Belt
        
        //Získání relikvie
        RelicManager.Instance.obtainRelic<BeltRelic>();


        if (RelicManager.Instance.isRelicEnabled<BeltRelic>())
        {
            ammo += RelicManager.Instance.GetRelic<BeltRelic>().ammo;
            ammoMax += RelicManager.Instance.GetRelic<BeltRelic>().ammo;

            RelicManager.Instance.GetRelic<BeltRelic>().obtained = true;

        }

        #endregion


        #region Debugs

        Debug.Log("Ship started with ammo: " + ammo);
        Debug.Log("Ship started with max ammo: " + ammoMax);

        #endregion

    }

    // Update is called once per frame
    void Update()
    {

        MoveToRight();
        MoveToLeft();
        LookAtMouse();

        #region Střílení
        if (Keyboard.current.spaceKey.isPressed && Time.time >= lastShotTime + fireSpeedCooldown)
        {
            Shoot();
            lastShotTime = Time.time;
        }
        #endregion


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

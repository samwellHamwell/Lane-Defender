using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private PlayerInput myPlayerInput;
    private InputAction move;
    private InputAction shoot;
    [SerializeField] private float Speed;
    [SerializeField] private float MoveDirection;
    bool moving;
    private Rigidbody2D rigidBody2D;
    [SerializeField] private GameObject BulletPrefab;
    [SerializeField] private GameObject ExplosionPrefab;
    [SerializeField] private GameObject BulletSpawn;
    private float shootTimer;
    [SerializeField] private float ShootTimerMax = .8f;
    bool holding;
    SoundManager SM;
    // Start is called before the first frame update
    void Start()
    {
        myPlayerInput = GetComponent<PlayerInput>();
        move = myPlayerInput.currentActionMap.FindAction("Move");
        shoot = myPlayerInput.currentActionMap.FindAction("Shoot");
        move.started += Move_started;
        move.canceled += Move_canceled;
        shoot.started += Shoot_started;
        shoot.canceled += Shoot_canceled;
        rigidBody2D = GetComponent<Rigidbody2D>();
        shootTimer = ShootTimerMax;
        SM = FindObjectOfType<SoundManager>();
    }
    private void Shoot_canceled(InputAction.CallbackContext obj)
    {
        holding = false;
    }
    private void Shoot_started(InputAction.CallbackContext obj)
    {
        SpawnBullet();
        holding = true;
    }
    private void OnDestroy()
    {
        StopListening();
    }
    public void StopListening()
    {
        move.started -= Move_started;
        move.canceled -= Move_canceled;
        shoot.started -= Shoot_started;
        shoot.canceled -= Shoot_canceled;
    }
    private void Move_canceled(InputAction.CallbackContext obj)
    {
        moving = false;
    }
    private void Move_started(InputAction.CallbackContext obj)
    {
        moving = true;
    }
    private void FixedUpdate()
    {
        if (moving)
        {
            rigidBody2D.velocity = new Vector2(0, Speed * MoveDirection);
        }
        else
        {
            rigidBody2D.velocity = Vector2.zero;
        }
    }
    void SpawnBullet()
    {
        Instantiate(ExplosionPrefab, BulletSpawn.transform);
        Instantiate(BulletPrefab, BulletSpawn.transform);
        AudioSource.PlayClipAtPoint(SM.TankShoot1, gameObject.transform.position);
    }
    // Update is called once per frame
    void Update()
    {
        if (moving)
        {
            MoveDirection = move.ReadValue<float>();
        }
        if(holding)
        {
            shootTimer -= Time.deltaTime;
            if(shootTimer < 0)
            {
                SpawnBullet();
                shootTimer = ShootTimerMax;
            }
        }
        else
        {
            shootTimer = ShootTimerMax;
        }
    }
}

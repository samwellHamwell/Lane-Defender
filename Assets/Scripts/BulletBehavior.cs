using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    [SerializeField] private float BulletSpeed;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody2D>().velocity = Vector2.right * BulletSpeed;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "KillBox")
        {
            Destroy(gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    [SerializeField] private float MoveSpeed;
    [SerializeField] private GameObject Explosion;
    [SerializeField] private uint Lives;
    private Animator anim;
    private GameManager GM;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody2D>().velocity = Vector2.left * MoveSpeed;
        anim = GetComponent<Animator>();
        GM = FindObjectOfType<GameManager>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            Instantiate(Explosion, collision.transform.position, Quaternion.identity);
            Destroy(collision.gameObject);
            LoseLife();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "KillBox")
        {
            //player loses life
            GM.LoseLife();
            Destroy(gameObject);
        }
        if (collision.gameObject.tag == "Player")
        {
            //player loses life
            GM.LoseLife();
            Instantiate(Explosion, collision.transform.position, Quaternion.identity);
            Lives = 1;
            LoseLife();
        }
    }
    void LoseLife()
    {
        Lives--;
        anim.SetTrigger("Hit");
        switch(Lives)
        {
            case 0:
                GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                GetComponent<CapsuleCollider2D>().enabled = false;
                anim.SetTrigger("Die");
                break;
        }
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}

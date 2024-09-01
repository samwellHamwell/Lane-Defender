using System.Collections;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    [SerializeField] private float MoveSpeed;
    [SerializeField] private GameObject Explosion;
    [SerializeField] private uint Lives;
    private Animator anim;
    private GameManager GM;
    private SoundManager SM;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody2D>().velocity = Vector2.left * MoveSpeed;
        anim = GetComponent<Animator>();
        GM = FindObjectOfType<GameManager>();
        SM = FindObjectOfType<SoundManager>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            Instantiate(Explosion, collision.transform.position, Quaternion.identity);
            Destroy(collision.gameObject);
            StopMovement();
            LoseLife();
        }
    }
    void StopMovement()
    {
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        StartCoroutine(Resume());
    }
    IEnumerator Resume()
    {
        yield return new WaitForSeconds(.5f);
        if(Lives > 0)
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.left * MoveSpeed;
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
        AudioSource.PlayClipAtPoint(SM.EnemyHit1, gameObject.transform.position);
        anim.SetTrigger("Hit");
        switch(Lives)
        {
            case 0:
                GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                GetComponent<CapsuleCollider2D>().enabled = false;
                GM.UpdateScore(100);
                anim.SetTrigger("Die");
                break;
        }
    }
    public void Die()
    {
        AudioSource.PlayClipAtPoint(SM.EnemyDead1, gameObject.transform.position);
        Destroy(gameObject);
    }
}

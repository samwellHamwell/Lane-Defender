using UnityEngine;

public class ExplosionBehavior : MonoBehaviour
{
    float timer = .5f;
    // Update is called once per frame
    void Update()
    {
        //zach don't be mad this is before I learned about animation events
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            Destroy(gameObject);
        }
    }
}

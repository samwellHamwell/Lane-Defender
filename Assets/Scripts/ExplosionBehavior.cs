using UnityEngine;

public class ExplosionBehavior : MonoBehaviour
{
    float timer = .5f;
    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            Destroy(gameObject);
        }
    }
}

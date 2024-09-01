using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioClip EnemyHit;
    [SerializeField] private AudioClip EnemyDead;
    [SerializeField] private AudioClip LifeLost;
    [SerializeField] private AudioClip TankShoot;

    public AudioClip EnemyHit1 { get => EnemyHit; set => EnemyHit = value; }
    public AudioClip EnemyDead1 { get => EnemyDead; set => EnemyDead = value; }
    public AudioClip LifeLost1 { get => LifeLost; set => LifeLost = value; }
    public AudioClip TankShoot1 { get => TankShoot; set => TankShoot = value; }
}

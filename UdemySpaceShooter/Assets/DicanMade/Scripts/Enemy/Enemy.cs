
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Enemy")]
    [SerializeField] float health = 100;
    [SerializeField] float shotCounter;
    [SerializeField] float minTimeBetweenShots;
    [SerializeField] float maxTimeBetweenShots;
    [SerializeField] float projectileSpeed = 10f;

    [Header("Prefabs")]
    [SerializeField] GameObject explosionPrefab;
    [SerializeField] GameObject laserPrefab;

    [Header("Audio")]
    [SerializeField] AudioClip shootSound;
    [SerializeField] float shootSoundVolume;
    [SerializeField] AudioClip deathSound;
    [SerializeField] float deathSoundVolume;

    // Start is called before the first frame update
    void Start() => shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);

    // Update is called once per frame
    void Update()
    {
        CountDownAndShoot();
    }

    private void CountDownAndShoot()
    {
        shotCounter -= Time.deltaTime;
        if(shotCounter <= 0f)
        {
            Fire();
            shotCounter = Random.Range(minTimeBetweenShots,maxTimeBetweenShots);
        }
    }
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.tag == "PlayerShot")
        {
            DamageDealer damagedealer = other.GetComponent<DamageDealer>();
            ProcessHit(damagedealer);
        }
        
    }

    private void Fire()
    {
        GameObject laser = Instantiate(laserPrefab, new Vector2(transform.position.x,transform.position.y - 1), Quaternion.identity) as GameObject;
        laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0,-projectileSpeed);
        AudioSource.PlayClipAtPoint(shootSound, Camera.main.transform.position, shootSoundVolume);
    }

    private void ProcessHit(DamageDealer damagedealer)
    {
        health -= damagedealer.GetDamage();
        damagedealer.Hit();
        if(health <= 0)
        {
            Die();
        }

    }

    private void Die()
    {
        GameObject explosion =  Instantiate(explosionPrefab, transform.transform.position, Quaternion.identity) as GameObject;
        AudioSource.PlayClipAtPoint(deathSound, Camera.main.transform.position, deathSoundVolume);
        Destroy(explosion, 0.4f);
        Destroy(gameObject);
    }
}

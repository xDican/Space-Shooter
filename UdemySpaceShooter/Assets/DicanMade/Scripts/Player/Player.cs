using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Player")]
    //configuration parameters
    [SerializeField] float moveSpeed = 10f;                      // Movement Speed of the player / ship
    [SerializeField] float health = 100;                        //Health of the player / ship

    [Header("Laser")]
    [SerializeField] GameObject laserPrefab;                   //Laser Prefab
    [SerializeField] [Range(1, 20)] float projectileSpeed = 10f;              //Laser Speed
    [SerializeField] float projectileFiringPeriod = 0.1f;      //Time between bullet fired

    [Header("Sound")]
    [SerializeField] AudioClip shootSound;                      //SFX when player shoots
    [SerializeField] float shootSoundVolume;                    //Volume of the SFX shoot
    [SerializeField] AudioClip deathSound;                      //Players death sound
    [SerializeField] float deathSoundVolume;                    //Volumne of the SFX player death

    [Header("Miscelaneous")]
    [SerializeField] SceneManger sceneManager;                  //

    private float xMin = 0f;
    private float xMax = 0f;
    private float yMin;
    private float yMax;
    private bool isShooting;
    Coroutine fireCoroutine;
    Sprite ship;

    


    // Start is called before the first frame update
    void Start()
    {

        SetUpMoveBoundaries();
        isShooting = false;
    }


    // Update is called once per frame
    void Update()
    {
        Move();
        Fire();
    }


    private void Fire()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            fireCoroutine = StartCoroutine(FireContinuosly());
        }
        if (Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(fireCoroutine);
        }
    }

    private void Move()
    {
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;
        var newXPos = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);
        var newYPos = Mathf.Clamp(transform.position.y + deltaY, yMin, yMax);
        transform.position = new Vector2(newXPos, newYPos);
        transform.rotation = AimToMousePosition();
    }

    private void SetUpMoveBoundaries()
    {
        Camera gameCamera = Camera.main;
        ship = GetComponent<SpriteRenderer>().sprite;
        float halfShipSize = ship.bounds.size.x / 2;
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + halfShipSize;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x;
        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + halfShipSize * 2.5f;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - halfShipSize * 2.5f;
    }

    IEnumerator FireContinuosly()
    {
        isShooting = true;

        while (true)
        {
            GameObject laser = Instantiate(laserPrefab, transform.position, AimToMousePosition()) as GameObject;
            Vector2 direction = ShootToMousePosition();
            laser.GetComponent<Rigidbody2D>().velocity = direction * projectileSpeed;
            AudioSource.PlayClipAtPoint(shootSound, Camera.main.transform.position, shootSoundVolume);
            yield return new WaitForSeconds(projectileFiringPeriod);
        }
    }

    private Vector2 ShootToMousePosition()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (Vector2)(mousePos - transform.position);
        direction.Normalize();
        return direction;
    }

    private Quaternion AimToMousePosition()
    {
        var mousePos = Input.mousePosition;
        var screenPoint = Camera.main.WorldToScreenPoint(transform.position);
        var offSet = new Vector2(mousePos.x - screenPoint.x, mousePos.y - screenPoint.y);
        var angle = Mathf.Atan2(offSet.y, offSet.x) * Mathf.Rad2Deg;
        return Quaternion.Euler(new Vector3(0, 0, angle + 270));
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "EnemyShot" || other.gameObject.tag == "Enemy")
        {
            Debug.Log("Enemy shot me");
            DamageDealer damagedealer = other.GetComponent<DamageDealer>();
            ProcessHit(damagedealer);
        }
    }
    void ProcessHit(DamageDealer _damagedealer)
    {
        health -= _damagedealer.GetDamage();
        Debug.Log("Player Health = " + health);
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        AudioSource.PlayClipAtPoint(deathSound, Camera.main.transform.position, deathSoundVolume);
        sceneManager.LoadGameOver();
        Destroy(gameObject);
    }

    
}

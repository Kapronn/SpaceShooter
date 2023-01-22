using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{

    [Header("Player Ñharacteristics")]
    [SerializeField] float health = 500f;
    [SerializeField] float moveSpeed = 1f;

    [Header("Shooting Settings")]
    [SerializeField] GameObject laserPrefab;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float rateOfFire = 0.1f;
    Coroutine firingCoroutine;

    [Header("Boundles")]
    [SerializeField] float gapX = 1f;
    [SerializeField] float gapY = 1f;
    float xMin;
    float xMax;
    float yMin;
    float yMax;

    [Header("VFX")]
    [SerializeField] GameObject explosionVFX;
    [SerializeField] float explosionTimeVFX;

    [Header("SFX")]
    [SerializeField] AudioClip explosionSFX;
    [SerializeField] [Range(0, 1)] float soundExplosionVolume = 0.05f;
    [SerializeField] AudioClip shootingSFX;
    [SerializeField] [Range(0, 1)] float soundShootingVolume = 0.15f;


    void Start()
    {
        SetBoundelsOnCamera();
    }


    void Update()
    {
        Move();
        Fire();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        DamageDealer damageDealer = collision.GetComponent<DamageDealer>();
        ProcessHit(damageDealer);

    }

    private void ProcessHit(DamageDealer damageDealer)
    {
        health -= damageDealer.GetDamageDealer();
        damageDealer.Hit();
        if (!damageDealer) { return; }
        if (health <= 0)
        {
            Die();

        }
    }

    void Die()
    {
        FindObjectOfType<Level>().LoadGameOverScene();
        Destroy(gameObject);
        GameObject explodeVFX = Instantiate(explosionVFX, transform.position, Quaternion.identity) as GameObject;
        AudioSource.PlayClipAtPoint(explosionSFX, Camera.main.transform.position, soundExplosionVolume);
        Destroy(explodeVFX, explosionTimeVFX);

    }

    private void Move()
    {
        float deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        float deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;

        float moveToX = Mathf.Clamp(transform.position.x, xMin, xMax) + deltaX;
        float moveToY = Mathf.Clamp(transform.position.y, yMin, yMax) + deltaY;

        transform.position = new Vector3(moveToX, moveToY);
    }
    private void Fire()
    {
        if (Input.GetButtonDown("Fire1"))
        {

            firingCoroutine = StartCoroutine(FireContiniously());
        }
        if (Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(firingCoroutine);
        }

    }
    IEnumerator FireContiniously()
    {
        while (true)
        {
            GameObject laser = Instantiate(laserPrefab, transform.position, Quaternion.identity) as GameObject;
            laser.GetComponent<Rigidbody2D>().velocity = new Vector3(0, projectileSpeed);
            AudioSource.PlayClipAtPoint(shootingSFX, Camera.main.transform.position, soundShootingVolume);
            yield return new WaitForSeconds(rateOfFire);
        }
    }

    private void SetBoundelsOnCamera()
    {
        Camera mainCamera = Camera.main;
        xMin = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + gapX;
        xMax = mainCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - gapX;
        yMin = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + gapY;
        yMax = mainCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - gapY;
    }
    // For HealthDisplay
    public float Health()
    {
        return health;
    }
}

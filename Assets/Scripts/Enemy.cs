using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Enemy Characteristics")]
    [SerializeField] float health = 100;
    [SerializeField] int scoreValue = 100;

    [Header("Projectile Settings")]
    [SerializeField] GameObject enemyLaserPrefab;
    [SerializeField] float laserMoveSpeed = 40f;
    [SerializeField] float shotCounter;
    [SerializeField] float minTimeBetweenShots = 0.2f;
    [SerializeField] float maxTimeBetweenShots = 3f;

    [Header("VFX")]
    [SerializeField] GameObject explosionVFX;

    [Header("SFX")]
    [SerializeField] AudioClip explosionSFX;
    [SerializeField] [Range(0, 1)] float soundExplosionVolume = 0.05f;
    [SerializeField] AudioClip shootingSFX;
    [SerializeField] [Range(0, 1)] float soundShootingVolume = 0.15f;

    private void Start()
    {
        shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
    }
    void Update()
    {
        CoundDownAndShot();
        
    }

    private void CoundDownAndShot()
    {
        shotCounter -= Time.deltaTime;
        if (shotCounter <= 0)
        {
            Fire();
            shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
        }
    }

    private void Fire()
    {
        GameObject laser = Instantiate(enemyLaserPrefab, transform.position, Quaternion.identity) as GameObject;
        laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -laserMoveSpeed);
        AudioSource.PlayClipAtPoint(shootingSFX, Camera.main.transform.position, soundShootingVolume);

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.GetComponent<DamageDealer>();
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

    private void Die()
    {
        FindObjectOfType<GameSession>().AddToScore(scoreValue);
        Destroy(gameObject);
        GameObject explodeVFX = Instantiate(explosionVFX, transform.position, Quaternion.identity);
        AudioSource.PlayClipAtPoint(explosionSFX, Camera.main.transform.position, soundExplosionVolume);
        Destroy(explodeVFX, 2);
        
    }
    
}

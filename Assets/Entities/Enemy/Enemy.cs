using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    // to enemies only
    public GameObject projectile;
    public float speedBeam = 5f;
    //frecuency
    public float shotsPerSeconds = 0.5f;

    public float health = 150f;
    public int scoreValue = 150;

    private ScoreKeeper scoreKeeper;

    public AudioClip fireSound;
    public AudioClip deathSound;

    private void Start()
    {
        scoreKeeper = GameObject.Find("Score").GetComponent<ScoreKeeper>();
    }

    private void Update()
    {
        float probability = Time.deltaTime * shotsPerSeconds;
        //randow values between 0 to 1
        if (Random.value < probability)
        {
            Fire();
        }
    }

    void Fire()
    {

        // Onlys shot projectile enemies
        // no longer need beacuse we are using layer colliders
        //Vector3 startPosiiton = transform.position + new Vector3(0, -1, 0);
        //GameObject missile = Instantiate(projectile, startPosiiton, Quaternion.identity) as GameObject;
        GameObject missile = Instantiate(projectile, transform.position, Quaternion.identity) as GameObject;
        missile.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -speedBeam);
        AudioSource.PlayClipAtPoint(fireSound, transform.position);
    }

    // destroy enemy
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Projectile missile = collision.gameObject.GetComponent<Projectile>();
        if (missile)
        {
            health -= missile.GetDamage();
            missile.Hit();
            if (health <= 0)
            {
                Die();
            }

        }
    }

    private void Die()
    {
        AudioSource.PlayClipAtPoint(deathSound, transform.position);
        Destroy(gameObject);
        scoreKeeper.Score(scoreValue);
    }
}

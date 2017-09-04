using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public float speed = 5.0f;
    public float padding = 1f;

    float xmin = -5;
    float xmax = 5;

    public GameObject projectile;
    public float projectileSpeed;
    public float fireRate = 0.2f;

    public float health = 250f;

    public AudioClip fireSound;

    // Use this for initialization
    void Start () {

        // Get screen size 
        float distance = transform.position.z - Camera.main.transform.position.z;
        //use relative 0 -1
        Vector3 leftBounderies = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance));
        Vector3 rightBounderies = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distance));
        xmin = leftBounderies.x + padding;
        xmax = rightBounderies.x - padding;
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            InvokeRepeating("fire", 0.000001f, fireRate);
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            CancelInvoke("fire");
        }

		if (Input.GetKey(KeyCode.LeftArrow))
        {
            //transform.position += new Vector3(-speed * Time.deltaTime, 0,0);
            //or
            transform.position += Vector3.left * speed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            // transform.position += new Vector3(+speed * Time.deltaTime, 0, 0);
            //or
            transform.position += Vector3.right * speed * Time.deltaTime;
        }

        //resctrict player to the game space
        float newX = Mathf.Clamp(transform.position.x, xmin, xmax);
        transform.position = new Vector3(newX, transform.position.y, transform.position.z);
    }

    void fire()
    {
        Vector3 offset = transform.position + new Vector3(0,1,0);
        GameObject beam = Instantiate(projectile, offset , Quaternion.identity) as GameObject;
        beam.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
        AudioSource.PlayClipAtPoint(fireSound, transform.position);
       
    }

    //destroy player
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Projectile missile = collision.gameObject.GetComponent<Projectile>();
        if (missile)
        {
            health -= missile.GetDamage();
            missile.Hit();
            if (health <= 0)
            {
                LevelManager man = GameObject.Find("LevelManager").GetComponent<LevelManager>();
                man.LoadLevel("Win Screen");
                Destroy(gameObject);
            }

        }
    }
}

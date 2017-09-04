using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFormation : MonoBehaviour {

    public GameObject enemyPrefab;
    public float width = 10f;
    public float height = 5;

    private bool movingRight = true;
    public float speed = 5f;

    float xmin;
    float xmax;

    public float spawDelay = 0.5f;

    // Use this for initialization
    void Start () {

        float distance = transform.position.z - Camera.main.transform.position.z;
        //use relative 0 -1
        Vector3 leftBounderies = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance));
        Vector3 rightBounderies = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distance));
        xmin = leftBounderies.x;
        xmax = rightBounderies.x;

        SpwnUntilFull();
    }

    void SpwnEnemies()
    {
        // foreach position
        foreach (Transform child in transform)
        {
            // Put enemy inside of formation
            GameObject enemy = Instantiate(enemyPrefab, child.transform.position, Quaternion.identity) as GameObject;
            enemy.transform.parent = child;

        }

    }

    void SpwnUntilFull()
    {
        Transform nextPosition = NextFreePosition();
        if (nextPosition)
        {
            GameObject enemy = Instantiate(enemyPrefab, nextPosition.position, Quaternion.identity) as GameObject;
            enemy.transform.parent = nextPosition;
        }
        if (NextFreePosition())
        {
            Invoke("SpwnUntilFull", spawDelay);
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(width, height));
    }

    // Update is called once per frame
    void Update()
    {
        if (movingRight)
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
        }
        else
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
        }

        float right = transform.position.x + (0.5f * width);
        float left = transform.position.x - (0.5f * width);
        if (left < xmin)
        {
            movingRight = true;
        }
        else if (right > xmax)
        {
            movingRight = false;
        }

        if (AllMemebersDead())
        {
            SpwnUntilFull();
        }
    }

    bool AllMemebersDead()
    {
        foreach (Transform child in transform)
        {
            if (child.childCount > 0)
            {
                return false;
            }
        }
        return true;
    }


    Transform NextFreePosition()
    {
        foreach (Transform child in transform)
        {
            if (child.childCount == 0)
            {
                return child;
            }
        }
        return null;
    }
}

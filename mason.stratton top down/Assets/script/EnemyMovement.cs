using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float movementspeed = 5;
    public float detectionRadius = 5;
    public bool canMove = false;
    private bool movementDirection = false;
    public bool isFollowing = false;

    private Rigidbody2D myRB;
    private CircleCollider2D detectionZone;
    private Vector2 up;
    private Vector2 down;
    private Vector2 zero;
    public Transform playerTarget;

    // Start is called before the first frame update
    void Start()
    {
        up = new Vector2(0, movementspeed);
        down = new Vector2(0, -movementspeed);
        zero = new Vector2(0, 0);

        playerTarget = GameObject.Find("playersprite").transform;

        myRB = GetComponent<Rigidbody2D>();
        detectionZone = GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        detectionZone.radius = detectionRadius;

        if (isFollowing == false)
            myRB.velocity = zero;
       
        else if (isFollowing == true)
        {
            Vector3 lookPos = playerTarget.position - transform.position;
            float angle = Mathf.Atan2(lookPos.y, lookPos.x) * Mathf.Rad2Deg;
            myRB.rotation = angle;
            lookPos.Normalize();

            myRB.MovePosition(transform.position + (lookPos * movementspeed * Time.deltaTime));
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Contains("bullit"))
        {
            Destroy(collision.gameObject);
            this.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Contains("player"))
            isFollowing = true;

        if (collision.gameObject.name == "trigger1")
            movementDirection = false;

        else if(collision.gameObject.name == "trigger2")
            movementDirection = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name.Contains("player"))
            isFollowing = false;
    }
}

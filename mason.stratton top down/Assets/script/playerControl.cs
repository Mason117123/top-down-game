using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerControl : MonoBehaviour
{
    private Rigidbody2D myRB;
    public float speed = 10;
    public float bullitspeed = 15;
    public float bullitLifespam = 1;
    public int playerHealth = 3;
    public GameObject bullit;

    // Start is called before the first frame update
    void Start()
    {
        myRB = GetComponent<Rigidbody2D>();
        playerHealth = 3;
    }

    // Update is called once per frame
    void Update()
    {
        if(playerHealth <=0)
        {
            transform.SetPositionAndRotation(new Vector2(), new Quaternion());
            playerHealth = 3;
        }

        Vector2 velocity = myRB.velocity;

        velocity.x = Input.GetAxisRaw("Horizontal") * speed;
        velocity.y = Input.GetAxisRaw("Vertical") * speed;

        myRB.velocity = velocity;

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            GameObject b = Instantiate(bullit, new Vector2(transform.position.x, transform.position.y + 1), transform.rotation);
            b.GetComponent<Rigidbody2D>().velocity = new Vector2(0, bullitspeed);
            Destroy(b, bullitLifespam);
        }

        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            GameObject b = Instantiate(bullit, new Vector2(transform.position.x, transform.position.y - 1), transform.rotation);
            b.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -bullitspeed);
            Destroy(b, bullitLifespam);
        }

        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            GameObject b = Instantiate(bullit, new Vector2(transform.position.x - 1, transform.position.y), transform.rotation);
            b.GetComponent<Rigidbody2D>().velocity = new Vector2(-bullitspeed, 0);
            Destroy(b, bullitLifespam);
        }

        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            GameObject b = Instantiate(bullit, new Vector2(transform.position.x + 1, transform.position.y), transform.rotation);
            b.GetComponent<Rigidbody2D>().velocity = new Vector2(bullitspeed, 0);
            Destroy(b, bullitLifespam);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name == "enemysprite")
        {
            playerHealth--;
        }

        else if((collision.gameObject.name == "pickup") && (playerHealth < 3))
        {
            playerHealth++;
            collision.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "enemytrigger")
        {
            GameObject.Find("enemysprite").GetComponent<EnemyMovement>().canMove = true;
            GameObject.Find("enemysprite").GetComponent<Rigidbody2D>().constraints &= ~RigidbodyConstraints2D.FreezePositionY;
            Destroy(collision.gameObject);
        }
    }

}

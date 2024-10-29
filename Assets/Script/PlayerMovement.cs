using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float AccDown = -10f;
    [SerializeField] private float JumpForce = 50f;
    [SerializeField] private float maxX = 5f;
    [SerializeField] private float maxY = 3f;

    private bool UserControl;
    private Rigidbody2D Rb;


    // Start is called before the first frame update
    void Start()
    {   
        Rb = GetComponent<Rigidbody2D>();
        UserControl = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(UserControl)
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                Rb.AddForce(JumpForce*Vector2.up, ForceMode2D.Impulse);
            }
        }
    }

    void FixedUpdate()
    {
        Vector3 position = transform.position;
        Vector2 velocity = Rb.velocity;

        Vector2 acc = new Vector2(0, AccDown);
        velocity = velocity + acc * Time.deltaTime;

        position.x = Mathf.Clamp(position.x, -maxX, maxX);
        position.y = Mathf.Clamp(position.y, -maxY, maxY);
        transform.position = position;
        Rb.velocity = velocity;
    }

    void Dead()
    {
        UserControl = false;
        GameObject pt = GameObject.Find("Platform");
        if(pt != null)
        {
            pt.GetComponent<Animator>().enabled = false;
        }

        pt = GameObject.Find("0Moving");
        if(pt != null)
        {
            pt.GetComponent<Animator>().enabled = false;
        }        
        pt = GameObject.Find("1Moving");
        if(pt != null)
        {
            pt.GetComponent<Animator>().enabled = false;
        }
        pt = GameObject.Find("2Moving");
        if(pt != null)
        {
            pt.GetComponent<Animator>().enabled = false;
        }
        pt = GameObject.Find("3Moving");
        if(pt != null)
        {
            pt.GetComponent<Animator>().enabled = false;
        }
        pt = GameObject.Find("4Moving");
        if(pt != null)
        {
            pt.GetComponent<Animator>().enabled = false;
        }

        GameObject spawner = GameObject.Find("Obstacle");
        if(spawner != null) spawner.SetActive(false);

        FindObjectOfType<GameManager>().EndGame();

        GetComponent<Animator>().enabled = false;


    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.transform.parent.CompareTag("Respawn"))
        {
            Dead();
        }
        
    }

    
}

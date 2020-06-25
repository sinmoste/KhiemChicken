using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class opposumwalker : MonoBehaviour
{
    public float speed = 1f;
    public Rigidbody2D myBody;
    public Transform startPos, endPos;
    private bool collision;
    public Player player;
    public HealthBar healthBar;
    public Slider slider;
    public Animator anim;
    public int maxHealth = 100;
    public bool faceright = true;// kiểm tra trái phải.

    private void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        anim = gameObject.GetComponent<Animator>();
        healthBar.SetMaxHealth(maxHealth);
    }
    private void Update()
    {
        Death();
    }
    void ChangeDirection()
    {
        collision = Physics2D.Linecast(startPos.position, endPos.position, 1 << LayerMask.NameToLayer("Ground"));
        Debug.DrawLine(startPos.position, endPos.position, Color.green);
        if (!collision)
        {
            Vector3 temp = transform.localScale;
            if (temp.x == 1f)
            {
                faceright = false;
                temp.x = -1f;
                healthBar.checkScale();
            }
            else
            {
                faceright = true;
                temp.x = 1f;
                healthBar.checkScale();
            }
            transform.localScale = temp;
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
        ChangeDirection();
    }
    private void Move()
    {
        myBody.velocity = new Vector2(transform.localScale.x, 0) * speed;

    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            player.Damage(10); //mất 1 máu
            player.KnockbackOpposum(200f, player.transform.position);
        }
    }
    void Damage(int damage)
    {

        maxHealth -= damage;
        healthBar.SetHealth(maxHealth);
    }
    void Death()
    {
        if (maxHealth <= 0)
        {
            anim.SetTrigger("Death");
        }
    }
    void Explosive()
    {
        Destroy(gameObject);
    }
    //test
    public void knockback()
    {
        Vector2 temp = gameObject.transform.position;
        if (player.faceright == true)
        {
            myBody.AddForce(new Vector2(temp.x + 500f, temp.y));
        }
        else
        {
            myBody.AddForce(new Vector2(temp.x -500f, temp.y));
        }
        
        
    }
}

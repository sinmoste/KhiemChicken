using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    public float speed = 50f, maxspeed = 3, jumpPow = 220f;
    public bool grounded = true, faceright = true, doublejump = false;

    //Máu nhân vật
    public int ourHealth;
    public int maxhealth = 3;

    public Rigidbody2D r2;
    public Animator anim;

    //Điểm
    public Gamemaster gm;
    public Cherry cherry;
    public Gem gem;


    // Use this for initialization
    void Start()
    {
        r2 = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();
        gm = GameObject.FindGameObjectWithTag("Gamemaster").GetComponent<Gamemaster>();
        ourHealth = maxhealth;
        cherry = gameObject.GetComponent<Cherry>();
        gem = gameObject.GetComponent<Gem>();
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetBool("Grounded", grounded);
        anim.SetFloat("Speed", Mathf.Abs(r2.velocity.x));

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (grounded)
            {
                grounded = false;
                doublejump = true;
                r2.AddForce(Vector2.up * jumpPow);
            }
            else
            {
                if (doublejump)
                {
                    doublejump = false;
                    r2.velocity = new Vector2(r2.velocity.x, 0);
                    r2.AddForce(Vector2.up * jumpPow * 1.5f);
                }
            }
        }

    }

    void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");
        r2.AddForce((Vector2.right) * speed * h);

        if (r2.velocity.x > maxspeed)
            r2.velocity = new Vector2(maxspeed, r2.velocity.y);
        if (r2.velocity.x < -maxspeed)
            r2.velocity = new Vector2(-maxspeed, r2.velocity.y);
        ////giới hạn nhảy
        //if (r2.velocity.y > maxjump)
        //    r2.velocity = new Vector2(r2.velocity.x, maxjump);
        //if (r2.velocity.y < -maxjump)
        //    r2.velocity = new Vector2(r2.velocity.x, -maxjump);

        if (h > 0 && !faceright)
        {
            Flip();
        }

        if (h < 0 && faceright)
        {
            Flip();
        }
        // giảm ma sát(giảm tốc độ)
        if (grounded)
        {
            r2.velocity = new Vector2(r2.velocity.x * 0.9f, r2.velocity.y);
        }
        //máu nhỏ 0 sẽ chết
        if (ourHealth <= 0)
        {
            Death();
        }
    }

    public void Flip()
    {
        faceright = !faceright;
        Vector3 Scale;
        Scale = transform.localScale;
        Scale.x *= -1;
        transform.localScale = Scale;
    }
    public void Death()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        if(PlayerPrefs.GetInt("highscore") < gm.points)
            PlayerPrefs.SetInt("highscore", gm.points);
    }

    //Start Bẫy chông
    public void Damage(int damage)
    {
        ourHealth -= damage;
        gameObject.GetComponent<Animation>().Play("redflash");// nháy đỏ khi ng chơi mất hp

    }

    public void Knockback(float Knockpow, Vector2 Knockdir)
    {
        r2.velocity = new Vector2(0, 0);
        r2.AddForce(new Vector2(Knockdir.x * -35, Knockdir.y * Knockpow));//(-40 độ giật lùi khi chạm bẫy)
    }
    //End Bẫy chông

    //Start Knockback Quái
    public void KnockbackOpposum(float Knockpowop, Vector2 Knockdirop)
    {
        r2.velocity = new Vector2(0, 0);
        r2.AddForce(new Vector2(Knockdirop.x * -100, Knockdirop.y * Knockpowop));//(-100 độ giật lùi khi chạm bẫy)
    }
    //End Knockback
    //Coins
    public void OnTriggerEnter2D(Collider2D col)
    {
        Cherry cherry = col.gameObject.GetComponent<Cherry>();
        Gem gem = col.gameObject.GetComponent<Gem>();

        if (col.CompareTag("Coins"))//Chạm trái cây có tag là coins
        {
            //Destroy(col.gameObject);
            gm.points += 100;// cộng điểm
            cherry.Boom();//thay đổi animation nổ
            
        }
        if (col.CompareTag("CoinsDiamond"))
        {
            //Destroy(col.gameObject);
            gm.points += 300;
            gem.Boom();
        }
    }

    //Endcoins

}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Player : MonoBehaviour {

    public int hp, damage, score;
    public float speed, maxSpeed;
    public GameObject barrel;
    public Text ptlbl;
    public Button restart, exit;
    public Rigidbody2D rgd2d;
    public GameObject blt;
    public GameObject explo;
    public float nextFire;
    public float fireRate = 0.1f;

    public bool isDead = false;
    public int fireType = 1;
    public ScreenShake cam;

    public float shakeAmount;

    private AudioSource sound;

    // Use this for initialization
    void Start() {
       
        rgd2d = this.GetComponent<Rigidbody2D>();
        sound = this.GetComponent<AudioSource>();
        cam = Camera.main.GetComponent<ScreenShake>();
        

    }

    // Update is called once per frame
    void Update() {
        
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            ptlbl.text = score.ToString();
        }


        if (hp <= 0 && !isDead) {
            isDead = true;
            hp = 0;
            this.GetComponent<SpriteRenderer>().enabled = false;
            this.GetComponent<BoxCollider2D>().enabled = false;
            restart.gameObject.SetActive(true);
            exit.gameObject.SetActive(true);
            Instantiate(explo, this.gameObject.transform);
            
            //Instantiate(explo, this.gameObject.transform);

        }

        //Store the current horizontal input in the float moveHorizontal.
        float moveHorizontal = Input.GetAxis("Horizontal");

        //Store the current vertical input in the float moveVertical.
        float moveVertical = Input.GetAxis("Vertical");

        //Use the two store floats to create a new Vector2 variable movement.
        Vector2 movement = new Vector2(moveHorizontal, moveVertical);

        //Call the AddForce function of our Rigidbody2D rb2d supplying movement multiplied by speed to move our player.

        if (!isDead) {
            rgd2d.velocity = movement * speed;

        }


        if (Input.GetButton("Fire1") && !isDead && Time.time > nextFire) {

            if (fireType == 1)  //automatic
            {
                fireRate = 0.1f;
                nextFire = Time.time + fireRate;
                GameObject bullet = Instantiate(blt, barrel.transform);
                sound.Play();
                cam.screenShake();
                Bullet b = bullet.GetComponent<Bullet>();
                b.damage = this.damage;
                b.origin = this.gameObject;
            }

            if (fireType == 2) //Rapid Fire
            {
                fireRate = 0.05f;
                nextFire = Time.time + fireRate;
                GameObject bullet = Instantiate(blt, barrel.transform);
                sound.Play();
                cam.screenShake();
                Bullet b = bullet.GetComponent<Bullet>();
                b.speed = 20;
                b.damage = this.damage;
                b.origin = this.gameObject;

            }
        }

        if (Input.GetKeyDown("escape")) {
            Application.Quit();
        }

    }

    void OnCollisionEnter2D(Collision2D col)
    {

        if (col.gameObject.tag == "Enemy")
        {

            takeDamage(10000);
        }

        if (col.gameObject.tag == "Pick Up")
        {
            Destroy(col.gameObject);
            StartCoroutine(Autofire());
        }

    }

    IEnumerator Autofire()
    {

        fireType = 2;
        yield return new WaitForSeconds(10);
        fireType = 1;
        yield return null;
    }

    public void takeDamage(int dmg) {
        hp -= dmg;
    }

    public void addPoints(int pts) {
        score += pts;
    }

}

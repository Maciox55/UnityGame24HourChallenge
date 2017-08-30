using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public int hp, damage, points;
    public bool isDead = false;
    public Player player;

    private float rand;
	// Use this for initialization
	void Start () {
        player = GameObject.Find("Player").GetComponent<Player>();
        rand = Random.Range(0f, 1f);
        Rigidbody2D rigid = this.GetComponent<Rigidbody2D>();
        rigid.velocity = new Vector2(Random.Range(-7f,-10f),0);
        
	}
	
	// Update is called once per frame
	void Update () {
       
        if (hp <= 0 && !isDead)
        {
            bulletDeath(); 
        }
        
        	
	}

    void OnCollisionEnter2D(Collision2D col)
    {

        if (col.gameObject.tag == "Enemy")
        {
            col.gameObject.SendMessage("collisionDeath");
            Instantiate(Resources.Load("Prefabs/Explosion") as GameObject, this.transform);
            this.collisionDeath();
        }

    }

    void bulletDeath() {
        isDead = true;
        this.GetComponent<Collider2D>().enabled = false;
        this.GetComponent<SpriteRenderer>().enabled = false;



        if (rand > 0.85f)
        {
            GameObject temp = Instantiate(Resources.Load("Prefabs/RapidPickUp"), transform.position, Quaternion.identity) as GameObject;
            temp.transform.SetParent(temp.transform);
        }
        player.addPoints(points);

        StartCoroutine(Dead());
    } 

    void TakeDamage(int dmg) {
        this.hp -= dmg;
    }

    void collisionDeath () {
        StartCoroutine(Dead());
    }

    IEnumerator Dead() {
        Instantiate(Resources.Load("Prefabs/Explosion") as GameObject, this.transform);
        ScreenShake shake = Camera.main.GetComponent<ScreenShake>();
        shake.screenShake();
        yield return new WaitForSeconds(0.25f);
        Destroy(this.gameObject);
        yield return null;
    }

}

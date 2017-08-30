using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public float speed;
    public int damage;
    public GameObject origin;

	// Use this for initialization
	void Start () {
        Physics2D.IgnoreCollision(this.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        StartCoroutine(SelfDestruct());
        this.GetComponent<Rigidbody2D>().velocity = new  Vector2(speed,0);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter2D(Collision2D col) {

        if (col.gameObject.tag == "Enemy") {

           ///col.gameObject.GetComponent<Enemy>();
            col.gameObject.SendMessage("TakeDamage", this.damage);
            this.GetComponent<Collider2D>().enabled = false;
            Destroy(this.gameObject);
        }

        if (col.gameObject.tag == "Bullet" || col.gameObject.tag == "Player" || col.gameObject.tag == "Pick Up") {
            Physics2D.IgnoreCollision(col.collider, col.otherCollider);
        }

       


    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Limiter")
        {
            Destroy(this.gameObject);
        }
    }

    IEnumerator SelfDestruct() {

        yield return new WaitForSeconds(10);
        Destroy(this.gameObject);
        yield return null;
    }

}

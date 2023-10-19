using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    float xspeed = 50f;
    private float yspeed = 0f;

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector2(transform.position.x + xspeed * Time.deltaTime, transform.position.y + yspeed * Time.deltaTime);
        if(transform.position.x < -19f || transform.position.x > 19f || transform.position.y < -4.5f || transform.position.y > 25.5f)
        {
            Destroy(this.gameObject);
        }
    }
    public void yspeed_set(float ys)
    {
        yspeed = ys;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            var enemy = collision.gameObject.GetComponent<Enemy>();
            enemy.Damaged(3);
        }
    }
}

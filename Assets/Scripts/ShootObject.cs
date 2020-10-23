using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootObject : MonoBehaviour
{
    void Start()
    {
        Destroy(this.gameObject, 5);
        transform.position = new Vector3(transform.position.x + 0.5f, transform.position.y, transform.position.z + 1.5f);
    }

    void Update()
    {
        transform.position = transform.position + new Vector3(0.15f, 0, 0);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Egg>() != null)
        {
            Destroy(this.gameObject);
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(55, 1), ForceMode2D.Impulse);
        }
        else if(collision.gameObject.tag == "Terrain" || collision.gameObject.GetComponent<Creature>() != null)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Egg>() != null)
        {
            Destroy(this.gameObject);
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(85, 1), ForceMode2D.Impulse);
        }
        else if (collision.gameObject.tag == "Terrain" || collision.gameObject.GetComponent<DinoCreature>() != null || collision.gameObject.GetComponent<ShroomCreature>() != null)
        {
            Destroy(this.gameObject);
        }
    }
}

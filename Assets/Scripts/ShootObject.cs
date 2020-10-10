using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class ShootObject : MonoBehaviour
{
    void Start()
    {
        Destroy(this.gameObject, 5);
    }

    void Update()
    {
        transform.position = transform.position + new Vector3(-0.05f, 0, 0);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Egg>() != null)
        {
            Destroy(this.gameObject);
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(-55, 1), ForceMode2D.Impulse);
        }
        else if(collision.gameObject.tag == "Terrain")
        {
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Egg>() != null)
        {
            Destroy(this.gameObject);
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(-55, 1), ForceMode2D.Impulse);
        }
        else if (collision.gameObject.tag == "Terrain")
        {
            Destroy(this.gameObject);
        }
    }
}

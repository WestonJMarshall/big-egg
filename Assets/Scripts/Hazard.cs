using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hazard : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Egg>() != null)
        {
            GameManager.Instance.ResetLevel();
        }

        if (collision.gameObject.GetComponent<Creature>() != null)
        {
            collision.gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
            collision.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePosition;
            collision.gameObject.transform.SetParent(GameObject.FindGameObjectWithTag("CreatureSelectUI").transform);
            collision.gameObject.GetComponent<Collider2D>().isTrigger = false;
        }
    }
}

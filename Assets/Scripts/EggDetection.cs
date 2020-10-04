using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggDetection : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        GetComponentInParent<Creature>().EggCollision(collision);
    }
}

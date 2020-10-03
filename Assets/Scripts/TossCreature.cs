using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TossCreature : Creature
{
    [SerializeField]
    protected float throwStrength = 1.0f;

    public override void CreatureAction(GameObject recipient)
    {
        base.CreatureAction(recipient);

        if (recipient == null || recipient.GetComponent<Rigidbody2D>() == null)
            return;

        recipient.GetComponent<Rigidbody2D>().AddForce(new Vector2(1.5f * throwStrength, 20 * throwStrength),ForceMode2D.Impulse);
    }
}
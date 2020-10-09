using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TossCreature : Creature
{
    [SerializeField]
    protected float throwStrength = 1.0f;
    [SerializeField]
    protected AudioClip actionSound;

    public override void CreatureAction(GameObject recipient)
    {
        GameManager.Instance.audioManager.GetComponent<AudioSource>().PlayOneShot(actionSound, 0.5f);
        base.CreatureAction(recipient);

        if (recipient == null || recipient.GetComponent<Rigidbody2D>() == null)
            return;

        recipient.GetComponent<Rigidbody2D>().AddForce(new Vector2(1.5f * throwStrength, 20 * throwStrength),ForceMode2D.Impulse);
    }
}
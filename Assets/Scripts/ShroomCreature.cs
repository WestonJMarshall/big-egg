using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShroomCreature : Creature
{
    [SerializeField]
    protected float throwStrength = 1.0f;
    [SerializeField]
    protected AudioClip actionSound;

    public override void EggCollision(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Egg>() != null)
        {
            if (canDoAction)
            {
                canDoAction = false;
                animator.speed = 1;
                animator.Play(actionAnimation);
                StartCoroutine(nameof(DelayAction), collision);
            }
        }
    }

    public override void CreatureAction(GameObject recipient)
    {
        animator.Play(actionAnimation, -1, 0f);
        animator.speed = 0;
        GameManager.Instance.audioManager.GetComponent<AudioSource>().PlayOneShot(actionSound, 0.5f);
        base.CreatureAction(recipient);

        if (recipient == null || recipient.GetComponent<Rigidbody2D>() == null)
            return;

        recipient.GetComponent<Rigidbody2D>().AddForce(new Vector2(1.5f * throwStrength, 20 * throwStrength), ForceMode2D.Impulse);
    }
}

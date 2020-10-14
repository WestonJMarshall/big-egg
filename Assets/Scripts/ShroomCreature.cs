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
                animator.Play(actionAnimation, -1, 0f);
                animator.Play(actionAnimation);
                StartCoroutine(nameof(StopAnimation), animator.GetCurrentAnimatorStateInfo(0).length);
                StartCoroutine(nameof(DelayAction), collision);
            }
        }
    }

    public override void CreatureAction(GameObject recipient)
    {
        GameManager.Instance.audioManager.GetComponent<AudioSource>().PlayOneShot(actionSound, 0.5f);
        base.CreatureAction(recipient);

        if (recipient == null || recipient.GetComponent<Rigidbody2D>() == null)
            return;

        recipient.GetComponent<Rigidbody2D>().velocity = Vector2.Reflect(recipient.GetComponent<Rigidbody2D>().velocity,Vector2.up) * throwStrength;
        recipient.GetComponent<Rigidbody2D>().velocity = new Vector2(recipient.GetComponent<Rigidbody2D>().velocity.x, Mathf.Clamp(recipient.GetComponent<Rigidbody2D>().velocity.y * 2.0f, 8, 25));
    }
}

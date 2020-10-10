using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogCreature : Creature
{
    [SerializeField]
    protected AudioClip actionSound;
    [SerializeField]
    protected GameObject shootPrefab;

    private float shootTimer = 0.0f;

    public override void EggCollision(Collider2D collision)
    {

    }

    protected override void Update()
    {
        if (GetComponent<Rigidbody2D>().bodyType == RigidbodyType2D.Dynamic)
        {
            shootTimer += Time.deltaTime;

            if (shootTimer > actionDelay)
            {
                shootTimer = 0.0f;
                CreatureAction(shootPrefab);
            }
        }
    }

    public override void CreatureAction(GameObject recipient)
    {
        animator.speed = 1;
        animator.Play(actionAnimation);
        StartCoroutine(nameof(ResetAnimation));
        GameManager.Instance.audioManager.GetComponent<AudioSource>().PlayOneShot(actionSound, 0.5f);

        GameObject sp = Instantiate(shootPrefab);
        sp.transform.position = transform.position + new Vector3(-0.5f, 0, 0);
    }

    public IEnumerator ResetAnimation()
    {
        yield return new WaitForSeconds(0.8f);
        animator.Play(actionAnimation, -1, 0f);
        animator.speed = 0;
    }
}

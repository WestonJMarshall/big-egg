using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creature : MonoBehaviour
{
    protected bool canDoAction = true;

    [SerializeField]
    protected float actionDelay = 1.0f;

    [SerializeField]
    protected float actionReset = 3.0f;

    [SerializeField]
    protected string actionAnimation = "creature1TurnRight";

    private GameObject egg;

    public Animator animator;

    protected virtual void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        animator.speed = 0;

        transform.position = new Vector3(transform.position.x, transform.position.y, -42);
    }

    protected virtual void Update()
    {
        if(egg != null)
        {
            if (egg.GetComponent<Collider2D>().IsTouching(GetComponentInChildren<Collider2D>()))
            {
                if (canDoAction)
                {
                    canDoAction = false;
                    StartCoroutine(nameof(DelayAction), egg.GetComponent<Collider2D>());
                }
            }
        }
        else
        {
            egg = FindObjectOfType<Egg>().gameObject;
        }
    }

    public virtual void EggCollision(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Egg>() != null)
        {
            if (canDoAction)
            {
                canDoAction = false;
                StartCoroutine(nameof(DelayAction), collision);
            }
        }
    }

    private void OnMouseDown()
    {
        if (Time.timeScale < 0.5f)
        {
            GameManager.Instance.CreatureSelected(gameObject);
        }
    }

    public virtual void CreatureAction(GameObject recipient) 
    {
        StartCoroutine(nameof(ResetAction));
    }

    public IEnumerator DelayAction(Collider2D collision)
    {
        yield return new WaitForSeconds(actionDelay);

        if (GetComponentsInChildren<Collider2D>().Length == 2)
        {
            if (egg.GetComponent<Collider2D>().IsTouching(GetComponentsInChildren<Collider2D>()[1]))
            {
                CreatureAction(collision.gameObject);
            }
        }
        else
        {
            CreatureAction(collision.gameObject);
        }
    }

    public IEnumerator ResetAction()
    {
        yield return new WaitForSeconds(actionReset);
        canDoAction = true;
    }

    public void ResetCanDoAction() { canDoAction = true; }
}

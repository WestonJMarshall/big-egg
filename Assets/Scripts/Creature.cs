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

    private void OnTriggerStay2D(Collider2D collision)
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

    public virtual void CreatureAction(GameObject recipient) 
    {
        StartCoroutine(nameof(ResetAction));
    }

    public IEnumerator DelayAction(Collider2D collision)
    {
        yield return new WaitForSeconds(actionDelay);
        CreatureAction(collision.gameObject);
    }

    public IEnumerator ResetAction()
    {
        yield return new WaitForSeconds(actionReset);
        canDoAction = true;
    }

    public void ResetCanDoAction() { canDoAction = true; }
}

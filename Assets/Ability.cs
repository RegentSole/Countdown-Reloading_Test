using UnityEngine;
using System.Collections;


public abstract class Ability : MonoBehaviour
{
    public string name;
    public float cooldownTime;
    protected bool isOnCooldown;
    protected float currentCooldown;

    public virtual void Initialize() {}

    public virtual void UseAbility(GameObject target)
    {
        if (!isOnCooldown)
        {
            StartCoroutine(Cooldown());
            OnUse(target);
        }
    }

    protected IEnumerator Cooldown()
    {
        isOnCooldown = true;
        yield return new WaitForSeconds(cooldownTime);
        isOnCooldown = false;
    }

    protected abstract void OnUse(GameObject target);
}
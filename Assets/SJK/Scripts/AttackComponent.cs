using TMPro;
using UnityEngine;
using Utils.Enums;
using static UnityEngine.UI.GridLayoutGroup;

public class AttackComponent : MonoBehaviour
{
    private Character owner;

    public bool isAttacking = false;

    LineupSlot targetSlot;
    Vector3 originPosition;
    Vector3 targetPosition;

    [SerializeField]
    GameObject bulletPrefab;

    GameObject bullet;
    float bulletSpeed = 0.8f;

    [SerializeField] 
    private bool impactApplied = false;

    

    void Start()
    {
        owner = GetComponent<Character>();
        originPosition = owner.gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if(isAttacking)
        {
            if (owner.characterData.rangeType == RangeType.Melee)
            {
                transform.position = Vector3.MoveTowards(gameObject.transform.position, targetPosition, 0.5f);
                if(transform.position == targetSlot.AttackedPosition.position)
                {
                    if (!impactApplied)
                    {
                        ApplyDamageOnce();
                        impactApplied = true;
                    }
                    targetPosition = originPosition;
                }
                else if(transform.position == originPosition)
                {
                    isAttacking = false;
                }
            }
            else
            {
                if (bullet == null) return;

                Vector3 dir = targetPosition - bullet.transform.position;
                float distanceToTarget = dir.magnitude;
                float moveDistance = bulletSpeed;

                if (distanceToTarget <= moveDistance)
                {
                    bullet.transform.position = targetPosition;

                    if (!impactApplied)
                    {
                        ApplyDamageOnce();
                        impactApplied = true;
                    }

                    isAttacking = false;

                    Destroy(bullet);
                    bullet = null;
                    return;
                }
                bullet.transform.position += dir.normalized * moveDistance;
            }
        }
    }

    public void Attack(LineupSlot target)
    {
        if (isAttacking) return;

        targetSlot = target;
        targetPosition = targetSlot.AttackedPosition.position;
        if (owner.characterData.rangeType == RangeType.Range)
        {
            bullet = Instantiate(bulletPrefab, originPosition, Quaternion.identity);
        }

        isAttacking = true;
    }

    private void ApplyDamageOnce()
    {
        if (targetSlot == null)
            return;

        var targetChar = targetSlot.character;

        if (targetSlot == null || targetSlot.character == null || targetSlot.character.HealthComp == null)
            return;

        float damage = owner.characterData.attack;
        targetChar.HealthComp.TakeDamage(damage);
        owner.StatusComp.UpdateDamageDealt(damage);
    }

}

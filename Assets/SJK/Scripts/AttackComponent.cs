using TMPro;
using UnityEngine;
using Utils.Enums;
using static UnityEngine.UI.GridLayoutGroup;

public class AttackComponent : MonoBehaviour
{
    private Character owner;

    public int targetIndex = 0;
    public bool isAttacking = false;

    LineupSlot targetSlot;
    Vector3 originPosition;
    Vector3 targetPosition;

    [SerializeField]
    GameObject bulletPrefab;

    GameObject bullet;
    float bulletSpeed = 0.8f;

    private BattleSystem battleSystem;

    [SerializeField] 
    private float impactRadius = 0.15f;
    private bool impactApplied = false;

    BattleSystem battleSystem;

    void Start()
    {
        owner = GetComponent<Character>();
        battleSystem = FindAnyObjectByType<BattleSystem>();
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
                Vector3 dir = targetPosition - bullet.transform.position;
                bullet.transform.Translate(dir.normalized * 1f);
                if (dir.magnitude <= bulletSpeed)
                {
                    bullet.transform.position = targetPosition; 
                }
                else
                {
                    bullet.transform.position += dir.normalized * bulletSpeed;
                }
                if (dir.magnitude <= impactRadius)
                {
                    if (!impactApplied)
                    {
                        ApplyDamageOnce();
                        impactApplied = true;
                    }

                    isAttacking = false;

                    if (bullet != null)
                    {
                        Destroy(bullet);
                        bullet = null;
                    }
                }
            }
        }
    }

    public void Attack()
    {
        if (isAttacking) return;

        if (owner.isEnemy)
        {
            targetSlot = battleSystem.friendlySlots[targetIndex].GetComponent<LineupSlot>();
        }
        else
        {
            targetSlot = battleSystem.enemySlots[targetIndex].GetComponent<LineupSlot>();
        }
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
    }

}

using TMPro;
using UnityEngine;
using Utils.Enums;
using static UnityEngine.UI.GridLayoutGroup;

public class AttackComponent : MonoBehaviour
{
    private Character owner;
    private BattleSystem battleSystem;

    public int targetIndex = 0;
    public bool isAttacking = false;

    LineupSlot targetSlot;
    Vector3 originPosition;
    Vector3 targetPosition;

    [SerializeField]
    GameObject bulletPrefab;

    GameObject bullet;
    float remainTime = 1.0f;
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
                    targetPosition = originPosition;
                }
                else if(transform.position == originPosition)
                {
                    isAttacking = false;
                }
            }
            else
            {
                Vector3 dir = targetPosition - originPosition;
                bullet.transform.Translate(dir.normalized * 1f);
                remainTime -= Time.deltaTime;
                if(remainTime <= 0f)
                {
                    isAttacking = false;
                    remainTime = 1.0f;
                    Destroy(bullet);
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
        if(owner.characterData.rangeType == RangeType.Range)
        {
            bullet = Instantiate(bulletPrefab, originPosition, Quaternion.identity);
        }

        isAttacking = true;
    }
}

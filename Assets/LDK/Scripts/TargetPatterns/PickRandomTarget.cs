using UnityEngine;

public class PickRandomTarget : IAttackTargetSelector
{
    private readonly BattleSystem battleSystem;
    public PickRandomTarget(BattleSystem system) => battleSystem = system;
    public virtual LineupSlot SelectTarget(Character Attacker)
    {
        if (battleSystem == null)
            return null;

        bool isEnemy = Attacker.isEnemy;
        if (isEnemy)
        {
            int Target = UnityEngine.Random.Range(0, battleSystem.friendlySlots.Length);
            LineupSlot targetslot = battleSystem.friendlySlots[Target].GetComponent<LineupSlot>();

            return targetslot;
        }
        else
        {
            int Target = UnityEngine.Random.Range(0, battleSystem.enemySlots.Length);
            LineupSlot targetslot = battleSystem.enemySlots[Target].GetComponent<LineupSlot>();

            return targetslot;
        }
    }
}

using UnityEngine;

public class PickRandomTarget : MonoBehaviour, IAttackTargetSelector
{
    BattleSystem battleSystem;
    public void Start()
    {
        battleSystem = GetComponent<BattleSystem>();
    }
    public virtual LineupSlot SelectTarget(bool isEnemy)
    {
        if (isEnemy)
        {
            int alive = 0;
            LineupSlot[] line = new LineupSlot[5];
            for (int i = 0; i < (int)ELinePosition.MAX; i++)
            {
                line[i] = battleSystem.friendlySlots[i].GetComponent<LineupSlot>();
                if (line[i].character.isAlive)
                    alive++;
            }

            if (alive == 0)
                return null;

            int k = UnityEngine.Random.Range(0, alive);
        }
        else
        {

        }
        return null;
    }
}

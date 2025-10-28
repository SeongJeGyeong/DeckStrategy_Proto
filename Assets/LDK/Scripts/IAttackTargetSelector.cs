using UnityEngine;

public interface IAttackTargetSelector
{
    public LineupSlot SelectTarget(bool isEnemy);
}

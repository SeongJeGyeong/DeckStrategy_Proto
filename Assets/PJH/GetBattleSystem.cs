using UnityEngine;

public class GetBattleSystem : MonoBehaviour
{
    [SerializeField] private BattleSystem battleSystem;
    [SerializeField] private TurnSystem turnSystem;

    void Start()
    {

        // �� TurnSystem �ʱ�ȭ
        turnSystem.InitTurnSystem();
    }
}

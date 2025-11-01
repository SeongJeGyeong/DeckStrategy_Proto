using UnityEngine;
using UnityEngine.Events;
using UnityEngine.TextCore.Text;

public class LineupSlot : MonoBehaviour
{
    [SerializeField]
    Character CharacterModelPrefab; // LDK : Character�� ����

    Transform slotTransform;
    public bool isPlaced = false;
    public Character character { get; private set; }

    public Transform AttackedPosition;

    public event System.Action OnCPUpdated;

    void Awake()
    {
        // ��� ��ư�� start�� LineupSlot�� Start���� ���� ����Ǿ� ���� �������� ���� ���� �ֱ⿡ Awake���� ȣ��
        slotTransform = this.transform;
        character = Instantiate(CharacterModelPrefab, slotTransform);
        character.gameObject.SetActive(false);
    }

    private void Start()
    {
        character.BattleComp.OnDie += DeselectCharacter;
    }

    public void SetSelectedCharacter(OwnedCharacterInfo info, bool isEnemy)
    {
        isPlaced = true;
        character.isEnemy = isEnemy;
        character.SetCharacterData(info);
        OnCPUpdated?.Invoke();
    }

    public void DeselectCharacter()
    {
        isPlaced = false;

        character.ClearCharacterInfo();
        OnCPUpdated?.Invoke();
    }

    public void ActivateBattleUI()
    {
        character.ActiveBattleUI();
    }
}

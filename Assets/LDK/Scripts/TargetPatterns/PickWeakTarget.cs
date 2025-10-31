using UnityEngine;
using Utils.Enums;

public class PickWeakTarget : IAttackTargetSelector
{
    private readonly BattleSystem battleSystem;
    public PickWeakTarget(BattleSystem system) => battleSystem = system;

    public virtual LineupSlot SelectTarget(Character Attacker)
    {
        if (battleSystem == null)
            return null;

        bool isEnemy = Attacker.isEnemy;

        if (isEnemy)
        {
            Utils.Enums.EAttributeType type = Attacker.characterData.type; //�����ڰ� ���̶�� 
            int length = battleSystem.friendlySlots.Length;

            LineupSlot[] slots = new LineupSlot[length];

            for (int i = 0; i < length; i++)
            {
                slots[i] = battleSystem.friendlySlots[i].GetComponent<LineupSlot>();
            }

            switch (type)
            {
                case EAttributeType.ROCK: //�������� type�� ������ ������ �����ִ��� 5��(Front)���� Ȯ����
                    {
                        for (int i = slots.Length - 1; i >= 0; i--)
                        {
                            if (slots[i].character.characterData.type== EAttributeType.SCISSORS)
                            {
                                return slots[i];
                            }
                        }
                    }
                    break;//goto case EAttributeType.PAPER; //������ ������밡 ������ �ٸ� ��� ����
                case EAttributeType.PAPER:
                    {
                        for (int i = slots.Length - 1; i >= 0; i--)
                        {
                            if (slots[i].character.characterData.type == EAttributeType.ROCK)
                            {
                                return slots[i];
                            }
                        }
                    }
                    break;//goto case EAttributeType.SCISSORS;
                case EAttributeType.SCISSORS:
                    {
                        for (int i = slots.Length - 1; i >= 0; i--)
                        {
                            if (slots[i].character.characterData.type == EAttributeType.PAPER)
                            {
                                return slots[i];
                            }
                        }
                    }
                    break; //goto case EAttributeType.ROCK;
            }
        }
        else
        {
            Utils.Enums.EAttributeType type = Attacker.characterData.type; //Friendly�� ����
            int length = battleSystem.enemySlots.Length;

            LineupSlot[] slots = new LineupSlot[length];

            for (int i = 0; i < length; i++)
            {
                slots[i] = battleSystem.enemySlots[i].GetComponent<LineupSlot>();
            }

            switch (type)
            {
                case EAttributeType.ROCK:
                    {
                        for (int i = slots.Length - 1; i >= 0; i--)
                        {
                            if (slots[i].character.characterData.type == EAttributeType.SCISSORS)
                            {
                                return slots[i];
                            }
                        }
                    }
                    break;// goto case EAttributeType.PAPER;
                case EAttributeType.PAPER:
                    {
                        for (int i = slots.Length - 1; i >= 0; i--)
                        {
                            if (slots[i].character.characterData.type == EAttributeType.ROCK)
                            {
                                return slots[i];
                            }
                        }
                    }
                    break; //goto case EAttributeType.SCISSORS;
                case EAttributeType.SCISSORS:
                    {
                        for (int i = slots.Length - 1; i >= 0; i--)
                        {
                            if (slots[i].character.characterData.type == EAttributeType.PAPER)
                            {
                                return slots[i];
                            }
                        }
                    }
                    break; //goto case EAttributeType.ROCK;
            }
        }

        return null;
    }
}

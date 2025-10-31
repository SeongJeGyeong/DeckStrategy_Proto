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
            Utils.Enums.EAttributeType type = Attacker.characterData.type; //공격자가 적이라면 
            int length = battleSystem.friendlySlots.Length;

            LineupSlot[] slots = new LineupSlot[length];

            for (int i = 0; i < length; i++)
            {
                slots[i] = battleSystem.friendlySlots[i].GetComponent<LineupSlot>();
            }

            switch (type)
            {
                case EAttributeType.ROCK: //공격작의 type을 가져와 약점인 적이있는지 5번(Front)부터 확인함
                    {
                        for (int i = slots.Length - 1; i >= 0; i--)
                        {
                            if (slots[i].character.characterData.type== EAttributeType.SCISSORS)
                            {
                                return slots[i];
                            }
                        }
                    }
                    break;//goto case EAttributeType.PAPER; //원래는 약점상대가 없으면 다른 상대 공격
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
            Utils.Enums.EAttributeType type = Attacker.characterData.type; //Friendly도 동일
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

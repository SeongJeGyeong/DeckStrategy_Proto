using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;
using static Character;

public interface IStatusEffect
{
    void Apply(Character target);
    void Turn(Character target);
    void Remove(Character target);
}

public class BurnEffect : IStatusEffect
{
    public void Apply(Character C) => Debug.Log("ȭ�� ����");
    public void Turn(Character C) => C.HealthComp.TakeDamage(1);
    public void Remove(Character C) => Debug.Log("ȭ�� ��");
}

public class PoisonEffect : IStatusEffect
{
    public void Apply(Character C) => Debug.Log("�� ����");
    public void Turn(Character C) => C.HealthComp.TakeDamage(1);
    public void Remove(Character C) => Debug.Log("�� ��");
}
public class StatusEffectComponent : MonoBehaviour
{
    Character owner;

    public event Action<StatusEffect> OnEffectAdded;
    public event Action<StatusEffect> OnEffectRemoved;

    private readonly Dictionary<string,StatusEffect> _effects = new();

    private void Start()
    {
        owner = GetComponent<Character>();
    }
    public void AddEffect(StatusEffect effect)
    {
        if (!owner.HealthComp.isAlive)
            return;

        if (_effects.TryGetValue(effect.Name, out StatusEffect getEffect))
        {
            getEffect.Stack += effect.Stack;  // ���� �� ����
            _effects[effect.Name] = getEffect;  // �ٽ� ����
        }
        else 
        {
            _effects.Add(effect.Name,effect);
        }

        effect.statusEffect.Apply(owner);
        OnEffectAdded?.Invoke(_effects[effect.Name]);
    }
    public void TurnAll()
    {
        foreach (StatusEffect effect in _effects.Values) effect.statusEffect.Turn(owner);
    }
    public void RemoveEffect(StatusEffect effect)
    {
        _effects.Remove(effect.Name);
        effect.statusEffect.Remove(owner);
        OnEffectRemoved?.Invoke(effect);
    }

    void OnDisable()
    {
        OnEffectAdded = null; // ��� ������ ����
        OnEffectRemoved = null;
    }
}

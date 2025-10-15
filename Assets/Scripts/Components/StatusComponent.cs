using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;
using static Character;

public interface IStatusEffect
{
    void Apply(Character target);
    void Tick(Character target);
    void Remove(Character target);
}

public class BurnEffect : IStatusEffect
{
    public void Apply(Character C) => Debug.Log("화상 시작");
    public void Tick(Character C) => C.healthComp.TakeDamage(5);
    public void Remove(Character C) => Debug.Log("화상 끝");
}
public class StatusComponent : MonoBehaviour
{
    Character owner;

    public event Action<StatusEffect> OnEffectAdded;
    public event Action<StatusEffect> OnEffectRemoved;

    private readonly List<StatusEffect> _effects = new();

    private void Start()
    {
        owner = GetComponent<Character>();
    }
    public void AddEffect(StatusEffect effect)
    {
        _effects.Add(effect);
        effect.statusEffect.Apply(owner);
        OnEffectAdded?.Invoke(effect);
    }
    public void TickAll()
    {
        foreach (StatusEffect effect in _effects) effect.statusEffect.Tick(owner);
    }
    public void RemoveEffect(StatusEffect effect)
    {
        _effects.Remove(effect);
        effect.statusEffect.Remove(owner);
        OnEffectRemoved?.Invoke(effect);
    }
}

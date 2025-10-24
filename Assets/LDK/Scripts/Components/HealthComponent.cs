using System;
using UnityEngine;
using UnityEngine.TextCore.Text;
using static Character;

public class HealthComponent : MonoBehaviour, IBattleable
{
    private Character owner;
    private float currHp;

    public event Action<float> OnDamaged;
    private void Start()
    {
        owner = GetComponent<Character>();
        currHp = owner.characterBase.characterData.maxHp;
    }
    public virtual void TakeDamage(float amount)
    {
        currHp -= amount;
        print($"{name} 데미지 받음");
        OnDamaged?.Invoke(currHp);
    }
    public virtual void Die()
    {

    }

    public virtual void AddEffect(StatusEffect effect) // IBattleable 인터페이스에 AddEffect를 추가해서 공격자가 상태도 부여할 수 있게 함
    {
        owner.StatusComp.AddEffect(effect);
    }
}
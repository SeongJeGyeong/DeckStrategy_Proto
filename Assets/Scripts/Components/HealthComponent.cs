using System;
using UnityEngine;
using UnityEngine.TextCore.Text;

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
}
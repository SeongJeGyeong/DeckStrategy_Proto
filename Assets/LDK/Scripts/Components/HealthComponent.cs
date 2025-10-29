using System;
using UnityEngine;
using UnityEngine.TextCore.Text;
using static Character;

public class HealthComponent : MonoBehaviour, IBattleable
{
    private Character owner;
    private float currHp;
    private Vector3 originalPosition;
    private Vector3 knockbackTarget;
    private float knockbackDistance = 0.4f;
    private float knockbackDuration = 0.2f;
    private float knockbackTimer = 0f;
    private bool isKnockback = false;

    public event Action<float> OnDamaged;
    private void Start()
    {
        owner = GetComponent<Character>();
        currHp = owner.characterData.maxHp;
    }

    private void Update()
    {
        if (isKnockback)
        {
            knockbackTimer += Time.deltaTime;
            float t = knockbackTimer / knockbackDuration;

            transform.position = Vector3.Lerp(knockbackTarget, originalPosition, t);

            if (knockbackTimer >= knockbackDuration)
            {
                isKnockback = false;
                transform.position = originalPosition;
            }
        }
    }
    public virtual void TakeDamage(float amount)
    {
        currHp -= amount;
        print($"{name} 데미지 받음");
        OnDamaged?.Invoke(currHp);
        owner.StatusComp.UpdateDamageTaken(amount);
        TriggerKnockback();
    }
    public virtual void Die()
    {

    }

    public virtual void AddEffect(StatusEffect effect) // IBattleable 인터페이스에 AddEffect를 추가해서 공격자가 상태도 부여할 수 있게 함
    {
        owner.StatusEffectComp.AddEffect(effect);
    }

    private void TriggerKnockback()
    {
        if (isKnockback) return;

        originalPosition = transform.position;

        float dirX = owner.isEnemy ? 1f : -1f;
        knockbackTarget = originalPosition + new Vector3(dirX * knockbackDistance, 0f, 0f);

        knockbackTimer = 0f;
        isKnockback = true;
    }
}
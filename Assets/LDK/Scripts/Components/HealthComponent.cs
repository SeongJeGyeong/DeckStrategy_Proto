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
        print($"{name} ������ ����");
        OnDamaged?.Invoke(currHp);
        owner.StatusComp.UpdateDamageTaken(amount);
        TriggerKnockback();
    }
    public virtual void Die()
    {

    }

    public virtual void AddEffect(StatusEffect effect) // IBattleable �������̽��� AddEffect�� �߰��ؼ� �����ڰ� ���µ� �ο��� �� �ְ� ��
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
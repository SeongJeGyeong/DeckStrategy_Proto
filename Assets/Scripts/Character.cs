using System;
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour, IDamageable
{
    private float MaxHp;
    private float CurrHp;
    private Int32 Level;
    private string name;
    private AttributeType type;
    public Slider HealthSlider;

    public virtual void OnDamage(float amount)
    {
        CurrHp -= amount;
        HealthSlider.value = CurrHp;
        print($"{name} 데미지 받음");
    }
    public void Awake()
    {
        MaxHp = 100;
        CurrHp = 100;
        Level = 100;
        name = "Mario";
        type = AttributeType.ROCK;
    }

    private void OnEnable()
    {
        HealthSlider.gameObject.SetActive(true);
        HealthSlider.maxValue = MaxHp;
        HealthSlider.value = CurrHp;
    }

    public void Update()
    {
    }
}

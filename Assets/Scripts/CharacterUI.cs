using UnityEngine;
using UnityEngine.UI;

public class CharacterUI : MonoBehaviour
{
    [SerializeField] private Slider HealthSlider;
    [SerializeField] private Slider GaugeSlider;
    private Character Character;

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void Start()
    {
        Character = GetComponent<Character>();
        HealthSlider.maxValue = Character.characterBase.characterData.maxHp;
        HealthSlider.value = Character.characterBase.characterData.maxHp;

        //GaugeSlider.maxValue;
        //GaugeSlider.value;

        Character.OnDamaged += HealthUpdate;
    }

    private void HealthUpdate(float CurrHp)
    {
        HealthSlider.value = CurrHp;
    }
}

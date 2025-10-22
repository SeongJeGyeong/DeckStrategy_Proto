using UnityEngine;

public class Result_Combat : MonoBehaviour
{
    [Header("MVP ���Ե�")]
    [SerializeField] private CharacterSlotUI[] mvpSlots;

    [Header("��� ������ (���� ������� �޾ƿ� ĳ���͵�)")]
    [SerializeField] private CharacterBase[] mvpCharacters; 

    void Start()
    {
        ApplyResults();
    }

    public void ApplyResults()
    {
        int count = Mathf.Min(mvpSlots.Length, mvpCharacters.Length);

        for (int i = 0; i < count; i++)
        {
            if (mvpSlots[i] != null)
                mvpSlots[i].SetData(mvpCharacters[i]);
        }
    }
}
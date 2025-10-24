using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MVPDisplay : MonoBehaviour
{
    [Header("씬의 MVP1 오브젝트")]
    [SerializeField] private GameObject mvp1Object; // MVP1 오브젝트
    //전투에 참여했던 데이터 조회 및 출력
    void Start()
    {
        StartCoroutine(UpdateFromMVP1NextFrame());
    }

    private System.Collections.IEnumerator UpdateFromMVP1NextFrame()
    {
        yield return null; // 한 프레임 대기 → MVP1 SetData가 끝난 뒤

        if (mvp1Object == null)
        {
            Debug.LogWarning("MVP1 오브젝트가 지정되지 않았습니다!");
            yield break;
        }

        // MVP1의 Image 가져오기
        Image mvpImage = mvp1Object.GetComponentInChildren<Image>();
        if (mvpImage != null)
        {
            var displayImage = GetComponentInChildren<Image>();
            if (displayImage != null)
                displayImage.material = mvpImage.material;
        }

        // MVP1의 이름 Text 가져오기
        TMP_Text mvpName = mvp1Object.GetComponentInChildren<TMP_Text>();
        if (mvpName != null)
        {
            var displayName = GetComponentInChildren<TMP_Text>();
            if (displayName != null)
                displayName.text = mvpName.text;
        }
    }
}
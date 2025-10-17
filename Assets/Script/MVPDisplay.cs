using UnityEngine;
using UnityEngine.UI;

public class MVPDisplay : MonoBehaviour
{
    [SerializeField] private Image sourceImage; // UI Image1
    private Renderer capsuleRenderer;

    void Start()
    {
        capsuleRenderer = GetComponent<Renderer>();

        if (sourceImage != null)
        {
            // UI Image에서 사용하던 Material 가져오기
            Material sourceMat = sourceImage.material;

            if (sourceMat != null)
            {
                // 그대로 캡슐에 적용
                capsuleRenderer.material = sourceMat;
            }
            else
            {
                Debug.LogWarning("Source Image Material이 비어있습니다!");
            }
        }
        else
        {
            Debug.LogWarning("Source Image가 지정되지 않았습니다!");
        }
    }
}
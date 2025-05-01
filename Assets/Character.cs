using System.Collections;
using UnityEngine;

public class Character : MonoBehaviour
{
    public RectTransform character; // 이건 본인 또는 외부에서 연결
    public float moveSpeed = 300f; // px/sec
    public float waitTimeMin = 1f;
    public float waitTimeMax = 3f;

    private RectTransform canvasRect;

    void Start()
    {
        // 본인 캐릭터 RectTransform을 자동으로 할당 (만약 외부에서 지정 안 한 경우)
        if (character == null)
            character = GetComponent<RectTransform>();

        // 캔버스 크기 계산
        canvasRect = GetComponentInParent<Canvas>().GetComponent<RectTransform>();

        StartCoroutine(MoveLoop());
    }

    IEnumerator MoveLoop()
    {
        while (true)
        {
            // 1. 랜덤 위치 구하기 (캔버스 안에서)
            Vector2 targetPos = GetRandomPositionInsideCanvas();

            // 2. 이동
            yield return StartCoroutine(MoveTo(targetPos));

            // 3. 멈춤 (랜덤 시간)
            float wait = Random.Range(waitTimeMin, waitTimeMax);
            yield return new WaitForSeconds(wait);
        }
    }

    IEnumerator MoveTo(Vector2 target)
    {
        while (Vector2.Distance(character.anchoredPosition, target) > 1f)
        {
            character.anchoredPosition = Vector2.MoveTowards(
                character.anchoredPosition,
                target,
                moveSpeed * Time.deltaTime
            );
            yield return null;
        }
    }

    Vector2 GetRandomPositionInsideCanvas()
    {
        float halfWidth = canvasRect.rect.width / 2f;
        float halfHeight = canvasRect.rect.height / 2f;

        float x = Random.Range(-halfWidth, halfWidth);
        float y = Random.Range(-halfHeight, halfHeight);

        return new Vector2(x, y);
    }
}

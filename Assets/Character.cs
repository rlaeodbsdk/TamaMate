using System.Collections;
using UnityEngine;

public class Character : MonoBehaviour
{
    public RectTransform character; // �̰� ���� �Ǵ� �ܺο��� ����
    public float moveSpeed = 300f; // px/sec
    public float waitTimeMin = 1f;
    public float waitTimeMax = 3f;

    private RectTransform canvasRect;

    void Start()
    {
        // ���� ĳ���� RectTransform�� �ڵ����� �Ҵ� (���� �ܺο��� ���� �� �� ���)
        if (character == null)
            character = GetComponent<RectTransform>();

        // ĵ���� ũ�� ���
        canvasRect = GetComponentInParent<Canvas>().GetComponent<RectTransform>();

        StartCoroutine(MoveLoop());
    }

    IEnumerator MoveLoop()
    {
        while (true)
        {
            // 1. ���� ��ġ ���ϱ� (ĵ���� �ȿ���)
            Vector2 targetPos = GetRandomPositionInsideCanvas();

            // 2. �̵�
            yield return StartCoroutine(MoveTo(targetPos));

            // 3. ���� (���� �ð�)
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

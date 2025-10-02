using UnityEngine;

public class DragArrow : MonoBehaviour
{
    private LineRenderer lineRenderer;
    [Header("矢印の曲線パラメータ")]
    public int pointsCount;
    public float arcModifier;

    private Vector3 mousePos;
    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10.0f));
        SetArrowPosition();
    }

    #region  ChatGPTから,ベジエ曲線
    public void SetArrowPosition()
    {
        Vector3 cardPosition = transform.position; // カードの位置
        Vector3 direction = mousePos - cardPosition; // カードからマウスへ向かう方向
        Vector3 normalizedDirection = direction.normalized; // 正規化された方向

        // カードからマウスへの方向に垂直なベクトルを計算する
        Vector3 perpendicular = new(-normalizedDirection.y, normalizedDirection.x, normalizedDirection.z);

        // 制御点のオフセットを設定する
        Vector3 offset = perpendicular * arcModifier; // この値を調整することで、曲線の形を変えることができる

        Vector3 controlPoint = (cardPosition + mousePos) / 2 + offset; // 制御点


        lineRenderer.positionCount = pointsCount; // LineRenderer のポイント数を設定する

        for (int i = 0; i < pointsCount; i++)
        {
            float t = i / (float)(pointsCount - 1);
            Vector3 point = CalculateQuadraticBezierPoint(t, cardPosition, controlPoint, mousePos);
            lineRenderer.SetPosition(i, point);
        }
    }

    //二次ベジエ曲線上の点を計算する
    Vector3 CalculateQuadraticBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2)
    {
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;

        Vector3 p = uu * p0; // 第一項
        p += 2 * u * t * p1; // 第二項
        p += tt * p2; // 第三項

        return p;
    }
    #endregion
}



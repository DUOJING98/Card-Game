using UnityEngine;

public class DragArrow : MonoBehaviour
{
    private LineRenderer lineRenderer;
    [Header("ʸӡ�������ѥ��`��")]
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

    #region  ChatGPT����,�٥�������
    public void SetArrowPosition()
    {
        Vector3 cardPosition = transform.position; // ���`�ɤ�λ��
        Vector3 direction = mousePos - cardPosition; // ���`�ɤ���ޥ������򤫤�����
        Vector3 normalizedDirection = direction.normalized; // ��Ҏ�����줿����

        // ���`�ɤ���ޥ����ؤη���˴�ֱ�ʥ٥��ȥ��Ӌ�㤹��
        Vector3 perpendicular = new(-normalizedDirection.y, normalizedDirection.x, normalizedDirection.z);

        // ������Υ��ե��åȤ��O������
        Vector3 offset = perpendicular * arcModifier; // ���΂����{�����뤳�Ȥǡ��������Τ�䤨�뤳�Ȥ��Ǥ���

        Vector3 controlPoint = (cardPosition + mousePos) / 2 + offset; // ������


        lineRenderer.positionCount = pointsCount; // LineRenderer �Υݥ���������O������

        for (int i = 0; i < pointsCount; i++)
        {
            float t = i / (float)(pointsCount - 1);
            Vector3 point = CalculateQuadraticBezierPoint(t, cardPosition, controlPoint, mousePos);
            lineRenderer.SetPosition(i, point);
        }
    }

    //���Υ٥��������Ϥε��Ӌ�㤹��
    Vector3 CalculateQuadraticBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2)
    {
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;

        Vector3 p = uu * p0; // ��һ�
        p += 2 * u * t * p1; // �ڶ��
        p += tt * p2; // �����

        return p;
    }
    #endregion
}



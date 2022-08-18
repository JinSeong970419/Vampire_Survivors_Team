using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCurve : ProjectilePrefab
{
    public TrailRenderer trail;
    public float tspeed = 2f;
    public float t = 0;

    Vector2[] point = new Vector2[4];              // 위치 계산용 4개 포인트 배열
    [HideInInspector] public Transform myPigeon = null;  // 둘기 위치
    public float posX = 3;    // x좌표 생성용
    public float posY = 2;    // y좌표 생성용

    public float CorPosition = 0.5f;  // y축 보정용

    // Bezier Curve 좌표값 불러오기
    protected override void OnEnable()
    {
        base.OnEnable();
        trail.Clear();
        t = 0;
    }

    public void Initialized(Transform myPigeon,Transform enemy)
    {
        // P0 -> 시작 위치
        point[0] = myPigeon.transform.position;
        // P1 -> 비둘기 Object의 Point, 1
        point[1] = RandPoint(myPigeon.transform.position);
        // P2 -> 적 Object의 Point, 2
        point[2] = RandPoint(enemy.position); ;
        // P3 -> 적 Object 위치
        point[3] = enemy.position;
        point[3][1] += CorPosition;
    }

    void FixedUpdate()
    {
        if (t > 1)
        {
            ObjectPooler.Instance.DestroyGameObject(gameObject);
            return;
        }

        t += Time.fixedDeltaTime;
        AttackTrajectroy();
    }

    // Point 좌표 랜덤 지정
    Vector2 RandPoint(Vector2 point)
    {
        float x, y;

        x = posX * Mathf.Cos(Random.Range(0, 360) * Mathf.Deg2Rad) + point.x;
        y = posY * Mathf.Sin(Random.Range(0, 360) * Mathf.Deg2Rad) + point.y;

        return new Vector2(x,y);
    }
    // Bezier Curve 궤적 그리기
    private void AttackTrajectroy()
    {
        float x = BezierPoint(point[0].x, point[1].x, point[2].x, point[3].x);
        float y = BezierPoint(point[0].y, point[1].y, point[2].y, point[3].y);
        transform.position = new Vector2(x,y);
    }

    // Bazier Curve Equation
    // p=(1-t)^3*P0 + 3(1-t)^2*t*P1+3(1-t)t^2*P2+t^3*P3
    private float BezierPoint(float P0, float P1, float P2, float P3)
    {
        return Mathf.Pow((1 - t), 3) * P0
            + Mathf.Pow((1 - t), 2) * 3 * t * P1
            + Mathf.Pow(t, 2) * 3 * (1 - t) * P2
            + Mathf.Pow(t, 3) * P3;
    }

}

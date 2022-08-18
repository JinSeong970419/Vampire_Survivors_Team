using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnPoint : MonoBehaviour
{
    public GameObject[] enemy = null;
    public int enemyCount = 0;
    public float interval = 0.5f;
    private SpriteRenderer render = null;
    private int randomEnemy = 0;
   
    private void Awake()
    {
        render = GetComponent<SpriteRenderer>();
    }

    // 오브젝트 활성화시 생성
    private void OnEnable()
    {
        if(enemy != null)
        {
            render.color = new Color(1, 1, 1, 1);
            StartCoroutine(Spowner(1));       

        }
    }

    IEnumerator Spowner(float delay )
    {
        yield return new WaitForSeconds(delay);

        for (int i = 0; i < enemyCount; i++)
        {
            randomEnemy = Random.Range(0,enemy.Length);
            GameObject obj = ObjectPooler.Instance.GenerateGameObject(enemy[randomEnemy]);
            obj.transform.position = transform.position;

            // 몬스터간의 간격
            yield return new WaitForSeconds(interval);
        }

        // 적 오브젝트 생성이후 오브젝트 비활성화
        //gameObject.SetActive(false);
        render.color = new Color(1, 1, 1, 0);
    }
}

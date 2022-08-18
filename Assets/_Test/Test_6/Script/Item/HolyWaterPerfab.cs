using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HolyWaterPerfab : MonoBehaviour
{
    internal float speed;
    internal float might;
    internal float area;
    internal Rigidbody2D rigid;

    LayerMask mask = new LayerMask();
    Vector2 HolyWaterZone;

    private GameObject HolyWaterUnit;
    public GameObject HolyWaterZones;

    IEnumerator coroutine;

    private void OnEnable()
    {
        rigid = GetComponent<Rigidbody2D>();
        GetComponent<SpriteRenderer>().enabled = true;
        mask = LayerMask.GetMask("Enemy");
        coroutine = HolyWaterArea();
        Invoke("Dead", Random.Range(1f,1.5f));
    }

    
    void Dead()
    {
        
            HolyWaterZone = transform.position;
            HolyWaterUnit = ObjectPooler.Instance.GenerateGameObject(HolyWaterZones);
            HolyWaterUnit.transform.localScale = Vector3.one * (area*2);
            HolyWaterUnit.transform.position = HolyWaterZone;
            StartCoroutine(coroutine);
            GetComponent<SpriteRenderer>().enabled = false;
            //Debug.Log("와장창");
        
    }

    IEnumerator HolyWaterArea()
    {
       // Debug.Log(might);
        for (int j = 0; j < 5; j++)
        {
            Collider2D[] cols = Physics2D.OverlapCircleAll(HolyWaterZone, area, mask);
            if (cols != null)
            {

                //데미지 주기
                for (int i = 0; i < cols.Length; i++)
                {
                    cols[i].gameObject.GetComponent<Enemy>().TakeDamage(might, transform.position);
                     
                }
            }
            yield return new WaitForSeconds(0.3f);
        }
      
        ObjectPooler.Instance.DestroyGameObject(this.gameObject);
        ObjectPooler.Instance.DestroyGameObject(HolyWaterUnit);
       // Debug.Log("파괴");
        StopCoroutine(coroutine);
        coroutine = HolyWaterArea();
    }
}

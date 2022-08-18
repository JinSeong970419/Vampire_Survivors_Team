using System.Collections;
using UnityEngine;

public class GameManager
{
    private RoomManager _room;
    private Player _player;

    public Player Player
    {
        get
        {
            if(_player == null) 
                _player = Object.FindObjectOfType<Player>();
            return _player;
        }
    }
    
    public RoomManager Room
    {
        get
        {
            if (_room == null)
                _room = Object.FindObjectOfType<RoomManager>();
            return _room;
        }
    }
    
    public void GameOver()
    {
        Managers.UI.GetGameOverUI().OnGameOver();
    }

    public void GameClear()
    {
        Managers.UI.GetGameOverUI().OnGameClear();
    }
    
}


#if false

    

    public GameObject enemyObject;
    public int enemyCount;
    private float delay;
    public GameObject expPrefab;

    private void Start()
    {
        if (expPrefab != null)
        {
            GameObject obj = ObjectPooler.Instance.GenerateGameObject(expPrefab);
            obj.transform.position = new Vector3(-5f, 2, 0f); //test
        }

        StartCoroutine(SpawnEnemy(2, 2)); //TEST
    }

    IEnumerator SpawnEnemy(float delay, float time) // TEST
    {
        yield return new WaitForSeconds(delay);

        while (true)
        {
            for (int i = 0; i < enemyCount; i++)
            {
                GameObject obj = ObjectPooler.Instance.GenerateGameObject(enemyObject);
                obj.transform.position = transform.position;
                yield return new WaitForSeconds(0.1f);
            }

            yield return new WaitForSeconds(time);
        }
    }

#endif
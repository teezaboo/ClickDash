using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnMonsterInHere : MonoBehaviour
{
    private GameObject _pool;
    [SerializeField] private List<Pool> monsterPrefabs; // สร้าง public array สำหรับเก็บ monsterPrefab
    public void timeToBack()
    {
        StartCoroutine(BackToPoolCoroutine());
    }
    private IEnumerator BackToPoolCoroutine()
    {
        yield return new WaitForSeconds(_pool.GetComponent<Pool_SpawnMonster>().GetTimeToBack());
        monsterPrefabs[Random.Range(0, monsterPrefabs.Count)].GetPool(transform.position, new Vector3(0,0,0), new Vector3(1,1,1));
        gameObject.SetActive(false);
    }
    public void SetPool(GameObject pool)
    {
        _pool = pool;
    }
}

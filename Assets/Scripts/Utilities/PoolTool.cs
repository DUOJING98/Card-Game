using UnityEngine;
using UnityEngine.Pool;

[DefaultExecutionOrder(-10)] //¥³©`¥É¤ÎŒgÐÐí˜Ðò

public class PoolTool : MonoBehaviour
{
    public GameObject objPrefab;

    private ObjectPool<GameObject> pool;

    private void Awake()
    {
        //³õÆÚ»¯
        pool = new ObjectPool<GameObject>(
            createFunc: () => Instantiate(objPrefab, transform),
            actionOnGet: (obj) => obj.SetActive(true),
            actionOnRelease: (obj) => obj.SetActive(false),
            actionOnDestroy: (obj) => Destroy(obj),
            collectionCheck: false,
            defaultCapacity:10,
            maxSize:20
            );

        PreFillPoll(7);
    }


    private void PreFillPoll(int count)
    {
        var preFillArray = new GameObject[count];
        for(int i = 0; i < count; i++)
        {
            preFillArray[i]=pool.Get();
        }

        foreach(GameObject item in preFillArray)
        {
            pool.Release(item);
        }
    }

    public GameObject GetObjectFromPool()
    {
        return pool.Get();
    }

    public void ReturnObjectToPool(GameObject obj)
    {
        pool.Release(obj);
    }
}

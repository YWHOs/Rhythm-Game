using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ObjectInfo
{
    public GameObject go;
    public int count;
    public Transform parent;
}
public class ObjectPooling : MonoBehaviour
{
    [SerializeField] ObjectInfo[] objectInfos;

    public static ObjectPooling instance;

    public Queue<GameObject> note = new Queue<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        note = InsertQueue(objectInfos[0]);
    }

    Queue<GameObject> InsertQueue(ObjectInfo _objectInfo)
    {
        Queue<GameObject> queue = new Queue<GameObject>();

        for (int i = 0; i < _objectInfo.count; i++)
        {
            GameObject prefab = Instantiate(_objectInfo.go, transform.position, Quaternion.identity);
            if(prefab != null)
            {
                prefab.SetActive(false);
                if (_objectInfo.parent != null)
                    prefab.transform.SetParent(_objectInfo.parent);
                else
                    prefab.transform.SetParent(this.transform);
            }

            queue.Enqueue(prefab);
        }
        return queue;
    }
}

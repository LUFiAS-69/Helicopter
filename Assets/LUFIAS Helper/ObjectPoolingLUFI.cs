using System.Collections.Generic;
using UnityEngine;

namespace ObjectPoolingLUFI
{
    public class PoolBucket
    {
        public string itemName;
        public GameObject prefab;
        public int amount;

        List<GameObject> prefabList;
        Transform parent;

        public PoolBucket(string _name, GameObject _prefab, int _amount)
        {
            itemName = _name;
            prefab = _prefab;
            amount = _amount;

            GeneratePool();
        }

        void GeneratePool()
        {
            parent = new GameObject(itemName).transform;
            prefabList = new List<GameObject>(amount);
            for (int i = 0; i < amount; i++)
            {
                GameObject g = MonoBehaviour.Instantiate(prefab);
                g.SetActive(false);
                g.transform.SetParent(parent);

                prefabList.Add(g);
            }
        }

        public GameObject GetItem()
        {
            foreach (GameObject item in prefabList)
            {
                if (!item.activeInHierarchy)
                {
                    item.SetActive(true);
                    return item;
                }
            }
            GameObject g = MonoBehaviour.Instantiate(prefab);
            g.transform.SetParent(parent);
            prefabList.Add(g);
            return g;
        }
    }
}
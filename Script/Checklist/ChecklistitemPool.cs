using System.Collections.Generic;
using UnityEngine;

public class ChecklistItemPool : MonoBehaviour
{
    [SerializeField] private ChecklistItemUI prefab;
    [SerializeField] private Transform parent;

    private readonly List<ChecklistItemUI> pool = new List<ChecklistItemUI>();

    public ChecklistItemUI GetObject()
    {
        // Step 1: Look for inactive object
        foreach (var item in pool)
        {
            if (!item.gameObject.activeSelf)
            {
                item.gameObject.SetActive(true);
                return item;
            }
        }

        // Step 2: If none available, create a new one
        var newItem = Instantiate(prefab, parent);
        newItem.transform.localScale = Vector3.one;
        pool.Add(newItem);
        return newItem;
    }

    public void ReturnAllObjects()
    {
        foreach (var item in pool)
        {
            item.gameObject.SetActive(false);
        }
    }

    public List<ChecklistItemUI> GetPoolList()
    {
        return pool;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    #region Singleton
    public static InventoryManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
    #endregion

    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;

    public int slots = 20;
    public List<Item> items;

    private void Start()
    {
        items = new List<Item>();
    }

    public bool Add(Item item)
    {
        if (!item.isDefaultItem)
        {
            if (items.Count >= slots)
            {
                Debug.Log("Not enough room in inventory");
                return false;
            }
            items.Add(item);

            if (onItemChangedCallback != null)
                onItemChangedCallback.Invoke();

        }
        return true;
    }

    public void Remove(Item item)
    {
        items.Remove(item);
        if (onItemChangedCallback != null)
            onItemChangedCallback.Invoke();
    }

}

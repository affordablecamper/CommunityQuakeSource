using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {

#region singleton

    public static Inventory instance;

    private void Awake()
    {
        instance = this;
    }

    #endregion

    public int space = 20;
    public List<Item> items = new List<Item>();
    public delegate void onItemChanged();
    public onItemChanged onItemChangedCallback;

    public bool Add(Item item) {

        if (items.Count >= space)
        {

            return false;

        }

        items.Add(item);
        if(onItemChangedCallback != null)
            onItemChangedCallback.Invoke();

        

        return true;
        
    }

    public void Remove(Item item){


        items.Remove(item);
        if (onItemChangedCallback != null)
            onItemChangedCallback.Invoke();
    }

}

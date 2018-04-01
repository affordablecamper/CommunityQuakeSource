
using UnityEngine;

public class Pickupable : Interactable {
    public Item item;

    public override void Interact()
    {
        base.Interact();

        PickUp();
    }

    private void PickUp()
    {
        Debug.Log("Player is trying to pick up an " + item.Itemname);
        bool wasPickedUp = Inventory.instance.Add(item);
        if(wasPickedUp)
        Destroy(gameObject);
    }
}

using UnityEngine;

public class ItemData : ScriptableObject
{
    string itemID;
    [SerializeField] string itemName;
    [SerializeField] Sprite itemIcon;

    public string ItemID { get { return itemID; } private set { itemID = value; } }
    public string ItemName { get { return itemName; } set { itemName = value; } }
    public Sprite ItemIcon { get { return itemIcon; } set { itemIcon = value; } }

    public void SetID(string newID)
    {
        if (ItemID != string.Empty) return;

        ItemID = newID;
    }
}
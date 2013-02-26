using UnityEngine;

public class ItemDropReward : ServerDeathEvent
{
    public string[] itemNames;
    public float[] itemDropRates;

    protected override void DeathEvent()
    {
        for (int g = 0; g < itemDropRates.Length; g++)
        {
            if (itemDropRates[g].TryRandomRoll())
            {
                DropItem(itemNames[g]);
            }
        }
    }

    private void DropItem(string itemName)
    {
        GameObject worldItemGO = Instantiate(WorldItem.GetWorldItemPrefab(itemName)) as GameObject;
        var wi = worldItemGO.GetComponent<WorldItem>();
        wi.itemName = itemName;
        wi.item = ItemFactory.CreateItemForPlayer(itemName);

        wi.transform.position = this.transform.position + Vector3.up * 2f;
        wi.transform.position += (1 - (2 * Random.value)) * Vector3.left + (1 - (2 * Random.value)) * Vector3.right;

        MessageTerminal.Main.networkView.RPC("SpawnWorldItem", RPCMode.Others, itemName, wi.item.viewID, transform.position, wi.networkView.viewID);
        var dropEffect = Resources.Load("Items/WorldItems/DropEffect") as GameObject;
        var dropGO = Instantiate(dropEffect) as GameObject;
        dropGO.transform.SetParentAndCenter(worldItemGO.transform);
    }
}
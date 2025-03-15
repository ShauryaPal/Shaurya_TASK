using UnityEngine;

public class Prop_Stall_UIManager : MonoBehaviour
{
    [SerializeField] private Prop_Stall_TradeItem tradedItemPrefab;
    [SerializeField] private Transform tradedItemsHolder;

    public void SetupStallUI(Prop_Stall_Items[] stallItems)
    {
        foreach (Transform child in tradedItemsHolder)
            Destroy(child.gameObject);

        foreach (var item in stallItems)
        {
            var spawnedTradeItem = Instantiate(tradedItemPrefab, tradedItemsHolder);
            spawnedTradeItem.SetTradedItem(item.tradedItem, item.recievedItem, item.tradedItemCount, item.recievedItemCount);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class CartMachinery_Controller : MonoBehaviour
{
    public List<CartMachineryItem_ObjectController> itemsInCartList = new List<CartMachineryItem_ObjectController>();
    
    public GameObject itemObjectListPrefab;
    public GameObject cartItemGridLayout;
    public TextMeshProUGUI totalPriceText;

    public delegate void SelectCartItemObject(CartMachineryItem_ObjectController itemIncartObject);
    public SelectCartItemObject selectCartItemObject;

    private CartMachineryItem_ObjectController currentData;

    public long totalPrice = 0;

    public void OnClickSelectedObject(CartMachineryItem_ObjectController itemDataBean) //********
    {
        currentData = itemDataBean;
        selectCartItemObject?.Invoke(currentData);

        Debug.Log("Selected Cart Item name : " + currentData.itemData.machineName);
    }
    public void AddToCart(MachineryDataBean targetItem, ShopMachinery_Controller shopController)
    {
        if (targetItem != null)
        {
            GameObject go = GameObjectUtil.Instance.AddChild(cartItemGridLayout, itemObjectListPrefab);
            CartMachineryItem_ObjectController itemObj = go.GetComponent<CartMachineryItem_ObjectController>(); // get component of CartMachineryItemController
            itemObj.Init(targetItem);// Init itemObj by using value form targetItem
            itemObj.RegisterShopMachineController(this);

            itemsInCartList.Add(itemObj);
            CalculateTotal();
        }
    }

    public void RemoveItemFromCart(CartMachineryItem_ObjectController targetItem)
    {
        if (itemsInCartList.Count > 0 )
        {
            CartMachineryItem_ObjectController tmpTargetItem = itemsInCartList[itemsInCartList.IndexOf(targetItem)];
            itemsInCartList.Remove(tmpTargetItem);
            Destroy(tmpTargetItem.gameObject);
            currentData = null;
        }
    }

    public void RemoveFromCart()
    {
        if (currentData != null)
        {
            RemoveItemFromCart(currentData);
            CalculateTotal();
        }
    }

    public List<CartMachineryItem_ObjectController> CheckoutItemInCart()
    {
        List<CartMachineryItem_ObjectController> tmpItemList = new List<CartMachineryItem_ObjectController>();
        itemsInCartList.ForEach(item =>
        {
            tmpItemList.Add(item);
            //Debug.Log("TmpItemList Data :: [" + tmpItemList.ToString() + "]");
        });
        //ClearCart();
        CalculateTotal();
        return tmpItemList;
    }


    public void ClearCart()
    {
        foreach (CartMachineryItem_ObjectController item in itemsInCartList)
        {
            Destroy(item.gameObject);
        }
        itemsInCartList.Clear();
        CalculateTotal();
    }

    public void CalculateTotal()
    {
        //Debug.Log("CalculateTotal is ACTIVE");
        long tmpTotalPrice = 0;
        itemsInCartList.ForEach(x => {
            tmpTotalPrice += x.itemData.machinePriceNVC;
        });

        totalPrice = tmpTotalPrice;
        totalPriceText.text = "Total Price :: " + FormatPrice(totalPrice) + " NVC$";
    }

    private string FormatPrice(long price)
    {
        return price.ToString("N0");
    }


}

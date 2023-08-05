using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class ShopCartController : MonoBehaviour
{
    public List<CartMachineryItemController> itemsInCartList = new List<CartMachineryItemController>();
    
    public GameObject itemObjectListPrefab;
    public GameObject cartItemGridLayout;
    public TextMeshProUGUI totalPriceText;

    public delegate void SelectCartItemObject(CartMachineryItemController itemIncartObject);
    public SelectCartItemObject selectCartItemObject;

    private CartMachineryItemController currentData;

    public int totalPrice = 0;

    public void OnClickSelectedObject(CartMachineryItemController itemDataBean) //********
    {
        currentData = itemDataBean;
        selectCartItemObject?.Invoke(currentData);

        Debug.Log("Selected Cart Item name : " + currentData.itemData.machineName);
    }
    public void AddToCart(MachineryDataBean targetItem, ShopCartController shopCartController)
    {
        if (targetItem != null)
        {
            GameObject go = GameObjectUtil.Instance.AddChild(cartItemGridLayout, itemObjectListPrefab);
            CartMachineryItemController itemObj = go.GetComponent<CartMachineryItemController>(); // get component of CartMachineryItemController
            itemObj.Init(targetItem);// Init itemObj by using value form targetItem
            itemObj.RegisterShopMachineController(this);

            itemsInCartList.Add(itemObj);
            CalculateTotal();
        }
    }

    public void RemoveItemFromCart(CartMachineryItemController targetItem)
    {
        if (itemsInCartList.Count > 0 )
        {
            CartMachineryItemController tmpTargetItem = itemsInCartList[itemsInCartList.IndexOf(targetItem)];
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

    public List<CartMachineryItemController> CheckoutItemInCart()
    {
        List<CartMachineryItemController> tmpItemList = new List<CartMachineryItemController>();
        foreach (CartMachineryItemController item in itemsInCartList)
        {
            Destroy(item.gameObject);
        }
        itemsInCartList.Clear();
        CalculateTotal();
        return tmpItemList;
    }


    public void ClearCart()
    {
        foreach (CartMachineryItemController item in itemsInCartList)
        {
            Destroy(item.gameObject);
        }
        itemsInCartList.Clear();
    }

    public void CalculateTotal()
    {
        Debug.Log("CalculateTotal is ACTIVE");
        itemsInCartList.ForEach(x =>
        {
           totalPrice += x.itemData.machinePriceNVC;
        });

        totalPriceText.text = "Total Price : " + FormatPrice(totalPrice) + " NVC$";

    }

    private string FormatPrice(int price)
    {
        return price.ToString("N0");
    }


}

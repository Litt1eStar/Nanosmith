using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
public class ShopItemCartController : MonoBehaviour
{
    public List<CartGenerateItemController> itemsInCartList = new List<CartGenerateItemController>();

    public GameObject itemObjectListPrefab;
    public GameObject cartItemGridLayout;
    public TextMeshProUGUI totalPriceText;

    public delegate void SelectCartItemObject(CartGenerateItemController itemIncartObject);
    public SelectCartItemObject selectCartItemObject;

    private CartGenerateItemController currentData;

    public int totalPrice = 0;

    public void OnClickSelectedObject(CartGenerateItemController itemDataBean) //********
    {
        currentData = itemDataBean;
        selectCartItemObject?.Invoke(currentData);

        Debug.Log("Selected Cart Item name : " + currentData.itemData.itemName);
    }


    public void AddToCart(ItemsDataBean targetItem, ShopGenerateItemController shopController)
    {
        if (targetItem != null)
        {
            GameObject go = GameObjectUtil.Instance.AddChild(cartItemGridLayout, itemObjectListPrefab);
            CartGenerateItemController itemObj = go.GetComponent<CartGenerateItemController>(); // get component of CartMachineryItemController
            itemObj.InitItemShop(targetItem);// Init itemObj by using value form targetItem
            itemObj.RegisterShopItemController(this);

            itemsInCartList.Add(itemObj);
            CalculateTotal();
        }
    }


    public void RemoveItemFromCart(CartGenerateItemController targetItem)
    {
        if (itemsInCartList.Count > 0)
        {
            CartGenerateItemController tmpTargetItem = itemsInCartList[itemsInCartList.IndexOf(targetItem)];
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

    public List<CartGenerateItemController> CheckoutItemInCart()
    {
        List<CartGenerateItemController> tmpItemList = new List<CartGenerateItemController>();
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
        foreach (CartGenerateItemController item in itemsInCartList)
        {
            Destroy(item.gameObject);
        }
        itemsInCartList.Clear();
        CalculateTotal();
    }


    public void CalculateTotal()
    {
        //Debug.Log("CalculateTotal is ACTIVE");
        float tmpTotalPrice = 0;
        itemsInCartList.ForEach(x => {
            tmpTotalPrice += x.itemData.itemMarginNVC;
        });
        if (totalPriceText != null)
        {
            totalPrice = (int) tmpTotalPrice;
            totalPriceText.text = "Total Price :: " + FormatPrice(totalPrice);
        }
    }

    private string FormatPrice(int price)
    {
        return price.ToString("N0");
    }


}

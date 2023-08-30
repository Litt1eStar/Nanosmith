using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
public class CartGenerateItem_Controller : MonoBehaviour
{
    public List<CartGenerateItem_ObjectController> itemsInCartList = new List<CartGenerateItem_ObjectController>();

    public GameObject itemObjectListPrefab;
    public GameObject cartItemGridLayout;
    public TextMeshProUGUI totalPriceText;

    public delegate void SelectCartItemObject(CartGenerateItem_ObjectController itemIncartObject);
    public SelectCartItemObject selectCartItemObject;

    private CartGenerateItem_ObjectController currentData;

    public int totalPrice = 0;

    public void OnClickSelectedObject(CartGenerateItem_ObjectController itemDataBean) //********
    {
        currentData = itemDataBean;
        selectCartItemObject?.Invoke(currentData);

        Debug.Log("Selected Cart Item name : " + currentData.itemData.itemName);
    }


    public void AddToCart(ItemsDataBean targetItem, ShopGenerateItem_Controller shopController)
    {
        if (targetItem != null)
        {
            CartGenerateItem_ObjectController existingItem = itemsInCartList.Find(item => item.itemData == targetItem);
            if (existingItem != null)
            {
                existingItem.IncreaseStack(); // Increase the stack count of existing item
            }
            else
            {
                GameObject go = GameObjectUtil.Instance.AddChild(cartItemGridLayout, itemObjectListPrefab);
                CartGenerateItem_ObjectController itemObj = go.GetComponent<CartGenerateItem_ObjectController>(); // get component of CartMachineryItemController
                itemObj.InitItemShop(targetItem);// Init itemObj by using value form targetItem
                itemObj.RegisterShopItemController(this);

                itemsInCartList.Add(itemObj);
            }
            
            CalculateTotal();
        }
    }


    public void RemoveItemFromCart(CartGenerateItem_ObjectController targetItem)
    {
        if (itemsInCartList.Count > 0)
        {
            CartGenerateItem_ObjectController tmpTargetItem = itemsInCartList[itemsInCartList.IndexOf(targetItem)];
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

    public List<CartGenerateItem_ObjectController> CheckoutItemInCart()
    {
        List<CartGenerateItem_ObjectController> tmpItemList = new List<CartGenerateItem_ObjectController>();
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
        foreach (CartGenerateItem_ObjectController item in itemsInCartList)
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
            totalPriceText.text = "Total Price :: " + FormatPrice(totalPrice) + " NVC$";
        }
    }

    private string FormatPrice(int price)
    {
        return price.ToString("N0");
    }


}

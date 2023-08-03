using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class SpriteSheetUtil : MonoSingleton<SpriteSheetUtil>
{
    private Dictionary<string, Sprite> spriteData;
    private Dictionary<string, Texture2D> textureData;

    public void Init() //Set spriteData by using data from Folder named "Sprites"
    {
        this.spriteData = new Dictionary<string, Sprite>();

        List<Sprite> tempSprite = new List<Sprite>(Resources.LoadAll<Sprite>("Sprites/")); //Get Sprite from Folder in this Path
        List<Sprite> spriteList = new List<Sprite>(); // spriteList contain data from folder Sprites that's itemKey + _icon
        spriteList.AddRange(tempSprite);//Add item from folder Sprites to the end of spriteList

        foreach (Sprite sprite in spriteList)
        {
            if (this.spriteData.ContainsKey(sprite.name)) // if spriteData have same key as spriteList then sprite
            {
                this.spriteData[sprite.name] = sprite; //Storage value of Sprite in Sprites to spriteData by access to Key of spriteData
            }
            else
            {
                this.spriteData.Add(sprite.name, sprite);
            }
        }
        this.textureData = new Dictionary<string, Texture2D>();
    }

    public Sprite GetSpriteByName(string name) //Contain data of Sprite that get from check spriteData with name in Method Paramether
    {
        Sprite spriteChoose = null;
        if (name != null)
        {
            //Debug.Log("Name :: " + name);
            //Debug.Log("Name :: " + name + " :: this.spriteData.ContainKey (name) :: " + this.spriteData.ContainsKey(name));
            Debug.Log("GetSpriteByName ContainsKey :: " + this.spriteData.ContainsKey(name));
            if (this.spriteData.ContainsKey(name)) //If spriteData Contain key that is same from Parameter of Method
            {
                spriteChoose = this.spriteData[name]; // Let the value of spriteData to be value of spriteChoose
            }
        }
        return spriteChoose;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using System.Linq;
using UnityEngine;
using System;

public class SpriteCycler : MonoBehaviour
{
    [Serializable]//potato
    public struct SpriteFrame
    {
        public string name;
        public Texture2D image;
    }
    public SpriteFrame[] sprites;
    public String state;

    private Texture2D[] currentState;
    private Texture2D currentSprite;

    private int spriteIndex;
    public float switchTime = 0.1f;

    public Rect r_Sprite;


    Texture2D[] GetSpritesByState(string state)
    {
        List<Texture2D> stateSprites = new List<Texture2D>();
        foreach(SpriteFrame s in sprites)
        {
            if(s.name.Split('_')[0].Equals(state))
            {
                stateSprites.Add(s.image);
            }
        }
        return stateSprites.ToArray();
    }

    void Start()
    {
        state = sprites[0].name.Split('_')[0];
        spriteIndex = 0;
        currentState = this.GetSpritesByState(state);
        StartCoroutine("SwitchSprite");
    }

    void OnGUI()
    {
        r_Sprite.x = transform.position.x;
        r_Sprite.y = transform.position.y;
        GetComponent<SpriteRenderer>().sprite = Sprite.Create(currentSprite, r_Sprite, new Vector2());
        Debug.Log(currentSprite.name);//TODO negative coords break Sprite.Create
    }

    private IEnumerator SwitchSprite()
    {
        currentSprite = currentState[spriteIndex];

        if (spriteIndex < currentState.Length - 1)
        {
            spriteIndex++;
        }
        else
        {
            spriteIndex = 0;
        }

        yield return new WaitForSeconds(switchTime);
        StartCoroutine("SwitchSprite");
    }
}
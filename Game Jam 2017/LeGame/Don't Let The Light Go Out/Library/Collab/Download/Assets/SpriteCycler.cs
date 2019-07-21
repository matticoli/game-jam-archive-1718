using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using System;

public class SpriteCycler : MonoBehaviour
{
    [Serializable]
    public struct SpriteFrame
    {
        public string name;
        public Sprite image;
    }
    public SpriteFrame[] sprites;
    public String state;

    private Sprite[] currentState;
    private Sprite currentSprite;

    private int spriteIndex;
    public float switchTime = 0.1f;

    public Rect r_Sprite;


    Sprite[] GetSpritesByState(string state)
    {
        List<Sprite> stateSprites = new List<Sprite>();
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
        r_Sprite.x = (Math.Abs(transform.position.x + (r_Sprite.width / 2)));
        r_Sprite.y = Math.Abs(transform.position.y);
        try
        {
            GetComponent<SpriteRenderer>().sprite = currentSprite;//Sprite.Create(currentSprite.texture, r_Sprite, new Vector2());
        } catch (MissingComponentException)
        {
            GetComponent<RawImage>().texture = currentSprite.texture;//Sprite.Create(currentSprite.texture, r_Sprite, new Vector2());
        }
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
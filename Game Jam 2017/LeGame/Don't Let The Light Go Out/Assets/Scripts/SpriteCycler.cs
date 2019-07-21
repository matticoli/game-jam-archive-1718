using System.Collections;
using System.Collections.Generic;
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

    private Sprite[] currentState;
    private Sprite currentSprite;

    private int spriteIndex;
    public float switchTime = 0.1f;

    public Rect r_Sprite;

    private String state;
    private string returnState;
    private bool lockState;


    void Start()
    {
        state = sprites[0].name.Split('_')[0];
        spriteIndex = 0;
        currentState = this.GetSpritesByState(state);
        StartCoroutine("SwitchSprite");
        returnState = "";
        lockState = false;
    }

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

    public bool StateLock()
    {
        return this.lockState;
    }

    public void StateLock(bool b)
    {
        this.lockState = b;
    }

    public string State()
    {
        return this.state;
    }

 
    /* If returnState is specified, given state will run one animation cycle and return to returnState.
     * otherwise, the sprite will remain in given state until updated */
    public void State(string state, string returnState = "")
    {
        if(this.state == state || this.lockState)
        {
            return;
        }
        // If a returnState has been specified, keep state locked until cycle finishes
        if(this.returnState.Length == 0)
        {
            this.state = state;
            spriteIndex = 0;
            this.returnState = returnState;
        }
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
    }

    private IEnumerator SwitchSprite()
    {
        currentState = this.GetSpritesByState(state);

        if (spriteIndex < currentState.Length - 1)
        {
            spriteIndex++;
        }
        else 
        {
            if (returnState.Length > 0) {
                this.state = returnState;
                this.returnState = "";
            }
            spriteIndex = 0;
        }

        currentSprite = currentState[spriteIndex];

        yield return new WaitForSeconds(switchTime);
        StartCoroutine("SwitchSprite");
    }
}
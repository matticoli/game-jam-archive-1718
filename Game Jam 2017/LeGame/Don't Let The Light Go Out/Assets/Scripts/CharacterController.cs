using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public float speedX, jumpSpeed;

    private Rigidbody2D rb;
    private Vector3 calib;
    private bool jump;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;
        calib = 4 * Input.acceleration;
        jump = false;
    }

    void RelocateEyeFire(float x, float y)
    {
        foreach (var child in transform)
        {
            try
            {

                if (((Transform)child).name.Equals("FIRE"))
                {
                    ((Transform)child).localPosition = new Vector3(x, y, ((Transform)child).localPosition.z);
                    break;
                }
            }catch (InvalidCastException e)
            {

            }
        }
    }

    void FixedUpdate()
    {
        float mx = Input.GetAxis("Horizontal") + Input.acceleration.x * 4 - calib.x;
        if (!(Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow)))
        {// Note: This will break with a joystick
            rb.velocity = new Vector2(0f, rb.velocity.y);
        }

        SpriteCycler sc = GetComponent<SpriteCycler>();

        if (Input.GetKeyDown(KeyCode.P))
        {
            sc.State("push", sc.State());
            sc.switchTime = 0.3f;
        } else if (Input.GetKey(KeyCode.H))
        {
            sc.State("hide");
            sc.StateLock(true);
            sc.switchTime = 0.3f;
        } else if(Input.GetKeyUp(KeyCode.H) || (sc.State()=="hide" && sc.StateLock()))
        {
            sc.StateLock(false);
        }
        else if(mx == 0)
        {
            sc.State("rest");
            sc.switchTime = 0.3f;//todo: fix this
            RelocateEyeFire((GetComponent<SpriteRenderer>().flipX ? -0.4f : 0f), 0f);
        } else
        {
            sc.State("walk");
            sc.switchTime = 0.1f;//todo: fix this
            RelocateEyeFire((mx > 0f ? -0.4f : 0f), 0.25f);
            
            //Debug.Log(mx + "   " + transform.localScale.x);
           GetComponent<SpriteRenderer>().flipX = (mx < 0f);
        }
        rb.AddForce(new Vector2(mx * speedX, 0.0f));
        if (Input.GetKeyDown(KeyCode.Space) && !jump)
        {
            rb.AddForce(new Vector2(0f, jumpSpeed), ForceMode2D.Force);
            jump = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        jump = false;
    }
}


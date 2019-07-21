// Smooth Follow from Standard Assets
// Converted to C# because I fucking hate UnityScript and it's inexistant C# interoperability
// If you have C# code and you want to edit SmoothFollow's vars ingame, use this instead.
using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour
{
    public GameObject player;
    public float minX, maxX;

    private Vector3 diff;

    private void Start()
    {
        diff = transform.position - player.transform.position;
    }

    private void Update()
    {
        if((player.transform.position.x + diff.x) <= minX)
        {
            transform.position = new Vector3(minX, transform.position.y, transform.position.z);
        } else if ((player.transform.position.x + diff.x) >= maxX)
        {
            transform.position = new Vector3(maxX, transform.position.y, transform.position.z);
        }
        else
        {
            Vector3 pos = player.transform.position + diff;
            transform.position = new Vector3(pos.x, transform.position.y, pos.z);
        }
    }
}
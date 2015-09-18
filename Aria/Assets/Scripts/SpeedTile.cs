using UnityEngine;
using System.Collections;

public class SpeedTile : MonoBehaviour {

    float mag = 150; // magnitude
    Vector2 vec;

    void OnTriggerEnter2D(Collider2D obj)
    {
        Rigidbody2D v = obj.gameObject.GetComponent<Rigidbody2D>();
        print(gameObject.tag);
        switch (gameObject.tag.Substring(7)) {
            case "Up":
                vec = Vector2.up;
                break;
            case "Right":
                vec = Vector2.right;
                break;
            case "Down":
                vec = -Vector2.up;
                break;
            case "Left":
                vec = -Vector2.right;
                break;
            default:
                print("tag error");
                break;
        }
        v.AddForce(vec*mag);
    }
}

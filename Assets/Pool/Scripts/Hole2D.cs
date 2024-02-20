using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hole2D : MonoBehaviour
{
    // declaring public variable so that this value can be accessed from other class
    public float Radius;

    // Start is called before the first frame update
    private void Awake()
    {
        // Getting spriteRender to gain access to different functions in SpriteRender Class
        Sprite sprite = GetComponent<SpriteRenderer>().sprite;
        // Using sprite.rect.size to get the vector value of the sprites width/height (vector value of width and height is same as width and height of circle is the same)
        HVector2D sprite_size = new HVector2D(sprite.rect.size);
        // Finding the local size of the sprite by deviding its vector length to its number of pixel in the sprite per one unit in world space
        HVector2D local_sprite_size = sprite_size / sprite.pixelsPerUnit;
        // Finding the radius of the hole by uising simple math (Radius = Diameter / 2)
        Radius = local_sprite_size.x / 2f;
    }

}

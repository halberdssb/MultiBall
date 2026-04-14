using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Holds data for collision bounds of the game space
 *
 * Jeff Stevenson
 * 4.14.26
 */
public class CollisionBounds : MonoBehaviour
{
    public static Vector3 CenterOfBounds { get; private set; }
    void Awake()
    {
        // assumes sides of bounds are equidistant from each other - move to calculate middle of bounds if not true/be more dynamic
        CenterOfBounds = transform.position;
    }
}

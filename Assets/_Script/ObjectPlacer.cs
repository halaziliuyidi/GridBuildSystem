using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPlacer : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> placedGameobjects = new();

    public int PlaceObject(GameObject prefab, Vector3 position)
    {

        GameObject newObject = Instantiate(prefab);
        newObject.transform.position =position;
        placedGameobjects.Add(newObject);
        return placedGameobjects.Count - 1;
    }

    internal void RemoveObjectAt(int gameObjectIndex)
    {
        if (placedGameobjects.Count <= gameObjectIndex|| placedGameobjects[gameObjectIndex] == null)
        {
            return;
        }
        Destroy(placedGameobjects[gameObjectIndex]);
        placedGameobjects[gameObjectIndex] = null;
    }
}

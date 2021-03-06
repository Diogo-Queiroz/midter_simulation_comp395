using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class QueueManager : MonoBehaviour
{
    //Queue<GameObject> queue = new Queue<GameObject>();
    List<GameObject> queue = new List<GameObject>();

    public GameObject Last()
    {
        GameObject go = null;

        if (queue.Count > 0)
        {
            go= queue[queue.Count - 1];
        }
        return go;
    }
    
    public GameObject First()
    {
        GameObject go = null;

        if (queue.Count > 0)
        {
            go = queue[0];
        }
        return go;
    }
    
    public void Add(GameObject gameObject)
    {
        queue.Add(gameObject);
    }
    
    public GameObject PopFirst()
    {
        GameObject go = null;
        if (queue.Count > 0)
        {
            go = queue[0];
            queue.RemoveAt(0);
        }
        return go;
    }
    
    public int Count()
    {   
        return queue.Count;
    }

}

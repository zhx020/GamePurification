using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Node : MonoBehaviour
{

    public GameObject[] surrondingNodes;
    private Transform currNode;
    private Dictionary<GameObject, int> d_surronding;
    //private LineRenderer line;

    private void Awake()
    {
        currNode = gameObject.transform;
        d_surronding = new Dictionary<GameObject, int>();

        //line = this.gameObject.AddComponent<LineRenderer>();
        //line.SetWidth(0.05F, 0.05F);
        //line.SetVertexCount(2);

        foreach (GameObject node in surrondingNodes){
            // add cost to each node, cost defined by distance
            d_surronding.Add(node, (int)Vector3.Distance(currNode.position, node.transform.position));
        }

    }

    public Dictionary<GameObject, int> GetSurrondingNodes()
    {
        return d_surronding;
    }


    //private void Update()
    //{
    //    foreach (GameObject node in d_surronding.Keys)
    //    {
    //        Debug.DrawLine(this.transform.position,node.transform.position);
    //       // Debug.DrawLine(this.transform.position, surrondingNodes[i].transform.position);
    //    }
    //}
}

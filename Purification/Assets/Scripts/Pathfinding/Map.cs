using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Map : ScriptableObject
{

    private Dictionary<GameObject, Dictionary<GameObject, int>> map;        // all nodes in the scene
    private PathFinder path_finder;

    private void Awake()
    {
        //pathFinder = new FindPath();
        map = new Dictionary<GameObject, Dictionary<GameObject, int>>();

        foreach (GameObject node in  GameObject.FindGameObjectsWithTag("Node")){
            map.Add(node, node.GetComponent<Node>().GetSurrondingNodes());
        }

        path_finder = new PathFinder(map);
    }

    public int GetCost(List<GameObject> path)
    {
        int total_cost = 0;

        for (int i = 0; i < path.Count - 1; i++)
        {
            total_cost = GetCost(path[i].GetComponent<Node>().GetSurrondingNodes(), path[i + 1]);
        }
        return total_cost;
    }

    private int GetCost(Dictionary<GameObject, int> surrondingNodes, GameObject goal){
        return surrondingNodes[goal];
    }

    public GameObject FindClosestPoint(GameObject currPosition, float objectHeight){

        return path_finder.FindClosestPoint(currPosition, objectHeight);
    }

    public List<GameObject> Shortest_path(GameObject start, GameObject finish){
        return path_finder.Shortest_path(start, finish);
    }
}
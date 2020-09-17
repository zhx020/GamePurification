using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder {

    private readonly Dictionary<GameObject, Dictionary<GameObject, int>> m_nodes;


    public PathFinder(Dictionary<GameObject, Dictionary<GameObject, int>> map)
    {
        m_nodes = map;
    }


    public GameObject FindClosestPoint(GameObject currPosition, float objectHeight){

        Vector3 destination = new Vector3(currPosition.transform.position.x, 
                                          currPosition.transform.position.y - objectHeight, 
                                          currPosition.transform.position.z);
        float shortestDis = Mathf.Infinity;
        var clostestP = currPosition;

        foreach (var node in m_nodes.Keys){
            // if the node is on the same horizontal line as the destination point, find the closest node
            // if not, jump forwards
            if (Mathf.Abs(node.transform.position.y - destination.y) < 1f){
                float currDis = Vector3.Distance(node.transform.position, destination);
                if (currDis < shortestDis)
                {
                    shortestDis = currDis;
                    clostestP = node;
                }
            }
        }
        return clostestP;
    }

    public List<GameObject> Shortest_path(GameObject start, GameObject finish)
    {
        var previous = new Dictionary<GameObject, GameObject>();
        var distances = new Dictionary<GameObject, int>();
        var nodes = new List<GameObject>();

        List<GameObject> path = null;

        foreach (var node in m_nodes)
        {
            distances[node.Key] = node.Key == start ? 0 : int.MaxValue;

            nodes.Add(node.Key);
        }

        while (nodes.Count != 0)
        {
            nodes.Sort((x, y) => distances[x] - distances[y]);

            var smallest = nodes[0];
            nodes.Remove(smallest);

            if (smallest == finish)
            {
                path = new List<GameObject>();
                while (previous.ContainsKey(smallest))
                {
                    path.Add(smallest);
                    smallest = previous[smallest];
                }

                break;
            }

            if (distances[smallest] == int.MaxValue)
            {
                break;
            }

            foreach (var neighbor in m_nodes[smallest])
            {
                var alt = distances[smallest] + neighbor.Value;
                if (alt < distances[neighbor.Key])
                {
                    distances[neighbor.Key] = alt;
                    previous[neighbor.Key] = smallest;
                }
            }
        }

        return path;
    }
}

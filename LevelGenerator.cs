using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private GameObject[] Roads;
    [SerializeField] private float Maxspeed = 2;
    [SerializeField] private int MaxRoadCount = 3;

    public float currentspeed = 0;

    private List<GameObject> roads = new List<GameObject>();

    void Start()
    {
        ResetLevel();
    }

    void Update()
    {
        if (currentspeed == 0) return;

        foreach (GameObject Road in roads)
        {
            if (Road != null)
                Road.transform.position -= new Vector3(currentspeed * Time.deltaTime, 0, 0);
        }

        if (roads[0].transform.position.x < -5f)
        {
            Destroy(roads[0]);
            roads.RemoveAt(0);

            CreateNextRoad(Random.Range(0, Roads.Length));
        }
    }

    void CreateNextRoad(int index)
    {
        Vector2 pos = Vector2.zero;

        if (roads.Count > 0)
        {
            pos = roads[roads.Count - 1].transform.position + new Vector3(5f, 0, 0);
        }

        GameObject go = Instantiate(Roads[index], pos, Quaternion.identity);
        go.transform.SetParent(transform, true);
        roads.Add(go);
    }

    public void StartLevel()
    {
        currentspeed = Maxspeed;
    }

    public void StopLevel()
    {
        currentspeed = 0;
    }

    public void ResetLevel()
    {
        currentspeed = 0;

        while (roads.Count > 0)
        {
            Destroy(roads[0]);

            roads.RemoveAt(0);
        }

        for (int i = 0; i < MaxRoadCount; i++)
        {
            CreateNextRoad(Random.Range(0, Roads.Length));
        }
    }
}

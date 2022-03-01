using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class AnotherGrid : Singleton<AnotherGrid>
{
    [SerializeField] private Vector3 grid;

    [SerializeField] private AnotherCell cellPrefab;

    [SerializeField] private int[] birth;
    [SerializeField] private int[] isLive;

    private List<GameObject> objs = new List<GameObject>();

    private bool steping = false;
    private IDisposable dis;
    public int[] Birth => birth;
    public int[] IsLive => isLive;

    public Action Check;

    private void Start()
    {
        Generate();
    }

    private void Update()
    {
        if (steping)
        {
            if (Input.GetKeyDown(KeyCode.D))
            {
                dis?.Dispose();
                steping = false;
                if (objs.Count > 0)
                {
                    while (objs.Count > 0)
                    {
                        Destroy(objs[0]);
                        objs.RemoveAt(0);
                    }
                }
                objs.Clear();
                Generate();
            }
            return;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            steping = true;
            dis = Observable.Interval(TimeSpan.FromSeconds(1)).TakeUntilDisable(gameObject).Subscribe(_ =>
            {
                Check?.Invoke();
            });
            Debug.Log("START INTERVAL");
        }
    }

    private void Generate()
    {
        for (int x = 0; x < grid.x; x++)
        {
            for (int y = 0; y < grid.y; y++)
            {
                for (int z = 0; z < grid.z; z++)
                {
                    System.Random random = new System.Random();
                    bool r = Convert.ToBoolean(random.Next(-1, 1));
                    AnotherCell cel = Instantiate(cellPrefab);
                    cel.transform.position = new Vector3(x, y, z) * (cel.transform.localScale.x * 1.05f);
                    if (r)
                    {
                        Debug.Log("FALSE");
                        cel.GetComponent<MeshRenderer>().enabled = false;
                    }
                    else
                    {
                        Debug.Log(r);
                    }
                    objs.Add(cel.gameObject);
                }
            }
        }
    }
}

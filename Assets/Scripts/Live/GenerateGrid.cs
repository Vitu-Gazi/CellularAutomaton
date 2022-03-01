using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UniRx;

public class GenerateGrid : Singleton<GenerateGrid>
{
    [SerializeField] private MeshRenderer cubePrefab;

    [SerializeField] private GameObject parent;

    [SerializeField] private List<MeshRenderer> cells;

    [SerializeField] private Vector3 grid;

    [SerializeField] private int[] birth;
    [SerializeField] private int[] isLive;

    private bool steping = false;

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
            return;
        }
        if (Input.GetMouseButtonDown(1))
        {
            steping = true;
            Observable.Interval(TimeSpan.FromSeconds(1)).TakeUntilDisable(gameObject).Subscribe(_ => 
            {
                Check?.Invoke();
            });
            Debug.Log("START INTERVAL");
        }
    }

    private void Generate()
    {
        Vector3 startSpawnPos = Vector3.zero - new Vector3((grid.x / 2), (grid.y / 2), 0);

        int i = 0;

        for (int x = 0; x < grid.x; x++)
        {
            for (int y = 0; y < grid.y; y++)
            {
                MeshRenderer cube = Instantiate(cubePrefab);
                cube.material.color = Color.white;
                cells.Add(cube);
                cube.transform.position = startSpawnPos + (new Vector3(x, y, 0) * (cubePrefab.transform.localScale.x * 1.05f));

                string n = i + " " + x + y;
                cube.GetComponent<CellController>().color = new PrefsValue<int>(n, 0);
                i++;
            }
        }
    }

    private void GenerateCube()
    {
        Vector3 startSpawnPos = Vector3.zero - new Vector3((grid.x / 2), (grid.y / 2), 0);

        int i = 0;

        for (int x = 0; x < grid.x; x++)
        {
            for (int y = 0; y < grid.y; y++)
            {
                for (int z = 0; z < grid.z; z++)
                {
                    if ((y == 0 || y == grid.y - 1) || (x == 0 || x == grid.x - 1))
                    {
                        MeshRenderer cube = Instantiate(cubePrefab);
                        cube.material.color = Color.white;
                        cells.Add(cube);
                        cube.transform.SetParent(parent.transform);
                        cube.transform.localPosition = startSpawnPos + (new Vector3(x, y, z) * (cubePrefab.transform.localScale.x * 1.05f));

                        string n = i + " " + x + y + z;
                        cube.GetComponent<CellController>().color = new PrefsValue<int>(n, 0);
                        i++;
                    }
                    else if ((x != 0 || x != grid.x - 1) && (z == 0 || z == grid.z - 1))
                    {
                        MeshRenderer cube = Instantiate(cubePrefab);
                        cube.material.color = Color.white;
                        cells.Add(cube);
                        cube.transform.SetParent(parent.transform);
                        cube.transform.localPosition = startSpawnPos + (new Vector3(x, y, z) * (cubePrefab.transform.localScale.x * 1.05f));

                        string n = i + " " + x + y + z;
                        cube.GetComponent<CellController>().color = new PrefsValue<int>(n, 0);
                        i++;
                    }
                }
            }
        }
    }
    
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;

public class CellController : MonoBehaviour
{
    [SerializeField] private List<CellController> neighbors;
    [SerializeField] private MeshRenderer mesh;

    [SerializeField] private Color lastColor;

    public PrefsValue<int> color; 

    private void Start()
    {
        GenerateGrid.Instanse.Check += CheckNeighbors;

        if (color.Value == 1)
        {
            mesh.material.color = Color.black;
        }
        else
        {
            mesh.material.color = Color.white;
        }

        lastColor = mesh.material.color;

        Collider[] colls = Physics.OverlapSphere(transform.position, transform.localScale.x * 1.5f);

        foreach (Collider coll in colls)
        {
            if (coll.gameObject != gameObject && coll.gameObject.TryGetComponent(out CellController cell))
            {
                if (cell.transform.position.x == gameObject.transform.position.x)
                {
                    neighbors.Add(cell);
                }
                else if (cell.transform.position.z == gameObject.transform.position.z)
                {
                    neighbors.Add(cell);
                }
                else if (cell.transform.position.y == gameObject.transform.position.y)
                {
                    neighbors.Add(cell);
                }
            }
        }
    }
    private void OnMouseDown()
    {
        if (mesh.material.color == Color.white)
        {
            mesh.material.color = Color.black;
            lastColor = Color.black;
            color.Value = 1;
        }
        else if (mesh.material.color == Color.black)
        {
            mesh.material.color = Color.white;
            lastColor = Color.white;
            color.Value = 0;
        }
    }

    private void OnDisable()
    {
        GenerateGrid.Instanse.Check -= CheckNeighbors;
    }

    private void CheckNeighbors()
    {
        int activeNeyghbors = 0;

        if (mesh.material.color == Color.white)
        {
            foreach (CellController cell in neighbors)
            {
                if (cell.lastColor == Color.black)
                {
                    activeNeyghbors++;
                }
            }

            foreach (int i in GenerateGrid.Instanse.Birth)
            {
                if (i == activeNeyghbors)
                {
                    mesh.material.color = Color.black;
                    color.Value = 1;
                    break;
                }
            }
        }

        else if (mesh.material.color == Color.black)
        {
            foreach (CellController cell in neighbors)
            {
                if (cell.lastColor == Color.black)
                {
                    activeNeyghbors++;
                }
            }

            foreach (int i in GenerateGrid.Instanse.IsLive)
            {
                if (i == activeNeyghbors)
                {
                    mesh.material.color = Color.black;
                    color.Value = 1;
                    break;
                }
                else
                {
                    mesh.material.color = Color.white;
                    color.Value = 0;
                }
            }
        }

        Observable.Timer(TimeSpan.FromSeconds(0.5f)).TakeUntilDisable(gameObject).Subscribe(_ => { lastColor = mesh.material.color; });
    }

    private void MursMethod()
    {
        Collider[] colls = Physics.OverlapSphere(transform.position, transform.localScale.x * 1.5f);

        foreach (Collider coll in colls)
        {
            if (coll.gameObject != gameObject && coll.gameObject.TryGetComponent(out CellController cell))
            {
                if (cell.transform.position.y == gameObject.transform.position.y && cell.transform.position.x == gameObject.transform.position.x)
                {
                    neighbors.Add(cell);
                }
                else if (cell.transform.position.z == gameObject.transform.position.z && cell.transform.position.x == gameObject.transform.position.x)
                {
                    neighbors.Add(cell);
                }
                else if (cell.transform.position.z == gameObject.transform.position.z && cell.transform.position.y == gameObject.transform.position.y)
                {
                    neighbors.Add(cell);
                }
            }
        }
    }
}

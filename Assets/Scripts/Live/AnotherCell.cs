using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class AnotherCell : MonoBehaviour
{
    [SerializeField] private MeshRenderer cube;

    [SerializeField] private List<CellController> neighbors;

    [SerializeField] private bool isActive;

    private void Awake()
    {
        AnotherGrid.Instanse.Check += CheckNeighbors;

        isActive = cube.enabled;

    }


    private void OnDisable()
    {
        AnotherGrid.Instanse.Check -= CheckNeighbors;
    }

    private void CheckNeighbors()
    {
        int activeNeyghbors = 0;

        Collider[] colls = Physics.OverlapSphere(transform.position, transform.localScale.x * 1.5f);

        isActive = cube.enabled;


        foreach (Collider coll in colls)
        {
            if (coll.gameObject.TryGetComponent(out AnotherCell cell) && cell.isActive && coll.gameObject != gameObject)
            {
                activeNeyghbors++;
            }
        }


        if (!cube.enabled)
        {
            foreach (int i in AnotherGrid.Instanse.Birth)
            {
                if (activeNeyghbors == i)
                {
                    cube.enabled = true;
                    break;
                }
            }
        }
        else
        {
            foreach (int i in AnotherGrid.Instanse.IsLive)
            {
                if (activeNeyghbors == i)
                {
                    break;
                }
                else
                {
                    cube.enabled = false;
                }
            }
        }

        Observable.Timer(TimeSpan.FromSeconds(0.5f)).TakeUntilDisable(gameObject).Subscribe(_ => { isActive = cube.enabled; });
    }
}

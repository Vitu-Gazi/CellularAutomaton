using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestNeuron : MonoBehaviour
{
    [SerializeField] private GameObject cube;

    private Neuron neuron = new Neuron();

    private void Start()
    {

    }

    int i = 0;

    private void Update()
    {
        if (neuron.LastError < -neuron.Smooth || neuron.LastError > neuron.Smooth)
        {
            i++;
            cube.transform.position = Vector3.zero;

            neuron.Teaching(new Vector3(0, 17.9f, 0));
            Debug.Log("Ошибка вычислений " + neuron.LastError + " количество итераций " + i);
            return;
        }
        if (cube.transform.position.y < -9.17f - -neuron.Smooth || cube.transform.position.y > -9.17f + neuron.Smooth)
        {
            Debug.Log("Правильный ответ");
            cube.transform.position = new Vector3(0, neuron.SetPosition(-9.17f), 0);
        }
    }
}


public class Neuron
{
    private float weight = 100f;
    public float Smooth { get; private set; } = 0.0001f;
    public float LastError { get; set; } = 100;

    private float MultipleMethod(float pos)
    {
        float input = pos * weight;
        return input;
    }

    public void Teaching(Vector3 expectedPosition)
    {
        float currentPos = MultipleMethod(expectedPosition.y);
        LastError = (expectedPosition.y - currentPos) * Smooth;
        weight += LastError;
    }

    public float SetPosition(float input)
    {
        float newPos = input * weight;
        return newPos;
    }
}

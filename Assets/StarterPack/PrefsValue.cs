using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

[Serializable]
public class PrefsValue<T>
{
    // Штука робит, но работает Инициализация просхидит только на Start или Awake. Со временем пофикшу

    public PrefsValue(string name, T value)
    {
        if (!PlayerPrefs.HasKey(name))
        {
            SetValue(value, name);
        }
        else
        {
            this.name = name;
        }
    }

    private string name;
    private T value;

    public T Value 
    {
        get
        {
            return CheckValue<T>();
        }

        set
        {
            SetValue(value, name);
        }
    }

    private void SetValue (T value, string name)
    {
        this.name = name;

        this.value = value;
        PlayerPrefs.SetString(name, Convert.ToString(value));
        return;
    }

    private T CheckValue<T> ()
    {
        if (PlayerPrefs.HasKey(name))
        {
            return (T)Convert.ChangeType(PlayerPrefs.GetString(name), Type.GetTypeCode(typeof(T)));
        }

        return (T)Convert.ChangeType("null", Type.GetTypeCode(typeof(T)));
    }
}

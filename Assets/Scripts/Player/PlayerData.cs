using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Player Data", menuName = "Scriptable Object/Player Data", order = int.MaxValue)]
public class PlayerData : ScriptableObject
{
    [SerializeField]
    private int _speed = 10;
    public int speed
    {
        get
        {
            return _speed;
        }
        set
        {
            _speed = value;
            OnDataChanged();
        }
    }

    [SerializeField]
    private int _coin = 0;
    public int coin
    {
        get
        {
            return _coin;
        }
        set
        {
            _coin = value;
            OnDataChanged();
        }
    }

    public int maxDurability = 100;
    [SerializeField]
    private int _durability = 100;
    public int durability
    {
        get
        {
            return _durability;
        }
        set
        {
            _durability = value;
            OnDataChanged();
        }
    }

    [SerializeField]
    private int _reliability = 0;
    public int reliability
    {
        get
        {
            return _reliability;
        }
        set
        {
            _reliability = value;
            OnDataChanged();
        }
    }

    //inventory
    public int maxCarrotNum = 4;
    [SerializeField]
    private int _carrotNum = 0;
    public int carrotNum
    {
        get
        {
            return _carrotNum;
        }
        set
        {
            _carrotNum = value;
            OnDataChanged();
        }
    }


    private List<Action> listeners = new List<Action>();
    private void OnDataChanged()
    {
        foreach(var listener in listeners)
        {
            listener.Invoke();
        }
    }

    public void AddListener(Action listener)
    {
        listeners.Add(listener);
    }

    public void RemoveListener(Action listener)
    {
        listeners.Remove(listener);
    }
}

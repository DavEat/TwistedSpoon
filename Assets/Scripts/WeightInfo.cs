﻿using UnityEngine;

public enum units
{
    kg,
    hg,
    dag,
    g,
    dg,
    cg,
    mg,

    kl,
    hl,
    dal,
    l,
    dl,
    cl,
    ml
}

public enum meshName
{
    Sphere,
    Cube
}

public class WeightInfo : MonoBehaviour
{
    public bool placeByPlayer;
    private bool upSide;
    private int quantity;
    private string meshName;
    private units unit;
    private Transform parentInventary;
    public float radius;
    public GameObject gameObject;

    public AudioClip soundM, soundW;

    public void Init(int _quantity, string _meshName, units _unit)
    {
        this.quantity = _quantity;
        this.meshName = _meshName.ToString();
        this.unit = _unit;
    }

    public void Init(int _quantity, string _meshName, units _unit, Transform _parentInventary)
    {
        this.quantity = _quantity;
        this.meshName = _meshName.ToString();
        this.unit = _unit;
        this.parentInventary = _parentInventary;
    }

    public void Init(int _quantity, string _meshName, units _unit, Transform _parentInventary, bool _placeByPlayer)
    {
        this.quantity = _quantity;
        this.meshName = _meshName.ToString();
        this.unit = _unit;
        this.parentInventary = _parentInventary;
        this.placeByPlayer = _placeByPlayer;
    }

    //----- Getter Setter -----
    public int GetQuantity()
    {
        return this.quantity;
    }

    public void SetQuantity(int value)
    {
        this.quantity = value;
    }

    public string GetMeshName()
    {
        return this.meshName;
    }

    public void SetMeshName(meshName value)
    {

        this.meshName = value.ToString();
    }

    public units GetUnit()
    {
        return this.unit;
    }

    public void SetUnit(units value)
    {
        this.unit = value;
    }

    public bool GetUpSide()
    {
        return this.upSide;
    }

    public void SetUpSide(bool value)
    {
        this.upSide = value;
    }

    public Transform GetParentInventory()
    {
        return this.parentInventary;
    }

    public void SetParentInventory(Transform value)
    {
        this.parentInventary = value;
    }

    public float GetRadius()
    {
        return this.radius;
    }

    public void SetrRadius(float value)
    {
        this.radius = value;
    }

    public AudioClip GetSound(bool metal)
    {
        if (metal)
            return soundM;
        else return soundW;
    }
}

public class WeightJustInfo
{
    public int quantity;
    public string meshName;
    public units unit;

    public WeightJustInfo(int _quantity, string _meshName, units _unit)
    {
        this.quantity = _quantity;
        this.meshName = _meshName.ToString();
        this.unit = _unit;
    }
}


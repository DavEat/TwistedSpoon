using UnityEngine;

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

public class WeightInfo
{
    private int quantity;
    private string meshName;
    private units unit;

    public WeightInfo(int _quantity, meshName _meshName, units _unit)
    {
        this.quantity = _quantity;
        this.meshName = _meshName.ToString();
        this.unit = _unit;
    }

    //----- Getter Setter -----
    public int GetWeight()
    {
        return this.quantity;
    }

    public void SetWeight(int value)
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
}

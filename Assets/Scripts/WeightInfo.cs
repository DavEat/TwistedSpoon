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

public class WeightInfo : MonoBehaviour
{
    private bool upSide;
    private int quantity;
    private string meshName;
    private units unit;

    private bool falling;

    void Update()
    {
        if (falling)
        {
            /**/
                
        }
    }

    public void Init(int _quantity, meshName _meshName, units _unit)
    {
        this.quantity = _quantity;
        this.meshName = _meshName.ToString();
        this.unit = _unit;
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
}


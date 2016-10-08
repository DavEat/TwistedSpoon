using UnityEngine;

public class WeightObjectInfo : MonoBehaviour {

    private WeightInfo weightInfo;

    public WeightInfo GetWeightInfo()
    {
        return this.weightInfo;
    }

    public void SetWeightInfo(WeightInfo value)
    {
        this.weightInfo = value;
    }
}

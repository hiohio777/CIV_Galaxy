using UnityEngine;

public class ValueChangeEffectFactory : BaseFactory 
{
    private IGalaxyUITimer _galaxyUITimer;

    private void Start()
    {
        this._galaxyUITimer = GetRegisterObject<IGalaxyUITimer>();
    }

    public ValueChangeEffect GetEffect()
    {
        ValueChangeEffect buffParam;

        if (buffer.Count > 0) buffParam = buffer.Pop() as ValueChangeEffect;
        else 
        {
            buffParam = InstantiateObject<ValueChangeEffect>("ValueChangeEffect");
            buffParam.Creat(Buffered, _galaxyUITimer);
        }
        
        return buffParam;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IPhone
{
    public IPhone()
    {
        Debug.Log("产生Iphone");
    }
}

public class IPhone8 : IPhone
{
    public IPhone8()
    {
        Debug.Log("产生Iphone8");
    }
}

public class IPhoneX : IPhone
{
    public IPhoneX()
    {
        Debug.Log("产生IphoneX");
    }
}

public interface IFactory
{
    IPhone CreateIPhone();
}

public class FactoryIPhone8 : IFactory
{
    public IPhone CreateIPhone()
    {
        return new IPhone8();
    }
}

public class FactoryIPhoneX : IFactory
{
    public IPhone CreateIPhone()
    {
        return new IPhoneX();
    }
}

public class DPFactory : MonoBehaviour
{
    void Start()
    {
        FactoryIPhone8 factoryIPhone8 = new FactoryIPhone8();
        factoryIPhone8.CreateIPhone();
        FactoryIPhoneX factoryIPhoneX = new FactoryIPhoneX();
        factoryIPhoneX.CreateIPhone();
    }
}

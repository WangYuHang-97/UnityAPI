using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DPMediator : MonoBehaviour
{
    //中介提供信息,耦合性低

    // Start is called before the first frame update
    void Start()
    {
        Matchmaker man = new Man(45,100000000,99999999,0);
        Matchmaker woman = new Woman(22,0,0,0);

        WomanMatchmakerMediator  womanMatchmakerMediator = new WomanMatchmakerMediator(man,woman);
        womanMatchmakerMediator.OfferManInformation2Woman();
        womanMatchmakerMediator.OfferWomanInformation2Man();
        print("男方目前好感度:"+ man.m_favor);
        print("方目前好感度:" + woman.m_favor);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

public class WomanMatchmakerMediator
{
    private Matchmaker m_man;
    private Matchmaker m_woman;

    public WomanMatchmakerMediator(Matchmaker man,Matchmaker woman)
    {
        m_man = man;
        m_woman = woman;
    }

    public void OfferWomanInformation2Man()
    {
        m_man.m_favor += -m_woman.m_age + m_woman.m_familyBG + m_woman.m_money;
    }

    public void OfferManInformation2Woman()
    {
        m_woman.m_favor += -m_man.m_age + m_man.m_familyBG + m_man.m_money;
    }
}

public abstract class Matchmaker
{
    public int m_age;
    public int m_money;
    public int m_familyBG;
    public int m_favor;

    public Matchmaker(int age, int money, int familyBg, int favor)
    {
        m_age = age;
        m_money = money;
        m_familyBG = familyBg;
        m_favor = favor;
    }

}

public class Man : Matchmaker
{
    public Man(int age, int money, int familyBg, int favor) : base(age, money, familyBg, favor)
    {
    }
}


public class Woman : Matchmaker
{
    public Woman(int age, int money, int familyBg, int favor) : base(age, money, familyBg, favor)
    {
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainOfResponsibility : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("前面有个小姐姐");
        GrilType grilType = GrilType.AYi;
        BaseHandler handlerA = new HandleAYi();
        BaseHandler handlerB = new HandleLuoLi();
        BaseHandler handlerC = new HandleShaoNv();
        handlerA.SetNextHandler(handlerB).SetNextHandler(handlerC).SetNextHandler(handlerA);
        handlerC.Handle(grilType);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

public enum GrilType
{
    LuoLi,
    ShaoNv,
    YuJie,
    AYi
}

public abstract class BaseHandler
{
    protected BaseHandler nextHandler = null;

    public BaseHandler SetNextHandler(BaseHandler baseHandler)
    {
        nextHandler = baseHandler;
        return nextHandler;
    }

    public virtual void Handle(GrilType grilType)
    {

    }
}

class HandleAYi:BaseHandler
{
    public override void Handle(GrilType grilType)
    {
        if (grilType == GrilType.AYi)
        {
            Debug.Log("阿姨,您长得真漂亮");
            Debug.Log("小伙子,说什么大实话");
        }
        else
        {
            if (nextHandler!=null)
            {
                nextHandler.Handle(grilType);
            }
        }
    }
}
class HandleLuoLi : BaseHandler
{
    public override void Handle(GrilType grilType)
    {
        if (grilType == GrilType.LuoLi)
        {
            Debug.Log("小朋友真可爱");
            Debug.Log("怪蜀黍");
        }
        else
        {
            if (nextHandler != null)
            {
                nextHandler.Handle(grilType);
            }
        }
    }
}

class HandleShaoNv: BaseHandler
{
    public override void Handle(GrilType grilType)
    {
        if (grilType == GrilType.ShaoNv)
        {
            Debug.Log("小姐姐");
            Debug.Log("小哥哥");
        }
        else
        {
            if (nextHandler != null)
            {
                nextHandler.Handle(grilType);
            }
        }
    }
}
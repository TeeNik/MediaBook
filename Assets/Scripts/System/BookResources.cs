using UnityEngine;

public class BookResources
{
    public T Get<T>(string name) where T : Object 
    {
        var obj = Resources.Load<T>("BookResorces/" + name);
        Assert.Inv(obj != null, "obj != null", name);
        return obj;
    }
}

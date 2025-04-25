public interface IBaseData 
{
    public void LoadDatas();
}

public abstract class BaseData<T> : IBaseData
{
    public Dictionary<string,T> datas;

    public T GetData(string key)
    {
        return datas[key];
    }

    public abstract void LoadDatas();
}


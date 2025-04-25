public class Datas<T>
{
    protected List<T> datas;

    public Datas() 
    {
        datas = new List<T>();
    }

    public T GetData(int key)
    {
        return datas[key];
    }

    public List<T> GetDatas() 
    {
        return datas;
    }

    public void AddData(T data) 
    {
        datas.Add(data);
    }
}


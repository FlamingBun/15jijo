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

    public List<T> GetData(string str)
    {
        string[]splitStr = str.Split(',');
        List<T> dataList = new ();
        foreach (string s in splitStr)
        {
            if (s == "")
            {
                continue;
            }
            else if (int.TryParse(s, out int index))
            {
                dataList.Add(datas[index]);
            }
            
        }
        return dataList;
    }

}


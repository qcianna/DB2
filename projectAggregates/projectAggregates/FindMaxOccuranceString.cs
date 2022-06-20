using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;
using System.Runtime.InteropServices;
using System.Collections.Generic;

[Serializable]
[Microsoft.SqlServer.Server.SqlUserDefinedAggregate(
Format.UserDefined,
IsNullIfEmpty = true,
MaxByteSize = 8000)]
[StructLayout(LayoutKind.Sequential)]
public struct FindMaxOccuranceString: IBinarySerialize
{
    private List<string> list;

    public void Init()
    {
        list = new List<string>();
    }

    public void Accumulate(SqlString Value)
    {
        if (!Value.IsNull)
        {
            list.Add(Value.Value);
        }
    }

    public void Merge(FindMaxOccuranceString Group)
    {
        list.InsertRange(list.Count, Group.list);
    }

    public SqlString Terminate()
    {
        List<string> cities = new List<string>();
        List<int> citiesCount = new List<int>();
        foreach (string v in list)
        {
            if (!cities.Contains(v))
            {
                cities.Add(v);
                citiesCount.Add(0);
            }
            int index = cities.IndexOf(v);
            citiesCount[index]++;
        }
        
        int indexMax = 0;
        for (int i = 0; i < cities.Count; i ++)
        {
            if (citiesCount[i] > citiesCount[indexMax])
            {
                indexMax = i;
            }
        }

        string cityMax = cities[indexMax];

        return cityMax;
    }

    public void Read(System.IO.BinaryReader r)
    {
        list = new List<string>();
        int j = r.ReadInt32();
        for (int i = 0; i < j; i++)
        {
            list.Add(r.ReadString());
        }
    }

    public void Write(System.IO.BinaryWriter w)
    {
        w.Write(list.Count);
        foreach (string d in list)
        {
            w.Write(d);
        }
    }
}

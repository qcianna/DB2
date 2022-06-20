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
public struct CountUniqueStrings: IBinarySerialize
{
    private List<string> cities;

    public void Init()
    {
        cities = new List<string>();
    }

    public void Accumulate(SqlString Value)
    {
        if (!Value.IsNull && !cities.Contains(Value.Value))
        {
            cities.Add(Value.Value);
        }
    }

    public void Merge(CountUniqueStrings Group)
    {
        cities.InsertRange(cities.Count, Group.cities);
    }

    public SqlInt32 Terminate()
    {
        return cities.Count;
    }

    public void Read(System.IO.BinaryReader r)
    {
        cities = new List<string>();
        int j = r.ReadInt32();
        for (int i = 0; i < j; i++)
        {
            cities.Add(r.ReadString());
        }
    }

    public void Write(System.IO.BinaryWriter w)
    {
        w.Write(cities.Count);
        foreach (string d in cities)
        {
            w.Write(d);
        }
    }
}

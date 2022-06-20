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
public struct FindShortestString : IBinarySerialize
{
    private List<string> logins;

    public void Init()
    {
        logins = new List<string>();
    }

    public void Accumulate(SqlString Value)
    {
        if (!Value.IsNull)
        {
            logins.Add(Value.Value);
        }
    }

    public void Merge(FindShortestString Group)
    {
        logins.InsertRange(logins.Count, Group.logins);
    }

    public SqlString Terminate()
    {
        int indexMin = 0;
        foreach (string login in logins)
        {
            if (logins[indexMin].Length > login.Length)
            {
                indexMin = logins.IndexOf(login);
            }
        }
        return logins[indexMin];
    }

    public void Read(System.IO.BinaryReader r)
    {
        logins = new List<string>();
        int j = r.ReadInt32();
        for (int i = 0; i < j; i++)
        {
            logins.Add(r.ReadString());
        }
    }

    public void Write(System.IO.BinaryWriter w)
    {
        w.Write(logins.Count);
        foreach (string d in logins)
        {
            w.Write(d);
        }
    }
}

using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Text;
using Microsoft.SqlServer.Server;
using System.Runtime.InteropServices;
using System.Collections.Generic;

[Serializable]
[Microsoft.SqlServer.Server.SqlUserDefinedAggregate(
Format.UserDefined,
IsNullIfEmpty = true,
MaxByteSize = 8000)]
public struct ConnectStrings : IBinarySerialize
{
    private StringBuilder joinedText;

    public void Init()
    {
        joinedText = new StringBuilder(null);
    }

    public void Accumulate(SqlString Value)
    {
        if (!Value.IsNull)
        {
            joinedText.AppendFormat("{0};", Value.Value);
        }
    }

    public void Merge(ConnectStrings Group)
    {
        joinedText.Append(Group.joinedText);
    }

    public SqlString Terminate()
    {
        return joinedText.ToString();
    }

    public void Read(System.IO.BinaryReader r)
    {
        joinedText = new StringBuilder(r.ReadString());
    }

    public void Write(System.IO.BinaryWriter w)
    {
        w.Write(joinedText.ToString());
    }
}

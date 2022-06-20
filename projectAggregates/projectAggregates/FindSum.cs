using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;


[Serializable]
[Microsoft.SqlServer.Server.SqlUserDefinedAggregate(Format.Native)]
public struct FindSum
{
    private int sum;

    public void Init()
    {
        sum = 0;
    }

    public void Accumulate(SqlInt32 Value)
    {
        if (!Value.IsNull)
        {
            sum += Value.Value;
        }
    }

    public void Merge(FindSum Group)
    {
        sum += Group.sum;
    }

    public SqlInt32 Terminate()
    {
        return sum;
    }
}

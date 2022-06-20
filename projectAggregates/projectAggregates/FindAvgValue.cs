using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;


[Serializable]
[Microsoft.SqlServer.Server.SqlUserDefinedAggregate(Format.Native)]
public struct FindAvgValue
{
    private double count;
    private double sum;

    public void Init()
    {
        count = 0;
        sum = 0;
    }

    public void Accumulate(SqlInt32 Value)
    {
        if (!Value.IsNull)
        {
            count++;
            sum += Value.Value;
        }
    }

    public void Merge(FindAvgValue Group)
    {
        count += Group.count;
        sum += Group.sum;
    }

    public SqlDouble Terminate()
    {
        return sum/count;
    }
}

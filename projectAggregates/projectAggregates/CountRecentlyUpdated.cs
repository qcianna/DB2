using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;


[Serializable]
[Microsoft.SqlServer.Server.SqlUserDefinedAggregate(Format.Native)]
public struct CountRecentlyUpdated
{
    private int count;

    public void Init()
    {
        count = 0;
    }

    public void Accumulate(SqlDateTime Value)
    {
        DateTime d1 = DateTime.Now;
        TimeSpan time = d1 - Value.Value;
        double years = time.Days / 365.0;
        if (!Value.IsNull && years < 20)
        {
            count++;
        }
    }

    public void Merge(CountRecentlyUpdated Group)
    {
        count += Group.count;
    }

    public SqlInt32 Terminate()
    {
        return count;
    }
}

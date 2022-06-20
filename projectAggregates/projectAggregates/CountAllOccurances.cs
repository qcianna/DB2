using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;


[Serializable]
[Microsoft.SqlServer.Server.SqlUserDefinedAggregate(Format.Native)]
public struct CountAllOccurances
{
    private int count;

    public void Init()
    {
        count = 0;
    }

    public void Accumulate(SqlString Value)
    {
        string jobTitle = "Design Engineer";
        if (!Value.IsNull && Value.Value.Equals(jobTitle))
        {
            count++;
        }
    }

    public void Merge(CountAllOccurances Group)
    {
        count += Group.count;
    }

    public SqlInt32 Terminate()
    {
        return count;
    }
}

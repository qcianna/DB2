using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;


[Serializable]
[Microsoft.SqlServer.Server.SqlUserDefinedAggregate(Format.Native)]
public struct FindOddEvenDiff
{
    private int countOdd;
    private int countEven;

    public void Init()
    {
        countOdd = 0;
        countEven = 0;
    }

    public void Accumulate(SqlInt32 Value)
    {
        if (!Value.IsNull)
        {
            if (Value.Value % 2 == 0)
            {
                countEven++;
            }
            else
            {
                countOdd++;
            }
        }
    }

    public void Merge(FindOddEvenDiff Group)
    {
        countOdd += Group.countOdd;
        countEven += Group.countEven;
    }

    public SqlInt32 Terminate()
    {
        return countOdd < countEven ? countEven - countOdd : countOdd - countEven;
    }
}

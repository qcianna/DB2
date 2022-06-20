using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace projekt1
{
    class Program
    {
        private static SqlConnection conn;

        public static void connect()
        {
            string conString = "DATA SOURCE=MSSQLSERVER58;" + "INITIAL CATALOG=AdventureWorks2008; INTEGRATED SECURITY=SSPI;";
            conn = new SqlConnection(conString);
            conn.Open();
        }

        public static void giveInstruction()
        {
            string instruction = @"                              
 Wybierz agregat:  
    1 - zlicza ilość pojawiających się w bazie unikalnych nazw miast 
    2 - zwraca informację ile jest adresów, których ostatnia aktualizacja 
        była mniej niż 20 lat temu
    3 - zwraca nazwę miasta, które jest najczęściej zamieszkiwane
    4 - zwraca sumę wykorzystanych wolnych dni chorobowych
        przez pracowników
    5 - zwraca średnią ilość wolnych dni chorobowych wziętą przez jednego
        pracownika
    6 - zwraca ilość osób pracujących na stanowisku 'Design Engineer'
    7 - zwraca najkrótszy login występujący wśród pracowników
    8 - zwraca różnicę pomiędzy ilością liczb nieparzystych, a parzystych
    9 - zwraca połączony ciąg znaków określających płeć pracowników (M lub K) podzielonych znakiem ';'
    q - wyjście           
               ";
            System.Console.WriteLine(instruction);
        }

        static void Main(string[] args)
        {
            giveInstruction();
            
            char key = System.Console.ReadKey().KeyChar;

            string aggr = null;
            string arg = null;
            switch (key)
            {
                case '1':
                    aggr = "CountUniqueStrings";
                    arg = "City";
                    break;
                case '2':
                    aggr = "CountRecentlyUpdated";
                    arg = "ModifiedDate";
                    break;
                case '3':
                    aggr = "FindMaxOccuranceString";
                    arg = "City";
                    break;
                case '4':
                    aggr = "FindSum";
                    arg = "SickLeaveHours";
                    break;
                case '5':
                    aggr = "FindAvgValue";
                    arg = "SickLeaveHours";
                    break;
                case '6':
                    aggr = "CountAllOccurances";
                    arg = "JobTitle";
                    break;
                case '7':
                    aggr = "FindShortestString";
                    arg = "LoginID";
                    break;
                case '8':
                    aggr = "FindOddEvenDiff";
                    arg = "NationalIDNumber";
                    break;
                case '9':
                    aggr = "ConnectStrings";
                    arg = "Gender";
                    break;
                default:
                    break;
            }

            if (key.Equals('q'))
            {
                Console.WriteLine("Exit");
            }
            else if (aggr == null)
            {
                Console.WriteLine("Wrong command");
            }
            else
            {
                connect();
                try
                {
                    string commandString = null;
                    if (key == '1' || key == '2' || key == '3')
                    {
                        commandString = "SELECT dbo." + aggr + "(" + arg + ") FROM (SELECT TOP 100 * FROM [Person].[Address]) AS res";
                    }
                    else
                    {
                        commandString = "SELECT dbo." + aggr + "(" + arg + ") FROM (SELECT TOP 100 * FROM [HumanResources].[Employee]) AS res";
                    }
                    SqlCommand command = new SqlCommand(commandString, conn);
                    SqlDataReader datareader = command.ExecuteReader();
                    datareader.Read();

                    Console.WriteLine("\nOtrzymany wynik:\n" + datareader[0]);
                }
                finally
                {
                    conn.Close();
                }
            }
        }
    }
}

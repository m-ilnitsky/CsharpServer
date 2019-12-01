using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L3_Task2_Transaction
{
    class ProgramForTransactions
    {
        private static void AddPairOfCategories(SqlConnection connection, string category1Name, string category2Name, bool isError)
        {
            const string sql = @"INSERT INTO [BookShop].[dbo].[Category] ([Name]) 
                                 VALUES (@CategoryName)";

            try
            {
                using (var command = new SqlCommand(sql, connection))
                {
                    command.Parameters.Add(new SqlParameter("@CategoryName", category1Name) { SqlDbType = SqlDbType.NVarChar });
                    command.ExecuteNonQuery();
                    Console.WriteLine("Добавлена категория [ {0} ]", category1Name);
                }

                if (isError)
                {
                    throw new Exception("Is error!");
                }

                using (var command = new SqlCommand(sql, connection))
                {
                    command.Parameters.Add(new SqlParameter("@CategoryName", category2Name) { SqlDbType = SqlDbType.NVarChar });
                    command.ExecuteNonQuery();
                    Console.WriteLine("Добавлена категория [ {0} ]", category2Name);
                }

            }
            catch (Exception)
            {
                Console.WriteLine("Пара операций [ {0}, {1} ] прервалась!", category1Name, category2Name);
            }
        }

        private static void AddPairOfCategoriesByTransaction(SqlConnection connection, string category1Name, string category2Name, bool isError)
        {
            const string sql = @"INSERT INTO [BookShop].[dbo].[Category] ([Name]) 
                                 VALUES (@CategoryName)";

            var transaction = connection.BeginTransaction();

            try
            {
                var command1 = new SqlCommand(sql, connection);
                command1.Transaction = transaction;
                command1.Parameters.Add(new SqlParameter("@CategoryName", category1Name) { SqlDbType = SqlDbType.NVarChar });
                command1.ExecuteNonQuery();

                if (isError)
                {
                    throw new Exception("Is error!");
                }

                var command2 = new SqlCommand(sql, connection);
                command2.Transaction = transaction;
                command2.Parameters.Add(new SqlParameter("@CategoryName", category2Name) { SqlDbType = SqlDbType.NVarChar });
                command2.ExecuteNonQuery();

                transaction.Commit();

                Console.WriteLine("Транзакция [ {0}, {1} ] прошла!", category1Name, category2Name);
            }
            catch (Exception)
            {
                transaction.Rollback();
                Console.WriteLine("Транзакция [ {0}, {1} ] откатилась!", category1Name, category2Name);
            }
        }

        static void Main(string[] args)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            Console.WriteLine("Connection string = " + connectionString);

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                Console.WriteLine("Connection state = " + connection.State);


                AddPairOfCategories(connection, "AF1", "AF2", false);
                AddPairOfCategories(connection, "AT1", "AT2", true);

                AddPairOfCategoriesByTransaction(connection, "BF1", "BF2", false);
                AddPairOfCategoriesByTransaction(connection, "BT1", "BT2", true);

                Console.ReadKey();
            }
        }
    }
}

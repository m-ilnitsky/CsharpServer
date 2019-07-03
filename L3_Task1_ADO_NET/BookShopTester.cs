using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L3_Task1_ADO_NET
{
    internal class BookShopTester
    {
        private static int GetGoodsCount(SqlConnection connection)
        {
            const string sql = "SELECT COUNT([Name]) FROM [Goods]";

            int count;

            using (var command = new SqlCommand(sql, connection))
            {
                count = (int)command.ExecuteScalar();
            }

            return count;
        }

        private static int GetGoodsCount(SqlConnection connection, string category)
        {
            const string sql = "SELECT COUNT([Goods].[Name]) FROM [Goods], [Category] WHERE [Category].[Name]=@CategoryName AND [Category].[ID]=[Goods].[CategoryID]";

            int count;

            using (var command = new SqlCommand(sql, connection))
            {
                command.Parameters.Add(new SqlParameter("@CategoryName", category) { SqlDbType = SqlDbType.NVarChar });
                count = (int)command.ExecuteScalar();
            }

            return count;
        }

        private static void PrintAllGoodsByReader(SqlConnection connection)
        {
            const string sql = "SELECT [Goods].[Name], [Goods].[Price], [Category].[Name] AS Category FROM [Goods], [Category] WHERE [Category].[ID]=[Goods].[CategoryID]";

            using (var command = new SqlCommand(sql, connection))
            {
                using (var reader = command.ExecuteReader())
                {
                    const string pattern = "{0,2}{1} {2,-20} {3,-40} {4,8}";
                    Console.WriteLine(pattern, "№", " ", reader.GetName(2), reader.GetName(0), reader.GetName(1));

                    var count = 0;
                    while (reader.Read())
                    {
                        count++;
                        Console.WriteLine(pattern, count, ".", reader[2], reader[0], reader[1]);
                    }
                }
            }
        }

        private static void PrintAllGoodsByAdapter(SqlConnection connection)
        {
            const string sql = "SELECT [Goods].[Name], [Goods].[Price], [Category].[Name] AS Category FROM [Goods], [Category] WHERE [Category].[ID]=[Goods].[CategoryID]";

            using (var adapter = new SqlDataAdapter(sql, connection))
            {
                var dataSet = new DataSet();

                adapter.Fill(dataSet);

                var dataTable = dataSet.Tables[0];
                var dataRows = dataTable.Rows;
                var dataColumns = dataTable.Columns;

                var columnsName = new string[dataColumns.Count];

                for (var i = 0; i < dataColumns.Count; i++)
                {
                    columnsName[i] = dataColumns[i].ColumnName;
                }

                for (var i = 0; i < dataRows.Count; i++)
                {
                    for (var j = 0; j < columnsName.Length; j++)
                    {
                        Console.Write("{0} = {1,-24} ", columnsName[j], dataRows[i][j]);
                    }

                    Console.WriteLine();
                }
            }
        }

        private static bool IsCategory(SqlConnection connection, string categoryName)
        {
            const string sql = "SELECT COUNT([Category].[ID]) FROM [Category] WHERE [Category].[Name]=@CategoryName";

            int count;

            using (var command = new SqlCommand(sql, connection))
            {
                command.Parameters.Add(new SqlParameter("@CategoryName", categoryName) { SqlDbType = SqlDbType.NVarChar });
                count = (int)command.ExecuteScalar();
            }

            if (count > 1)
            {
                throw new Exception("Category '" + categoryName + "' has a duplicate!");
            }

            return count > 0;
        }

        private static int GetCategoryId(SqlConnection connection, string categoryName)
        {
            const string sql = "SELECT [Category].[ID] FROM [Category] WHERE [Category].[Name]=@CategoryName";

            int id;

            using (var command = new SqlCommand(sql, connection))
            {
                command.Parameters.Add(new SqlParameter("@CategoryName", categoryName) { SqlDbType = SqlDbType.NVarChar });

                using (var reader = command.ExecuteReader())
                {
                    reader.Read();

                    id = reader.GetInt32(0);
                }
            }

            return id;
        }

        private static int AddCategory(SqlConnection connection, string categoryName)
        {
            if (IsCategory(connection, categoryName))
            {
                throw new Exception("Category '" + categoryName + "' already exists!");
            }

            const string sql = "INSERT INTO [BookShop].[dbo].[Category] ([Name]) VALUES (@CategoryName)";

            int count;

            using (var command = new SqlCommand(sql, connection))
            {
                command.Parameters.Add(new SqlParameter("@CategoryName", categoryName) { SqlDbType = SqlDbType.NVarChar });
                count = (int)command.ExecuteNonQuery();
            }

            return count;
        }

        private static int AddGoods(SqlConnection connection, string categoryName, string goodsName, decimal price)
        {
            if (!IsCategory(connection, categoryName))
            {
                throw new Exception("No category '" + categoryName + "'!");
            }

            var categoryId = GetCategoryId(connection, categoryName);

            const string sql = "INSERT INTO [BookShop].[dbo].[Goods] ([Name], [Price], [CategoryID]) VALUES(@GoodsName, @Price, @CategoryId)";

            int count;

            using (var command = new SqlCommand(sql, connection))
            {
                command.Parameters.Add(new SqlParameter("@GoodsName", goodsName) { SqlDbType = SqlDbType.NVarChar });
                command.Parameters.Add(new SqlParameter("@Price", price) { SqlDbType = SqlDbType.Decimal });
                command.Parameters.Add(new SqlParameter("@CategoryId", categoryId) { SqlDbType = SqlDbType.Int });
                count = (int)command.ExecuteNonQuery();
            }

            return count;
        }

        private static void Main()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            Console.WriteLine(connectionString);

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                Console.WriteLine("Connection state = " + connection.State);

                var goodsCount = BookShopTester.GetGoodsCount(connection);
                Console.WriteLine("Goods Count = " + goodsCount);

                var booksCount = BookShopTester.GetGoodsCount(connection, "книга");
                Console.WriteLine("Books Count = " + booksCount);

                var newspapersCount = BookShopTester.GetGoodsCount(connection, "газета");
                Console.WriteLine("Newspapers Count = " + newspapersCount);

                BookShopTester.PrintAllGoodsByReader(connection);
                BookShopTester.PrintAllGoodsByAdapter(connection);

                BookShopTester.AddCategory(connection, "ручка");
                BookShopTester.AddCategory(connection, "карандаш");
                BookShopTester.AddCategory(connection, "ластик");

                BookShopTester.AddGoods(connection, "ручка", "Паркер пластмассовый", 4000);
                BookShopTester.AddGoods(connection, "ручка", "Паркер деревянный", 5000);
                BookShopTester.AddGoods(connection, "ластик", "Гнев божий", 20);

                BookShopTester.PrintAllGoodsByReader(connection);
                BookShopTester.PrintAllGoodsByAdapter(connection);
            }
        }
    }
}

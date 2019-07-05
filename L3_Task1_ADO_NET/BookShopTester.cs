using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace L3_Task1_ADO_NET
{
    internal class BookShopTester
    {
        private static int GetGoodsCount(SqlConnection connection)
        {
            const string sql = @"SELECT COUNT([Name])
                                 FROM [Goods]";

            using (var command = new SqlCommand(sql, connection))
            {
                return (int)command.ExecuteScalar();
            }
        }

        private static int GetGoodsCount(SqlConnection connection, string category)
        {
            const string sql = @"SELECT COUNT([Goods].[Name]) 
                                 FROM [Goods]
                                 INNER JOIN [Category]
                                 ON [Category].[Name] = @CategoryName AND [Category].[ID] = [Goods].[CategoryID]";

            using (var command = new SqlCommand(sql, connection))
            {
                command.Parameters.Add(new SqlParameter("@CategoryName", category) { SqlDbType = SqlDbType.NVarChar });
                return (int)command.ExecuteScalar();
            }
        }

        private static void PrintAllGoodsByReader(SqlConnection connection)
        {
            const string sql = @"SELECT [Goods].[Name], [Goods].[Price], [Category].[Name] AS Category 
                                 FROM [Goods]
                                 LEFT JOIN [Category] 
                                 ON [Category].[ID] = [Goods].[CategoryID]";

            using (var command = new SqlCommand(sql, connection))
            {
                using (var reader = command.ExecuteReader())
                {
                    const string pattern = "{0,2}{1} {2,-20} {3,-40} {4,8}";

                    Console.WriteLine();
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
            const string sql = @"SELECT [Goods].[Name], [Goods].[Price], [Category].[Name] AS Category 
                                 FROM [Goods]
                                 LEFT JOIN [Category] 
                                 ON [Category].[ID] = [Goods].[CategoryID]";

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

                Console.WriteLine();

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

        private static bool HasCategory(SqlConnection connection, string categoryName)
        {
            const string sql = @"SELECT COUNT([Category].[ID]) 
                                 FROM [Category] 
                                 WHERE [Category].[Name] = @CategoryName";

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
            const string sql = @"SELECT [Category].[ID] 
                                 FROM [Category] 
                                 WHERE [Category].[Name] = @CategoryName";

            using (var command = new SqlCommand(sql, connection))
            {
                command.Parameters.Add(new SqlParameter("@CategoryName", categoryName) { SqlDbType = SqlDbType.NVarChar });

                using (var reader = command.ExecuteReader())
                {
                    reader.Read();

                    return reader.GetInt32(0);
                }
            }
        }

        private static void AddCategory(SqlConnection connection, string categoryName)
        {
            if (HasCategory(connection, categoryName))
            {
                throw new Exception("Category '" + categoryName + "' already exists!");
            }

            const string sql = @"INSERT INTO [BookShop].[dbo].[Category] ([Name]) 
                                 VALUES (@CategoryName)";

            using (var command = new SqlCommand(sql, connection))
            {
                command.Parameters.Add(new SqlParameter("@CategoryName", categoryName) { SqlDbType = SqlDbType.NVarChar });
                command.ExecuteNonQuery();
            }
        }

        private static void AddGoods(SqlConnection connection, string categoryName, string goodsName, decimal price)
        {
            if (!HasCategory(connection, categoryName))
            {
                throw new Exception("No category '" + categoryName + "'!");
            }

            var categoryId = GetCategoryId(connection, categoryName);

            const string sql = @"INSERT INTO [BookShop].[dbo].[Goods] ([Name], [Price], [CategoryID]) 
                                 VALUES(@GoodsName, @Price, @CategoryId)";

            using (var command = new SqlCommand(sql, connection))
            {
                command.Parameters.Add(new SqlParameter("@GoodsName", goodsName) { SqlDbType = SqlDbType.NVarChar });
                command.Parameters.Add(new SqlParameter("@Price", price) { SqlDbType = SqlDbType.Decimal });
                command.Parameters.Add(new SqlParameter("@CategoryId", categoryId) { SqlDbType = SqlDbType.Int });
                command.ExecuteNonQuery();
            }
        }

        private static int[] GetGoodsId(SqlConnection connection, string categoryName, string goodsName, decimal price)
        {
            const string sql = @"SELECT [Goods].[ID] 
                                 FROM [Goods]
                                 INNER JOIN [Category] 
                                 ON [Category].[ID] = [Goods].[CategoryID] 
                                    AND [Category].[Name] = @CategoryName 
                                    AND [Goods].[Name] = @GoodsName 
                                    AND [Goods].[Price] = @Price";

            using (var command = new SqlCommand(sql, connection))
            {
                command.Parameters.Add(new SqlParameter("@CategoryName", categoryName) { SqlDbType = SqlDbType.NVarChar });
                command.Parameters.Add(new SqlParameter("@GoodsName", goodsName) { SqlDbType = SqlDbType.NVarChar });
                command.Parameters.Add(new SqlParameter("@Price", price) { SqlDbType = SqlDbType.Decimal });

                using (var adapter = new SqlDataAdapter(command))
                {
                    var dataSet = new DataSet();

                    adapter.Fill(dataSet);

                    var dataTable = dataSet.Tables[0];
                    var dataRows = dataTable.Rows;

                    var ids = new int[dataRows.Count];

                    for (var i = 0; i < dataRows.Count; i++)
                    {
                        ids[i] = (int)dataRows[i][0];
                    }

                    return ids;
                }
            }
        }

        private static void DeleteGoods(SqlConnection connection, int goodsId)
        {
            const string sql = @"DELETE FROM [BookShop].[dbo].[Goods] 
                                 WHERE [Goods].[ID] = @GoodsId";

            using (var command = new SqlCommand(sql, connection))
            {
                command.Parameters.Add(new SqlParameter("@GoodsId", goodsId) { SqlDbType = SqlDbType.Int });
                command.ExecuteNonQuery();
            }
        }

        private static void DeleteGoods(SqlConnection connection, string categoryName, string goodsName, decimal price)
        {
            var ids = GetGoodsId(connection, categoryName, goodsName, price);

            Console.WriteLine();

            if (ids.Length < 1)
            {
                Console.WriteLine("Товара заданными параметрами в базе нет!");
                Console.WriteLine("Были заданы следующие параметры:");
                Console.WriteLine("Категория = '{0}', название = '{1}', цена = '{2}'", categoryName, goodsName, price);
                return;
            }

            if (ids.Length > 1)
            {
                Console.WriteLine("Найдено {0} товаров.", ids.Length);
                Console.Write("Найденные ID: ");
                foreach (var id in ids)
                {
                    Console.Write(id + " ");
                }
                Console.WriteLine();
            }

            Console.WriteLine("Будет удалён товар с ID={0}.", ids[0]);

            DeleteGoods(connection, ids[0]);
        }

        private static void UpdateGoods(SqlConnection connection, string currentCategoryName, string currentGoodsName, decimal currentPrice, bool updateAll, string newCategoryName, string newGoodsName, decimal newPrice)
        {
            var goodsIds = GetGoodsId(connection, currentCategoryName, currentGoodsName, currentPrice);

            Console.WriteLine();

            if (goodsIds.Length < 1)
            {
                Console.WriteLine("Товара заданными параметрами в базе нет!");
                Console.WriteLine("Были заданы следующие параметры:");
                Console.WriteLine("Категория = '{0}', название = '{1}', цена = '{2}'", currentCategoryName, currentGoodsName, currentPrice);
                return;
            }

            if (goodsIds.Length > 1)
            {
                Console.WriteLine("Найдено {0} товаров.", goodsIds.Length);
                Console.Write("Найденные ID: ");
                foreach (var id in goodsIds)
                {
                    Console.Write(id + " ");
                }
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("Найден товар с id = {0}.", goodsIds[0]);
            }

            if (currentCategoryName == newCategoryName && currentGoodsName == newGoodsName && currentPrice == newPrice)
            {
                Console.WriteLine("Новые параметры совпадают со старыми - попытка обновления отменена.");
                return;
            }

            var categoryId = 0;
            var sqlCategory = "";
            if (currentCategoryName != newCategoryName)
            {
                if (!HasCategory(connection, newCategoryName))
                {
                    throw new Exception("No category '" + newCategoryName + "'!");
                }

                categoryId = GetCategoryId(connection, newCategoryName);

                sqlCategory = "[Goods].[CategoryID] = @CategoryID";
            }

            var sqlName = "";
            if (currentGoodsName != newGoodsName)
            {
                sqlName = "[Goods].[Name] = @Name";
            }

            var sqlPrice = "";
            if (currentPrice != newPrice)
            {
                sqlPrice = "[Goods].[Price] = @Price";
            }

            var sqlWhere = " WHERE [Goods].[ID] ";
            if (!updateAll || goodsIds.Length == 1)
            {
                Console.WriteLine("Будет изменён товар с ID={0}.", goodsIds[0]);

                sqlWhere += "= " + goodsIds[0] + ";";
            }
            else
            {
                Console.WriteLine("Будут изменены все найденные товары ");

                sqlWhere += "IN (";
                for (var i = 0; i < goodsIds.Length; ++i)
                {
                    sqlWhere += goodsIds[i];
                    if (i + 1 < goodsIds.Length)
                    {
                        sqlWhere += ", ";
                    }
                }
                sqlWhere += ");";
            }

            var sql = "UPDATE [Goods] SET " + sqlCategory;

            if (sqlCategory != "" && sqlName != "")
            {
                sql += ", ";
            }

            sql += sqlName;

            if ((sqlCategory != "" || sqlName != "") && sqlPrice != "")
            {
                sql += ", ";
            }

            sql += sqlPrice + sqlWhere;

            using (var command = new SqlCommand(sql, connection))
            {
                command.Parameters.Add(new SqlParameter("@CategoryID", categoryId) { SqlDbType = SqlDbType.NVarChar });
                command.Parameters.Add(new SqlParameter("@Name", newGoodsName) { SqlDbType = SqlDbType.NVarChar });
                command.Parameters.Add(new SqlParameter("@Price", newPrice) { SqlDbType = SqlDbType.Decimal });
                command.ExecuteNonQuery();
            }
        }

        private static void Main()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            Console.WriteLine(connectionString);

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                Console.WriteLine("Connection state = " + connection.State);

                var goodsCount = GetGoodsCount(connection);
                Console.WriteLine("Goods Count = " + goodsCount);

                var booksCount = GetGoodsCount(connection, "книга");
                Console.WriteLine("Books Count = " + booksCount);

                var newspapersCount = GetGoodsCount(connection, "газета");
                Console.WriteLine("Newspapers Count = " + newspapersCount);

                PrintAllGoodsByAdapter(connection);
                PrintAllGoodsByReader(connection);

                AddCategory(connection, "ручка");
                AddCategory(connection, "карандаш");
                AddCategory(connection, "ластик");

                AddGoods(connection, "ручка", "Паркер пластмассовый", 4000);
                AddGoods(connection, "ручка", "Паркер пластмассовый", 4000);
                AddGoods(connection, "ручка", "Паркер пластмассовый", 4000);
                AddGoods(connection, "ручка", "Паркер пластмассовый", 4000);
                AddGoods(connection, "ручка", "Паркер деревянный", 5000);
                AddGoods(connection, "ручка", "Паркер деревянный", 5000);
                AddGoods(connection, "ручка", "Паркер деревянный", 5000);
                AddGoods(connection, "ручка", "Паркер деревянный", 5000);
                AddGoods(connection, "ластик", "Гнев божий", 20);
                AddGoods(connection, "ластик", "Гнев божий", 20);
                AddGoods(connection, "ластик", "Гнев божий", 20);
                AddGoods(connection, "ластик", "Гнев божий", 20);

                PrintAllGoodsByReader(connection);

                DeleteGoods(connection, "ластик", "Гнев божий", 20);
                DeleteGoods(connection, "ручка", "Паркер деревянный", 5);
                DeleteGoods(connection, "ручка", "Паркер деревянный", 5000);

                PrintAllGoodsByReader(connection);

                UpdateGoods(connection, "ручка", "Паркер деревянный", 5000, true, "карандаш", "Паркер деревянный", 1000);
                UpdateGoods(connection, "ручка", "Паркер пластмассовый", 4000, false, "ручка", "Паркер особенный", 8000);

                PrintAllGoodsByReader(connection);
            }
        }
    }
}

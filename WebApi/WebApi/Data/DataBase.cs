using System;
using System.Collections.Generic;
using System.Data.SQLite;
using WebApi.Models;

namespace WebApi.Data
{
    public static class DataBase
    {
        public static SQLiteConnection CreateConnection()
        {
            var connection = new SQLiteConnection("Data Source=database.db; Version=3; New=True; Compress=True; ");
            try
            {
                connection.Open();
            }
            catch { }
            return connection;
        }

        public static void CreateTables()
        {
            var connection = CreateConnection();
            var command = connection.CreateCommand();
            command.CommandText = @"
            CREATE TABLE IF NOT EXISTS Fugure
            (
                [ID]    INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL
                ,[TYPE] INT NULL
                ,[X]    INT NULL
                ,[Y]    INT NULL
            )"; ;
            command.ExecuteNonQuery();
            connection.Close();
        }

        public static int InsertFigure(FigureType type, int? x, int? y)
        {
            var connection = CreateConnection();
            var command = connection.CreateCommand();
            command.CommandText = $"INSERT INTO Fugure (TYPE, X, Y) VALUES({(int)type}, {(x.HasValue ? x.ToString() : "NULL")}, {(y.HasValue ? y.ToString() : "NULL")}); ";
            command.ExecuteNonQuery();
            var id = (int)connection.LastInsertRowId;
            connection.Close();
            return id;
        }

        public static List<IFigureModel> ReadFigures()
        {
            var result = new List<IFigureModel>();
            var connection = CreateConnection();
            var command = connection.CreateCommand();
            command.CommandText = "SELECT ID, TYPE, X, Y, Z FROM Fugure";

            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                var id = reader.GetInt32(0);
                var type = GetFieldValue<FigureType>(reader, 1);

                switch (type){
                    case FigureType.Circle:
                        var r = GetFieldValue<int?>(reader, 2);
                        result.Add(new CircleModel
                        {
                            ID = id,
                            R = r.GetValueOrDefault(0)
                        }) ;
                        break;

                    case FigureType.Triangle:
                        var h = GetFieldValue<int?>(reader, 2);
                        var a = GetFieldValue<int?>(reader, 3);
                        result.Add(new TriangleModel
                        {
                            ID = id,
                            H = h.GetValueOrDefault(0),
                            A = a.GetValueOrDefault(0),
                        });
                        break;

                    default:
                        throw new Exception("Не известный тип");
                }
                
            }
            connection.Close();

            return result;
        }

        private static T GetFieldValue<T>(SQLiteDataReader reader, int order) {
            if (reader[order].GetType() != typeof(DBNull)) {
                return reader.GetFieldValue<T>(order);
            }

            return default;
        }
    }
}

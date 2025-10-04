using UnityEngine;
using System.IO;
using SQLite;
using MassageApp.Core;
using System.Collections.Generic;
using System;
using UnityEngine.Networking;

namespace MassageApp.Database
{
    public class DatabaseManager : MonoBehaviour
    {
        private SQLiteConnection _connection;
        private const string DatabaseName = "massage_app.db";
        private string _databasePath;

        private void Awake()
        {
            InitializeDatabase();
        }

        private void InitializeDatabase()
        {
            string sourcePath;
            string persistentDataPath = Path.Combine(Application.persistentDataPath, DatabaseName);
            bool isNewDatabaseCreated = false;

            if (!File.Exists(persistentDataPath))
            {
                isNewDatabaseCreated = true;

                if (Application.platform == RuntimePlatform.Android)
                {
                    sourcePath = Path.Combine(Application.streamingAssetsPath, DatabaseName);

                    UnityWebRequest loadDb = UnityWebRequest.Get(sourcePath);
                    loadDb.SendWebRequest();

                    while (!loadDb.isDone) { }

                    if (loadDb.result == UnityWebRequest.Result.Success)
                    {
                        File.WriteAllBytes(persistentDataPath, loadDb.downloadHandler.data);
                    }
                    else
                    {
                        Debug.LogError($"Failed to load database from StreamingAssets: {loadDb.error}");
                    }
                }
                else
                {
                    sourcePath = Path.Combine(Application.dataPath, "StreamingAssets", DatabaseName);

                    if (File.Exists(sourcePath))
                    {
                        File.Copy(sourcePath, persistentDataPath);
                    }
                }
            }

            _databasePath = persistentDataPath;
            _connection = new SQLiteConnection(_databasePath, SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create);

            if (isNewDatabaseCreated)
            {
                CreateTables();
            }
        }

        private void CreateTables()
        {
            _connection.CreateTable<Client>();
            _connection.CreateTable<Massage>();
            _connection.CreateTable<Discount>();
            _connection.CreateTable<Workplace>();
            _connection.CreateTable<Appointment>();
        }

        public SQLiteConnection GetConnection()
        {
            return _connection;
        }

        public List<Appointment> GetAppointmentsForMonth(int year, int month)
        {
            if (_connection == null)
            {
                Debug.LogError("Database connection is not initialized. Cannot fetch appointments.");
                return new List<Appointment>();
            }

            DateTime startDate = new DateTime(year, month, 1);
            DateTime endDate = startDate.AddMonths(1);

            string startString = startDate.ToString("yyyy-MM-dd HH:mm:ss");
            string endString = endDate.ToString("yyyy-MM-dd HH:mm:ss");

            return _connection.Query<Appointment>(
                "SELECT * FROM Appointment WHERE DateTime >= ? AND DateTime < ?",
                startString, endString
            );
        }
    }
}
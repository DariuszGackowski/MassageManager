using UnityEngine;
using System.IO;
using SQLite;
using MassageApp.Core;

namespace MassageApp.Database
{
    public class DatabaseManager : MonoBehaviour
    {
        private SQLiteConnection _connection;
        private const string DatabaseName = "massage_app.db";
        private string _databasePath;

        void Awake()
        {
            InitializeDatabase();
        }
        private void InitializeDatabase()
        {
            string streamingAssetsPath = Path.Combine(Application.streamingAssetsPath, DatabaseName);
            string persistentDataPath = Path.Combine(Application.persistentDataPath, DatabaseName);

            if (!File.Exists(persistentDataPath))
            {
                if (Application.platform == RuntimePlatform.Android)
                {
                    WWW loadDb = new WWW(streamingAssetsPath);
                    while (!loadDb.isDone) { }

                    File.WriteAllBytes(persistentDataPath, loadDb.bytes);
                }
                else
                {
                    File.Copy(streamingAssetsPath, persistentDataPath);
                }
            }

            _databasePath = persistentDataPath;
            _connection = new SQLiteConnection(_databasePath, SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create);

            CreateTables();
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
    }
}
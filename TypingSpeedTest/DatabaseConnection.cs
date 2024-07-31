using System;
using MySqlConnector;
using System.Collections.Generic;


namespace TypingSpeedTest
{
    public class DatabaseConnection
    {
        private MySqlConnection connection;

        public DatabaseConnection()
        {
            MySqlConnectionStringBuilder builder = new MySqlConnectionStringBuilder
            {
                Server = "localhost",
                UserID = "root",
                Password = "password", // what did you expect, lol?
                Database = "lol",
            };

            connection = new MySqlConnection(builder.ConnectionString);
        }

        public void OpenConnection()
        {
            try
            {
                if (connection.State == System.Data.ConnectionState.Closed)
                {
                    connection.Open();
                    //Console.WriteLine("Connected to the database.");
                }
            }
            catch (MySqlException ex)
            {
                // Console.WriteLine("Error opening connection: " + ex.Message);
            }
        }
        public void CloseConnection()
        {
            try
            {
                if (connection.State == System.Data.ConnectionState.Open)
                {
                    connection.Close();
                    //Console.WriteLine("Connection closed.");
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Error closing connection: " + ex.Message);
            }
        }

        private int GetCount(int dataset_id)
        {
            OpenConnection();
            int count = 0;
            string sql = $"SELECT `count` FROM paragraph_count WHERE dataset_id = {dataset_id}";
            using (MySqlCommand command = new MySqlCommand(sql, connection))
            {
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        count = reader.GetInt32(reader.GetOrdinal("count"));
                    }
                }
            }

            CloseConnection();
            return count;
        }

        public string GetRandomParagraph(int dataset_id)
        {
            string s = "Error";
            Random rand = new Random();

            int count = GetCount(dataset_id);
            Dictionary<int, string> tableName = new Dictionary<int, string>() { { 0, "practice_paragraph" }, { 1, "easy_paragraph" }, { 2, "medium_paragraph" }, { 3, "hard_paragraph" } };
            int randomNumber = rand.Next(0, count);

            OpenConnection();

            string sql = $"SELECT content from {tableName[dataset_id]} where `index` = {randomNumber}";
            using (MySqlCommand command = new MySqlCommand(sql, connection))
            {
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        s = reader.GetString(reader.GetOrdinal("content"));
                    }
                }
            }
            CloseConnection();
            return s;
        }

        public List<Record> GetLeaderBoard()
        {
            List<Record> LeaderBoard = new List<Record>();
            OpenConnection();
            string sql = "SELECT * FROM leaderboard ORDER BY page DESC, accuracy DESC, wpm DESC LIMIT 10";

            using (MySqlCommand command = new MySqlCommand(sql, connection))
            {
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int submissionId = Convert.ToInt32(reader["submission_id"]);
                        string name = reader["name"].ToString();
                        float accuracy = Convert.ToSingle(reader["accuracy"]);
                        float wpm = Convert.ToSingle(reader["wpm"]);
                        int page = Convert.ToInt32(reader["page"]);
                        string submissionDate = reader["submission_date"].ToString();

                        Record record = new Record(submissionId, name, accuracy, wpm, page, submissionDate);

                        LeaderBoard.Add(record);
                    }
                }
            }

            CloseConnection();
            return LeaderBoard;
        }

        public void AddSubmission(string name, float accuracy, float wpm, int page)
        {
            string submissionDate = DateTime.Now.ToString();
            OpenConnection();
            string sql = "INSERT INTO leaderboard (name, accuracy, wpm, page, submission_date) " + "VALUES (@name, @accuracy, @wpm, @page, @submissionDate)";

            using (MySqlCommand command = new MySqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@name", name);
                command.Parameters.AddWithValue("@accuracy", accuracy);
                command.Parameters.AddWithValue("@wpm", wpm);
                command.Parameters.AddWithValue("@page", page);
                command.Parameters.AddWithValue("@submissionDate", submissionDate);
                command.ExecuteNonQuery();

            }

            CloseConnection();
        }


    }
}
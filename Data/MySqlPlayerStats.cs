using System;
using System.Collections.Generic;
using InvestigationGame.Models;
using MySql.Data.MySqlClient;

namespace InvestigationGame.Data
{
    public class MySqlPlayerStats
    {
        private readonly string _connectionString;

        public MySqlPlayerStats(string connectionString)
        {
            _connectionString = connectionString;
        }

        public Player? GetPlayerByName(string name)
        {
            using var connection = new MySqlConnection(_connectionString);
            connection.Open();

            string query = "SELECT id, name, current_agent_index FROM players WHERE name = @name LIMIT 1";
            using var command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@name", name);

            using var reader = command.ExecuteReader();
            if (reader.Read())
            {
                return new Player(reader.GetString("name"), reader.GetInt32("current_agent_index"))
                {
                    Id = reader.GetInt32("id")
                };
            }

            return null;
        }

        public void SaveNewPlayer(Player player)
        {
            using var connection = new MySqlConnection(_connectionString);
            connection.Open();

            string query = "INSERT INTO players (name, current_agent_index) VALUES (@name, @current)";
            using var command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@name", player.Name);
            command.Parameters.AddWithValue("@current", player.CurrentAgentIndex);

            command.ExecuteNonQuery();
            player.Id = (int)command.LastInsertedId;
        }

        public void UpdatePlayerProgress(Player player)
        {
            using var connection = new MySqlConnection(_connectionString);
            connection.Open();

            string query = "UPDATE players SET current_agent_index = @current WHERE id = @id";
            using var command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@current", player.CurrentAgentIndex);
            command.Parameters.AddWithValue("@id", player.Id);

            command.ExecuteNonQuery();
        }

        public Player LoadPlayer(string name)
        {
            var player = GetPlayerByName(name);
            if (player == null)
            {
                player = new Player(name, 0);
                SaveNewPlayer(player);
            }
            return player;
        }
    }
}
using Npgsql;
using toxis.Models;

namespace toxis
{
    internal class DataBase
    {
        private static string constring = "Host=localhost;Username=postgres;Password=VEST777berto;Database=postgres";

        public async Task<int> zoneCost(string selectedZone)
        {
            using (var conn = new NpgsqlConnection(constring)) 
            {
                await conn.OpenAsync();
                string query = "SELECT cost FROM toxis.zones WHERE zone = @zone";
                using (var cmd = new NpgsqlCommand(query, conn)) 
                {
                    cmd.Parameters.AddWithValue("@zone", selectedZone);
                    return (int)(await cmd.ExecuteScalarAsync());
                }

            }
        }
        public async Task<int> addTicket(client client)
        {
            using (var conn = new NpgsqlConnection(constring))
            {
                await conn.OpenAsync();
                string query = "INSERT INTO toxis.clients (firstName, secondName, surname, email) VALUES (@firstName, @secondName, @surname, @email) RETURNING id;";
                int clientId;

                using (var cmd = new NpgsqlCommand(query, conn)) 
                {
                    cmd.Parameters.AddWithValue("@firstName", client.firstName);
                    cmd.Parameters.AddWithValue("@secondName", client.secondName);
                    cmd.Parameters.AddWithValue("@surname", client.surname);
                    cmd.Parameters.AddWithValue("@email", client.email);

                    clientId = (int)(await cmd.ExecuteScalarAsync());

                }

                string query2 = "INSERT INTO toxis.ticket (client, zone) VALUES (@clientId, (SELECT id FROM toxis.zones WHERE zone = @zone)) RETURNING id;";
                using (var cmd = new NpgsqlCommand(query2, conn)) 
                {
                    cmd.Parameters.AddWithValue("@clientId", clientId);
                    cmd.Parameters.AddWithValue("@zone", client.SelectedZone);

                    return (int)(await cmd.ExecuteScalarAsync());
                }
            }
        }
    }
}

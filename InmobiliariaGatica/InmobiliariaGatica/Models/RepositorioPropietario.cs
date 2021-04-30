using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace InmobiliariaGatica.Models
{

    public class RepositorioPropietario
    {
        private readonly IConfiguration configuration;
        private readonly string connectionString;

        public RepositorioPropietario(IConfiguration configuration)
        {
            this.configuration = configuration;
            connectionString = configuration["ConnectionStrings:DefaultConnection"];
        }

        
        public int Alta(Propietario p)
        {
            int res = -1;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = "INSERT INTO Propietario (Dni, Apellido, Nombre, Direccion) " +
                    "VALUES (@nombre, @apellido, @dni, @direccion);" +
                    "SELECT SCOPE_IDENTITY();";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@dni", p.Dni);
                    command.Parameters.AddWithValue("@apellido", p.Apellido);
                    command.Parameters.AddWithValue("@nombre", p.Nombre);
                    command.Parameters.AddWithValue("@direccion", p.Direccion);
                    connection.Open();
                    res = Convert.ToInt32(command.ExecuteScalar());
                    p.Id = res;
                    connection.Close();
                }
            }
            return res;
        }
        public List<Propietario> ObtenerTodos()
        {
            var listaPropietarios = new List<Propietario>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = "SELECT Id, Dni, Apellido, Nombre, Direccion " +
                    "FROM Propietario;";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.CommandType = CommandType.Text;
                    connection.Open();
                    var cursor = command.ExecuteReader();
                    while (cursor.Read())
                    {
                        listaPropietarios.Add(new Propietario
                        {
                            Id = cursor.GetInt32(0),
                            Dni = cursor.GetString(1),
                            Apellido = cursor.GetString(2),
                            Nombre = cursor.GetString(3),
                            Direccion = cursor.GetString(4),
                        });
                    }
                    connection.Close();
                }
            }
            return listaPropietarios;
        }
        public int Baja(int id)
        {
            int res = -1;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = "DELETE FROM Propietario " +
                    "WHERE Id = @id;";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@id", id);
                    connection.Open();
                    res = command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            return res;
        }
        public int Modificacion(Propietario p)
        {
            int res = -1;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = $"UPDATE Propietario SET Dni = @dni, Apellido = @apellido, Nombre = @nombre, Direccion = @direccion " +
                    $"WHERE Id = @id";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.CommandType = CommandType.Text;
                    
                    command.Parameters.AddWithValue("@dni", p.Dni);
                    command.Parameters.AddWithValue("@apellido", p.Apellido);
                    command.Parameters.AddWithValue("@nombre", p.Nombre);
                    command.Parameters.AddWithValue("@direccion", p.Direccion);
                    command.Parameters.AddWithValue("@id", p.Id);
                    connection.Open();
                    res = command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            return res;
        }

        public Propietario BuscarPropietario(int id)
        {
            Propietario p = null;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = "SELECT Id, Dni, Apellido, Nombre, Direccion " +
                    "FROM Propietario " +
                    "WHERE Id = @id;";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@id", id);
                    connection.Open();
                    var cursor = command.ExecuteReader();
                    while (cursor.Read())
                    {
                        p = new Propietario
                        {
                            Id = cursor.GetInt32(0),
                            Dni = cursor.GetString(1),
                            Apellido = cursor.GetString(2),
                            Nombre = cursor.GetString(3),
                            Direccion = cursor.GetString(4)
                        };
                    }

                    connection.Close();

                }
            }
            return p;
        }
        public Propietario Buscar(int id)
        {
            Propietario p = null;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = "SELECT Id, Dni, Apellido, Nombre, Direccion " +
                    "FROM Propietario " +
                    "WHERE Id = @id;";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@id", id);
                    connection.Open();
                    var cursor = command.ExecuteReader();
                    while (cursor.Read())
                    {
                        p = new Propietario
                        {
                            Id = cursor.GetInt32(0),
                            Dni = cursor.GetString(1),
                            Apellido = cursor.GetString(2),
                            Nombre = cursor.GetString(3),
                            Direccion = cursor.GetString(4)
                        };
                    }

                    connection.Close();

                }
            }
            return p;
        }
    }
}

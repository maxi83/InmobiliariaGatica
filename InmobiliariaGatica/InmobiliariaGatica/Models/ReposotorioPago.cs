using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace InmobiliariaGatica.Models
{

    public class RepositorioPago
    {
        private readonly IConfiguration configuration;
        private readonly string connectionString;

        public RepositorioPago(IConfiguration configuration)
        {
            this.configuration = configuration;
            connectionString = configuration["ConnectionStrings:DefaultConnection"];
        }


        public int Alta(Pago p)
        {
            int res = -1;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = "INSERT INTO Pagos (Nro, Fecha, Importe, IdContrato) " +
                    "VALUES (@Numero, @Fecha, @Importe, @IdContrato);" +
                    "SELECT SCOPE_IDENTITY();";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@numero", p.Nro);
                    command.Parameters.AddWithValue("@fecha", p.Fecha);
                    command.Parameters.AddWithValue("@importe", p.Importe);
                    command.Parameters.AddWithValue("@idContrato", p.IdContrato);
                    connection.Open();
                    res = Convert.ToInt32(command.ExecuteScalar());
                    p.Id = res;
                    connection.Close();
                }
            }
            return res;
        }
        public List<Pago> ObtenerTodos()
        {
            var listaPagos = new List<Pago>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = "SELECT Id, Nro, Fecha, Importe, IdContrato " +
                    "FROM Pagos;";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.CommandType = CommandType.Text;
                    connection.Open();
                    var cursor = command.ExecuteReader();
                    while (cursor.Read())
                    {
                        listaPagos.Add(new Pago
                        {
                            Id = cursor.GetInt32(0),
                            Nro = cursor.GetInt32(1),
                            Fecha = cursor.GetDateTime(2),
                            Importe = cursor.GetDecimal(3),
                            IdContrato = cursor.GetInt32(4),
                        });
                    }
                    connection.Close();
                }
            }
            return listaPagos;
        }
        public int Baja(int id)
        {
            int res = -1;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = "DELETE FROM Pagos " +
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
        public int Modificacion(Pago p)
        {
            int res = -1;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = "UPDATE Pagos" +
                    "SET Numero = @numero, Fecha = @fecha, Importe = @importe, IdContrato = @idContrato " +
                    "WHERE Id = @id;";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@id", p.Id);
                    command.Parameters.AddWithValue("@numero", p.Nro);
                    command.Parameters.AddWithValue("@fecha", p.Fecha);
                    command.Parameters.AddWithValue("@importe", p.Importe);
                    command.Parameters.AddWithValue("@idContrato", p.IdContrato);
                    connection.Open();
                    res = command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            return res;
        }
        public Pago BuscarPago(int id)
        {
            Pago p = null;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = "SELECT Id, Nro, Fecha, Importe, IdContrato " +
                    "FROM Pagos " +
                    "WHERE Id = @id;";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@id", id);
                    connection.Open();
                    var cursor = command.ExecuteReader();
                    while (cursor.Read())
                    {
                        p = new Pago
                        {
                            Id = cursor.GetInt32(0),
                            Nro = cursor.GetInt32(1),
                            Fecha = cursor.GetDateTime(2),
                            Importe = cursor.GetDecimal(3),
                            IdContrato = cursor.GetInt32(4),
                        };
                    }

                    connection.Close();

                }
            }
            return p;
        }
        
    }
}


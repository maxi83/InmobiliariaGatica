using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace InmobiliariaGatica.Models
{

    public class RepositorioContrato
    {
        private readonly IConfiguration configuration;
        private readonly string connectionString;

        public RepositorioContrato(IConfiguration configuration)
        {
            this.configuration = configuration;
            connectionString = configuration["ConnectionStrings:DefaultConnection"];
        }


        public int Alta(Contrato c)
        {
            int res = -1;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = $"INSERT INTO Contratos(IdInquilino, IdInmueble, FechaInicio, FechaFinal, Importe, Estado)  " +
                    $"VALUES( @idInquilino, @idInmueble, @fechaInicio, @fechaFinal, @importe, @estado)" +
                    $"SELECT SCOPE_IDENTITY();";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.CommandType = CommandType.Text;
                 
                    command.Parameters.AddWithValue("@idInquilino", c.IdInquilino);
                    command.Parameters.AddWithValue("@idInmueble", c.IdInmueble);
                    command.Parameters.AddWithValue("@fechaInicio", c.FechaInicio);
                    command.Parameters.AddWithValue("@fechaFinal", c.FechaFinal);
                    command.Parameters.AddWithValue("@importe", c.Importe);
                    command.Parameters.AddWithValue("@estado", c.Estado);
                    connection.Open();
                    res = Convert.ToInt32(command.ExecuteScalar());
                    c.Id= res;
                    connection.Close();
                }
            }
            return res;
        }
        public List<Contrato> ObtenerTodos()
        {
            List<Contrato> res = new List<Contrato>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = $"SELECT c.Id, c.IdInquilino, c.IdInmueble, FechaInicio, FechaFinal, Importe, Estado," +
                    $" inq.Nombre, inq.Apellido," +
                    $" inm.Direccion" +
                    $" FROM Contratos c INNER JOIN Inmuebles inm ON c.IdInmueble = inm.Id INNER JOIN Inquilino inq ON c.IdInquilino = inq.Id";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.CommandType = CommandType.Text;
                    connection.Open();
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Contrato c = new Contrato
                        {
                            Id = reader.GetInt32(0),
                            IdInquilino = reader.GetInt32(1),
                            IdInmueble = reader.GetInt32(2),
                            FechaInicio = reader.GetDateTime(3),
                            FechaFinal = reader.GetDateTime(4),
                            Importe = reader.GetDecimal(5),
                            Estado = reader.GetBoolean(6),

                            Inquilino = new Inquilino
                            {
                                Id = reader.GetInt32(1),
                                Nombre = reader.GetString(7),
                                Apellido = reader.GetString(8),
                            },

                            Inmueble = new Inmueble
                            {
                                Id = reader.GetInt32(2),
                                Direccion = reader.GetString(9)
                            }


                        }; res.Add(c);
                    }

                }
                connection.Close();
            }

            return res;

        }
        public int Baja(int id)
        {
            int res = -1;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = $"DELETE FROM Contratos WHERE Id=@id";

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
        public int Modificacion(Contrato c)
        {
            int res = -1;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = $"UPDATE Contratos SET IdInmueble=@idInmueble, idInquilino=@idInquilino, FechaInicio=@fechaInicio, FechaFinal=@fechaFinal, Importe=@importe, Estado=@estado " +
                        $"WHERE id=@id";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.CommandType = CommandType.Text;
                    
                    command.Parameters.AddWithValue("@idInmueble", c.IdInmueble);
                    command.Parameters.AddWithValue("@idInquilino", c.IdInquilino);
                    command.Parameters.AddWithValue("@fechaInicio", c.FechaInicio);
                    command.Parameters.AddWithValue("@fechaFinal", c.FechaFinal);
                    command.Parameters.AddWithValue("@importe", c.Importe);
                    command.Parameters.AddWithValue("@estado", c.Estado);
                    command.Parameters.AddWithValue("@id", c.Id);

                    connection.Open();
                    res = command.ExecuteNonQuery();
                    connection.Close();
                }
            }

            return res;
        }
        public Contrato ObtenerPorId(int id)
        {
            Contrato con = null;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = $" SELECT Id, c.IdInquilino, c.IdInmueble, FechaInicio, FechaFinal, Importe, Estado, " +
                    $" inq.Nombre, inq.Apellido ," +
                    $" inm.Direccion, inm.Ambientes" +
                    $" FROM Contratos c INNER JOIN Inmuebles inm ON c.Id = inm.Id " +
                    $" INNER JOIN Inquilinos inq ON c.IdInq = inq.Id " +
                    $" WHERE c.Id = @id";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.Add("@id", SqlDbType.Int).Value = id;
                    command.CommandType = CommandType.Text;
                    connection.Open();
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        con = new Contrato
                        {
                            Id = reader.GetInt32(0),
                            IdInquilino = reader.GetInt32(1),
                            IdInmueble = reader.GetInt32(2),
                            FechaInicio = reader.GetDateTime(3),
                            FechaFinal = reader.GetDateTime(4),
                            Importe = reader.GetDecimal(5),
                            Estado = reader.GetBoolean(6),


                            Inquilino = new Inquilino
                            {
                                Nombre = reader.GetString(8),
                                Apellido = reader.GetString(9),
                            },

                            Inmueble = new Inmueble
                            {
                                Direccion = reader.GetString(10),
                                Ambientes = reader.GetInt32(11)
                            }


                        };
                    }

                }
                connection.Close();
            }

            return con;

        }
    }
}

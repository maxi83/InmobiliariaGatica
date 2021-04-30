using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;


namespace InmobiliariaGatica.Models
{
	public class RepositorioInquilino
	{
		private readonly IConfiguration configuration;
		private readonly string connectionString;

	
    public RepositorioInquilino(IConfiguration configuration)
	{
		this.configuration = configuration;
		connectionString = configuration["ConnectionStrings:DefaultConnection"];
	}

      

        public int Alta(Inquilino e)
		{
			int res = -1;
			using (SqlConnection connection = new SqlConnection(connectionString))
			{

				string sql = "INSERT INTO Inquilino (Dni, Apellido, Nombre, Direccion_Trabajo, Telefono, Correo, Dni_Garante, Nombre_Garante, Apellido_Garante, Telefono_Garante) " +
					"VALUES (@dni, @apellido, @nombre, @direccion_Trabajo, @telefono, @correo, @dni_Garante, @nombre_Garante, @apellido_Garante, @telefono_Garante);" +
					"SELECT SCOPE_IDENTITY();";
				using (SqlCommand command = new SqlCommand(sql, connection))
				{
					command.CommandType = CommandType.Text;
					command.Parameters.AddWithValue("@dni", e.Dni);
					command.Parameters.AddWithValue("@apellido", e.Apellido);
					command.Parameters.AddWithValue("@nombre", e.Nombre);
					command.Parameters.AddWithValue("@direccion_trabajo", e.Direccion_Trabajo);
			
					command.Parameters.AddWithValue("@telefono", e.Telefono);
					command.Parameters.AddWithValue("@correo", e.Correo);
					command.Parameters.AddWithValue("@dni_Garante", e.Dni_Garante);
					command.Parameters.AddWithValue("@nombre_Garante", e.Nombre_Garante);
					command.Parameters.AddWithValue("@apellido_Garante", e.Apellido_Garante);
					command.Parameters.AddWithValue("@telefono_Garante", e.Telefono_Garante);
					connection.Open();
					res = Convert.ToInt32(command.ExecuteScalar());
					e.Id = res;
					connection.Close();
					
				}
			}
			return res;
		}
		public int Baja(int id)
		{
			int res = -1;
			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				string sql  = "DELETE FROM Inquilino " +
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
		public int Modificacion(Inquilino e)
		{
			int res = -1;
			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				string sql = $"UPDATE Inquilino SET " +
					$"Dni=@dni,Apellido=@apellido, Nombre=@nombre, Direccion_Trabajo=@Direccion_Trabajo , Telefono=@telefono, Correo=@Correo " +
					$"WHERE Id = @id";
				using (SqlCommand command = new SqlCommand(sql, connection))
				{
					command.CommandType = CommandType.Text;
					command.Parameters.AddWithValue("@id", e.Id);
					command.Parameters.AddWithValue("@dni", e.Dni);
					command.Parameters.AddWithValue("@apellido", e.Apellido);
					command.Parameters.AddWithValue("@nombre", e.Nombre);
					command.Parameters.AddWithValue("@direccion_trabajo", e.Direccion_Trabajo);
					command.Parameters.AddWithValue("@telefono", e.Telefono);
					command.Parameters.AddWithValue("@correo", e.Correo);
					command.Parameters.AddWithValue("@dniGarante", e.Dni_Garante);
					command.Parameters.AddWithValue("@nombreGarante", e.Nombre_Garante);
					command.Parameters.AddWithValue("@apellidoGarante", e.Apellido_Garante);
					command.Parameters.AddWithValue("@telGarante", e.Telefono_Garante);
					connection.Open();
					res = command.ExecuteNonQuery();
					connection.Close();
				}
			}
			return res;
		}

		public IList<Inquilino> ObtenerTodos()
		{
			var lisInquilinos = new List<Inquilino>();
			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				string sql = "SELECT Id, Dni, Apellido, Nombre, Direccion_Trabajo, Telefono, Correo, Dni_Garante, Nombre_Garante, Apellido_Garante, Telefono_Garante " +
				   "FROM Inquilino;";
				using (SqlCommand command = new SqlCommand(sql, connection))
				{
					command.CommandType = CommandType.Text;
					connection.Open();
					var reader = command.ExecuteReader();
					while (reader.Read())
					{
						lisInquilinos.Add(new Inquilino
						{
							Id = reader.GetInt32(0),
							Dni = reader.GetString(1),
							Apellido = reader.GetString(2),
							Nombre = reader.GetString(3),
							Direccion_Trabajo = reader.GetString(4),
							Telefono = reader.GetString(5),
							Correo= reader.GetString(6),
							Dni_Garante = reader.GetString(7),
							Nombre_Garante = reader.GetString(8),
							Apellido_Garante = reader.GetString(9),
							Telefono_Garante = reader.GetString(10),
						});
					
					}
					connection.Close();
				}
			}
			return lisInquilinos;
		}

		public Inquilino ObtenerPorId(int id)
		{
			Inquilino p = null;
			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				string sql = "SELECT Id, Dni, Apellido, Nombre, Direccion_Trabajo, Telefono, Correo, Dni_Garante, Nombre_Garante, Apellido_Garante, Telefono_Garante " +
					"FROM Inquilino " +
					"WHERE Id = @id;";
				using (SqlCommand command = new SqlCommand(sql, connection))
				{
					command.CommandType = CommandType.Text;
					command.Parameters.Add("@id", SqlDbType.Int).Value = id;
					connection.Open();
					var reader = command.ExecuteReader();
					while (reader.Read())
					{
						p = new Inquilino
						{
							Id = reader.GetInt32(0),
							Apellido = reader.GetString(1),
							Nombre = reader.GetString(2),
							Direccion_Trabajo = reader.GetString(3),
							Dni = reader.GetString(4),
							Telefono = reader.GetString(5),
							Correo = reader.GetString(6),
							Dni_Garante = reader.GetString(7),
							Nombre_Garante = reader.GetString(8),
							Apellido_Garante = reader.GetString(9),
							Telefono_Garante = reader.GetString(10),
						};
						
					}
					connection.Close();
				}
			}
			return p;
		}
	}
}
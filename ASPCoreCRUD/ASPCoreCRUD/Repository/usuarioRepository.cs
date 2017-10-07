using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Dapper;
using System.Data;
using Npgsql;
using ASPCoreCRUD.Models;
using ASPCoreCRUD.iRepository;

namespace ASPCoreCRUD.iRepository
{
    public class usuarioRepository : usuarioiRepository<usuarioModel>
    {
        private string connectionString;
        public usuarioRepository(IConfiguration configuration)
        {
            connectionString = configuration.GetValue<string>("ConnectionDB:ConnectionString");
        }

        internal IDbConnection Connection
        {
            get
            {
                return new NpgsqlConnection(connectionString);
            }
        }

        public IEnumerable<usuarioModel> FindAll()
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<usuarioModel>("SELECT * from usuarios order by idusuario");
            }
        }
        public void Delete(int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                dbConnection.Execute("DELETE FROM usuarios WHERE idusuario=@Id", new { Id = id });
            }
        }

        public void Insert(usuarioModel item)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                dbConnection.Execute("INSERT INTO usuarios (nombrecompleto, iniciosesion, clave, fechanamimiento, activo) VALUES(@nombrecompleto,@iniciosesion,@clave,@fechanamimiento, @activo)", item);
            }

        }

        public usuarioModel FindByID(int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<usuarioModel>("SELECT * FROM usuarios WHERE idusuario = @Id", new { Id = id }).FirstOrDefault();
            }
        }

        public void Update(usuarioModel item)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                dbConnection.Query("UPDATE usuarios SET nombrecompleto = @nombrecompleto,  iniciosesion  = @iniciosesion, clave = @clave, fechanamimiento = @fechanamimiento, activo = @activo  WHERE idusuario = @idusuario", item);
            }
        }

        
    }
}


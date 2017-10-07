using ASPCoreCRUD.iRepository;
using ASPCoreCRUD.Models;
using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ASPCoreCRUD.Repository
{
    public class perfilRepository : perfiliRepository<perfilModel>
    {
        private string connectionString;
        public perfilRepository(IConfiguration configuration)
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

        public IEnumerable<perfilModel> FindAll()
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<perfilModel>("SELECT * FROM perfil order by idperfil");
            }
        }
        public void Delete(string id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                dbConnection.Execute("DELETE FROM perfil WHERE idperfil=@Id", new { Id = id });
            }
        }

        public void Insert(perfilModel item)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                dbConnection.Execute("INSERT INTO perfil (nombre, fechains, fechaact, activo) VALUES(@nombre,@fechains,@fechaact, @activo)", item);
            }

        }

        public perfilModel FindByID(string id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<perfilModel>("SELECT * FROM perfil WHERE idperfil = @Id", new { Id = id }).FirstOrDefault();
            }
        }

        public void Update(perfilModel item)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                dbConnection.Query("UPDATE perfil SET nombre = @nombre,  fechains  = @fechains, fechaact = @fechaact, activo = @activo  WHERE idperfil = @idperfil", item);
            }
        }

      
    }
}

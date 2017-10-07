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
    public class homeRepository : homeiRepository<usuarioModel>
    {

        private string connectionString;
        public homeRepository(IConfiguration configuration)
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
                return dbConnection.Query<usuarioModel>("SELECT * FROM usuarios order by idusuario");
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

    }
}

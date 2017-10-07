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
    public class MenuRepository : MenuiRepository<menuModel>
    {
        private string connectionString;
        public MenuRepository(IConfiguration configuration)
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

        public IEnumerable<menuModel> FindAll()
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<menuModel>("SELECT * FROM menu order by idmenu");
            }
        }
        public void Delete(int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                dbConnection.Execute("DELETE FROM menu WHERE idmenu=@Id", new { Id = id });
            }
        }

        public void Insert(menuModel item)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                dbConnection.Execute("INSERT INTO menu (titulo, url, icono, activo, idpadre) VALUES(@titulo, @url, @icono, @activo, @idpadre)", item);
            }

        }

        public menuModel FindByID(int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<menuModel>("SELECT * FROM menu WHERE idmenu = @Id", new { Id = id }).FirstOrDefault();
            }
        }

        public void Update(menuModel item)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                dbConnection.Query("UPDATE menu SET titulo = @titulo,  url = @url, icono = @icono, activo = @activo, idpadre = @idpadre  WHERE idmenu = @idmenu", item);
            }
        }


    }

}


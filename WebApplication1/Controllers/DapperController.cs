using Microsoft.AspNetCore.Mvc;
using Npgsql;
using Dapper;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]

    public class UsersController : ControllerBase
    {
        private string CONNECTIONSTRING = "Server=127.0.0.1;Port=5432;Database=MyData;User Id=postgres;Password=admin;";
        [HttpGet]
        public List<Products> GetDapper()
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(CONNECTIONSTRING))
            {
                string query = "select * from products";
                
                var lst  = connection.Query<Products>(query).ToList();

                return lst;
            }
        }

        [HttpPost]
        public string PostDapper(string Name, string Description, string PhotoPath)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(CONNECTIONSTRING))
            {
                string query = $"insert into products(name, description, photopath) values (@name, @description, @photopath);";
                int status = connection.Execute(query, new
                {
                    name = Name,
                    description = Description,
                    photopath = PhotoPath
                });
                return $"Post status [=> {status}";
            }
        }
        [HttpPut]
        public string PutDapper(int id,string name,string description,string photopath, Products products)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(CONNECTIONSTRING))
            {
                string query = $"update products set name = @name, description = @description, photopathth = @photopath where id = @id";
                int status = connection.Execute(query, new
                {
                    id = id,
                    name = name,
                    description = description,
                    photopath = photopath
                });
                return $"Update Status [=> {status}";
            }
        }
        [HttpPatch]
        public string PatchDapper(int id, string Name)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(CONNECTIONSTRING))
            {
                string query = $"update products set name = @name where id = @id";
                int status = connection.Execute(query, new { id = id, name = Name });

                return $"Patch status [=> {status}";
            }


        }
        [HttpDelete]
        public string DeleteDapper(int id)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(CONNECTIONSTRING))
            {
                string query = $"delete from products where id = @id";
                int status = connection.Execute(query, new { id = id });
                return $"Delete status [=> {status}";
            }
        }
    }
}

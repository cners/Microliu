using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

namespace Db.Test.Connection.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        [HttpGet("/api/test")]
        public ActionResult<string> Get()
        {
            try
            {
                string connectionString = "Database=microliu_email;Data Source=39.107.24.71;Port=9020;UserId=haofenfen;Password=haofenfen123;Charset=utf8;";
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "insert into test (name) values(" + (DateTime.Now.ToString("yyyyMMddHHmmss")) + ")";
                    MySqlCommand cmd = connection.CreateCommand();
                    cmd.CommandText = sql;
                    cmd.ExecuteNonQuery();

                    return "ok";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}

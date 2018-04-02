using DomainModel;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SKYINTRA_RestAPI.DAL.Context
{
    public class IPWMetazoContext
    {
        public string ConnectionString { get; set; }

        public IPWMetazoContext(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        public MySqlConnection GetConnection()
        {
            return new MySqlConnection(ConnectionString);
        }

        
    }
}

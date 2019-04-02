using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageUpload.Data
{
    public class Image
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string FileName { get; set; }
    }

    public class ImageManager
    {
        private string _connectionString;

        public ImageManager(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void SaveImage(Image image)
        {
            using (var connection = new SqlConnection(_connectionString))
            using (var cmd = connection.CreateCommand())
            {
                cmd.CommandText = "INSERT INTO Images (Description, FileName) " +
                                  "VALUES (@desc, @fileName)";
                cmd.Parameters.AddWithValue("@desc", image.Description);
                cmd.Parameters.AddWithValue("@fileName", image.FileName);
                connection.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public IEnumerable<Image> Get()
        {
            using (var connection = new SqlConnection(_connectionString))
            using (var cmd = connection.CreateCommand())
            {
                var list = new List<Image>();
                cmd.CommandText = "SELECT * FROM Images";
                connection.Open();
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(new Image
                    {
                        Id = (int)reader["Id"],
                        Description = (string)reader["Description"],
                        FileName = (string)reader["FileName"]
                    });
                }

                return list;
            }
        }
    }


}

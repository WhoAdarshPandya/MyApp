using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
namespace MyApp.Models
{
    public class SqlPojo
    {
        private static MySqlConnection connection = new MySqlConnection(getConnectionString());
        private static MySqlCommand command = null;
        private static MySqlDataReader rdr = null;
        private static string insertQuery = "insert into places (name,description,isPrime,image_url,price,days) values (@name,@description,@isPrime,@image_url,@price,@days);";
        private static string updateQuery = "update places set name=@name,description=@description,isPrime=@isPrime,image_url=@image_url,price=@price,days=@days where id = @id";
        private static string deleteQuery = "delete from places where id = @id";
        private static string selectQueryWishlist = "select * from wishlist where id = @id";
        private static string insertWishlistQuery = "insert into wishlist values(@id)";
        private static string wishlistSelectQuery = "select * from places where id IN(select id from wishlist)";
        private static string deleteFromWishlistQuery = "delete from wishlist where id = @id";
        private static string getSelectedPlacesQuery = "select * from places where id = @id";

        public static int insertAPlace(string name, string desc, string isPrime,string image_url,string price,string days)
        {
            int noro;
            connection.Open();
            command = new MySqlCommand(insertQuery, connection);
            command.Parameters.AddWithValue("@name", name);
            command.Parameters.AddWithValue("@description", desc);
            command.Parameters.AddWithValue("@isPrime", isPrime);
            command.Parameters.AddWithValue("@image_url", image_url);
            command.Parameters.AddWithValue("@price", price);
            command.Parameters.AddWithValue("@days", days);
            noro = command.ExecuteNonQuery();
            connection.Close();
            return noro;
        }

        public static int updatePlace(int id, string name, string isPrime, string desc, string image_url,string price,string days)
        {
            int noro;
            connection.Open();
            command = new MySqlCommand(updateQuery, connection);
            command.Parameters.AddWithValue("@id", id);
            command.Parameters.AddWithValue("@name", name);
            command.Parameters.AddWithValue("@description", desc);
            command.Parameters.AddWithValue("@isPrime", isPrime);
            command.Parameters.AddWithValue("@image_url", image_url);
            command.Parameters.AddWithValue("@price", price);
            command.Parameters.AddWithValue("@days", days);

            noro = command.ExecuteNonQuery();
            connection.Close();
            return noro;
        }
        public static int deleteFromWishlist(int id)
        {
            int noro = 0;
            connection.Open();
            command = new MySqlCommand(deleteFromWishlistQuery, connection);
            command.Parameters.AddWithValue("@id", id);
            command.ExecuteNonQuery();
            connection.Close();
            return noro;
        }
        public static List<PlacesModel> getAllWishlist()
        {
            List<PlacesModel> movies = new List<PlacesModel>();
            connection.Open();
            command = new MySqlCommand(wishlistSelectQuery, connection);
            rdr = command.ExecuteReader();
            while (rdr.Read())
            {
                movies.Add(new PlacesModel
                {
                    id = Convert.ToInt32(rdr["id"].ToString()),
                    name = rdr["name"].ToString(),
                    isPrime = rdr["isPrime"].ToString(),
                    image_url = rdr["image_url"].ToString(),
                    days = rdr["days"].ToString(),
                    description = rdr["description"].ToString(),
                    price = rdr["price"].ToString()
                }) ;
            }
            connection.Close();
            return movies;
        }

        public static int insertPlaceinWishlist(int id)
        {
            int noro = 0;
            connection.Open();
            command = new MySqlCommand(selectQueryWishlist, connection);
            command.Parameters.AddWithValue("@id", id);
            rdr = command.ExecuteReader();
            if (rdr.HasRows)
            {
                rdr.Close();
            }
            else
            {
                rdr.Close();
                command = new MySqlCommand(insertWishlistQuery, connection);
                command.Parameters.AddWithValue("@id", id);
                noro = command.ExecuteNonQuery();

            }
            connection.Close();
            return noro;
        }

        public static PlacesModel getSelectedPlace(int id)
        {
            PlacesModel mv = new PlacesModel();
            connection.Open();
            command = new MySqlCommand(getSelectedPlacesQuery, connection);
            command.Parameters.AddWithValue("@id", id);
            rdr = command.ExecuteReader();
            while (rdr.Read())
            {
                mv.id = Convert.ToInt32(rdr["id"].ToString());
                mv.name = rdr["name"].ToString();
                mv.image_url = rdr["image_url"].ToString();
                mv.description = rdr["description"].ToString();
                mv.days = rdr["days"].ToString();
                mv.price = rdr["price"].ToString();
                mv.isPrime = rdr["isPrime"].ToString();
            }
            connection.Close();
            return mv;
        }

        public static int deletePlace(int id)
        {
            int noro;
            connection.Open();
            command = new MySqlCommand(deleteQuery, connection);
            command.Parameters.AddWithValue("@id", id);
            noro = command.ExecuteNonQuery();
            connection.Close();
            return noro;
        }

        public static List<PlacesModel> getAllPlaces(string order, string field)
        {
            connection.Open();
            List<PlacesModel> movies = new List<PlacesModel>();
            var query = "";

            if (order == "" && field == "")
                query = "select * from places ORDER BY id ASC";
            else
                query = "select * from places ORDER BY " + field + " " + order;

            command = new MySqlCommand(query, connection);
            rdr = command.ExecuteReader();
            while (rdr.Read())
            {
                movies.Add(new PlacesModel
                {
                    id = Convert.ToInt32(rdr["id"].ToString()),
                    name = rdr["name"].ToString(),
                    description = rdr["description"].ToString(),
                    image_url = rdr["image_url"].ToString(),
                    isPrime = rdr["isPrime"].ToString(),
                    days = rdr["days"].ToString(),
                    price = rdr["price"].ToString()
                });
            }
            connection.Close();
            return movies;
        }

        public static string getConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["mycon"].ToString();
        }
    }
}
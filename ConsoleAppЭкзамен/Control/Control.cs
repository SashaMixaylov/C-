using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Control
{
    public class Control : Connection
    {
        private void AddProduct(Music product)
        {
            using (var connection = new SqlConnection(_connectionString)) 
            {
                connection.Open();

                const string sql = @"INSERT INTO Products 
                            (NameAlbum, NameGroup, Publisher, AmountOfTrack, Genre, Year, CostPrice, Price)
                            VALUES (@NameAlbum, @NameGroup, @Publisher, @AmountOfTrack, @Genre, @Year, @CostPrice, @Price)";

                using (var command = new SqlCommand(sql, connection)) 
                {
                    command.Parameters.AddWithValue("@NameAlbum", record.NameAlbum);
                    command.Parameters.AddWithValue("@NameGroup", record.NameGroup);
                    command.Parameters.AddWithValue("@Publisher", record.Publisher);
                    command.Parameters.AddWithValue("@AmountOfTrack", record.AmountOfTrack);
                    command.Parameters.AddWithValue("@Genre", record.Genre);
                    command.Parameters.AddWithValue("@Year", record.Year);
                    command.Parameters.AddWithValue("@CostPrice", record.CostPrice);
                    command.Parameters.AddWithValue("@Price", record.Price);
                    command.Parameters.AddWithValue("@Quantity", record.Quantity);

                    command.ExecuteNonQuery();
                }

            }

        }

        public List<Record> SearchRecords(string searchTerm, RecordSearchType searchType)
        {
            using (var connection = new SqlConnection(_connectionString)
            {
                connection.Open();

                string sql = "";
                switch (searchType)
                {
                    case RecordSearchType.ByArtist:
                        sql = "SELECT * FROM Records WHERE NameGroup LIKE @SearchTerm";
                        break;
                    case RecordSearchType.ByGenre:
                        sql = "SELECT * FROM Records WHERE Genre LIKE @SearchTerm";
                        break;
                    case RecordSearchTypeByName:
                        sql = "SELECT * FROM Records WHERE NameAlbum LIKE @SearchTerm";
                        break;
                }

                using (var command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@SearchTerm", $"%{searchTerm}%");

                    using (var reader = command.ExecuteReader())
                    {
                        var records = new List<Record>();
                        while (reader.Read())
                        {
                            records.Add(new Record
                            {
                                ID = (int)reader["ID"],
                                NameAlbum = reader["NameAlbum"].ToString(),
                                NameGroup = reader["NameGroup"].ToString(),
                                Publisher = reader["Publisher"].ToString(),
                                AmountOfTrack = (int)reader["AmountOfTrack"],
                                Genre = reader["Genre"].ToString(),
                                Year = (int)reader["Year"],
                                CostPrice = (decimal)reader["CostPrice"],
                                Price = (decimal)reader["Price"],
                                Quantity = (int)reader["Quantity"]
                            });
                        }
                        return records;
                    }
                }
            }
        }

        public decimal Discount(string genre, decimal totalSum)
        {
            // Скидка 10% на джаз в определенные дни
            bool JazzWeek = DateTime.Now.DayOfWeek == DayOfWeek.Monday ||
                             DateTime.Now.DayOfWeek == DayOfWeek.Tuesday;

            if (genre.ToLower() == "джаз" && JazzWeek)
                return totalSum * 0.10m;

            // Дополнительные скидки по сумме покупок
            if (totalSum >= 70000) return totalSum * 0.15m;
            if (totalSum >= 40000) return totalSum * 0.10m;
            if (totalSum >= 10000) return totalSum * 0.05m;

            return 0;
        }

        public Dictionary<string, int> GetPopularGenres()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

               string sql = @"
                SELECT Genre, COUNT(*) as Count
                FROM Records r
                JOIN OrderItems oi ON r.RecordID = oi.RecordID
                GROUP BY Genre
                ORDER BY Count DESC";

                var genres = new Dictionary<string, int>();

                using (var command = new SqlCommand(sql, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            genres.Add(reader["Genre"].ToString(), (int)reader["Count"]);
                        }
                    }
                }

                return genres;
            }
        }
          
        
   }

    public class Record
    {
        public int RecordID { get; set; }
        public string Name { get; set; }
        public string Artist { get; set; }
        public string Publisher { get; set; }
        public int TrackCount { get; set; }
        public string Genre { get; set; }
        public int Year { get; set; }
        public decimal CostPrice { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }

    public enum RecordSearchType
    {
        Artist,
        Genre,
        Name
    }
}


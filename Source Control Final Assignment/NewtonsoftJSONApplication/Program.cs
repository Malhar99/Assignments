using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSON
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Serialization");
            // 1. Serialization
            Movie movie = new Movie { Id = 1, Title = "Mission Impossible" };
            // movie is a object let us convert this to a string by using JsonConvert.SerializeObject            
            string result = JsonConvert.SerializeObject(movie);
            // Converts to string as {"Id":1,"Title":"Mission Impossible"}
            Console.WriteLine(result);

            Console.WriteLine("\nDeserialization");
            // 2. Deserialization
            Movie newMovie = JsonConvert.DeserializeObject<Movie>(result);
            // Now string is converted to object            
            Console.WriteLine("Id : " + newMovie.Id);
            Console.WriteLine("Title : " + newMovie.Title);

            Console.WriteLine("\nSerialization of collection");
            // 3. Serialization of collection
            List<Movie> movies = new List<Movie> {
                new Movie { Id = 1, Title = "Titanic" },
                new Movie { Id = 2, Title = "The martian" },
                new Movie { Id = 3, Title = "Black panther" } ,
                new Movie { Id = 4, Title = "Deadpool 2" } ,
            };

            string collectionResult = JsonConvert.SerializeObject(movies);
            Console.WriteLine(collectionResult);

            Console.WriteLine("\nDeserialization of collection");
            // 4. Deserialization of collection
            List<Movie> newMovies = JsonConvert.DeserializeObject<List<Movie>>(collectionResult);
            foreach (var item in newMovies)
            {
                Console.WriteLine("Id : " + item.Id + "\tTitle : " + item.Title);
            }
        }


        class Movie
        {
            public int Id { get; set; }
            public string Title { get; set; }
        }

    }
}

using System.Data;

namespace MistralMoviesAPI
{
    public class Helper
    {
        public static IEnumerable<Movie> GetMoviesArray(DataTable Movies, SqlHelper sql)
        {
            Movie[] _movies = new Movie[Movies.Rows.Count];
            for (int i = 0; i < Movies.Rows.Count; i++)
            {
                _movies[i] = new Movie();
                _movies[i].MovieID = int.Parse(Movies.Rows[i]["MovieId"].ToString());
                _movies[i].Title = Movies.Rows[i]["Title"].ToString();
                _movies[i].Description = Movies.Rows[i]["Description"].ToString();
                _movies[i].ImgUrl = Movies.Rows[i]["Thumbnail"].ToString();
                _movies[i].Rating = int.Parse(Movies.Rows[i]["Rating"].ToString());
                _movies[i].LoadActors(sql.ConnectionString);
                _movies[i].ReleaseDate = Movies.Rows[i]["ReleaseDate"].ToString();
            }
            return _movies.ToArray();
        }
    }
}

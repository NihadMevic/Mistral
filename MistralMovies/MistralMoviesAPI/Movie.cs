using System.Data;
using System.Text.RegularExpressions;

namespace MistralMoviesAPI
{
    public class Movie
    {
        public int MovieID { get; set; }
        public string Title { get; set; }
        public string ImgUrl { get; set; }
        public string Description { get; set; }
        public string ReleaseDate { get; set; }
        public List<Actor> Cast { get; set; }
        public double Rating { get; set; }
        SqlHelper sql { get; set; }

        public Movie()
        {
            this.Cast = new List<Actor>();
             sql = new SqlHelper();
            this.Rating = 0;
        }
        public void GetMovieRating(string connS)
        {
            DataTable _ratingDt = new DataTable();
            sql.ConnectionString = connS;
            _ratingDt = sql.ExecSql("exec MistralMovies.dbo.qMovieRating " + this.MovieID);
            if (_ratingDt.Rows.Count > 0)
            {
                double rating = 0;
                if(double.TryParse(_ratingDt.Rows[0]["Rating"].ToString(),out rating))
                {
                    this.Rating = rating;
                }
            }
        }
        public void LoadActors(string connS)
        {
            DataTable Actors = new DataTable();
            sql.ConnectionString = connS;
            Actors = sql.ExecSql("exec MistralMovies.dbo.qActors " + this.MovieID);
            if(Actors.Rows.Count > 0)
            {
                for(int i = 0; i < Actors.Rows.Count; i++)
                {
                    Actor _actor = new Actor();
                    _actor.Name = Actors.Rows[i]["Name"].ToString();
                    this.Cast.Add(_actor);
                }
            }
        }
    }
    public class MovieFilter
    {
        public string Query { get; set; }
        public int MinStars { get; set; }
        public int MaxStars { get; set; }
        public DateTime NewerThan  { get; set; }
        public DateTime OlderThan { get; set; }
        public string sNewerThan { get; set; }
        public string sOlderThan { get; set; }
        public MovieFilter(string query)
        {
            this.Query = "";
            this.MinStars = 0;
            this.MaxStars = 5;
            this.OlderThan = DateTime.Now;
            this.NewerThan = new DateTime(1800,1,1);

            List<string> queryParts = new List<string>();
            if(query.Contains(","))
            {
                queryParts = query.Split(",").ToList();
                if(queryParts[0].Trim() != "")
                {
                    this.Query = queryParts[0].Trim();
                }
                if(queryParts.Count > 1)
                {
                    for(int i = 1; i < queryParts.Count; i++)
                    {
                        CheckString(queryParts[i]);
                    }
                }
            }
            else if(query.ToLower().Contains("star") || query.ToLower().Contains("years") || query.ToLower().Contains("older than") || query.ToLower().Contains("after"))
            {
                CheckString(query);
            }
            else
            {
                this.Query = query;
            }
            this.FormatDates();
        }
        public void FormatDates()
        {
            this.sNewerThan = this.NewerThan.Year + "-" + this.NewerThan.Month + "-" + this.NewerThan.Day;
            this.sOlderThan = this.OlderThan.Year + "-" + this.OlderThan.Month + "-" + this.OlderThan.Day;
        }
        private void CheckString(string query)
        {
            if (query.ToLower().Contains("star"))
            {
                if (query.Trim().ToLower().StartsWith("at least") || query.Trim().ToLower().StartsWith("minimum"))
                {
                    this.MinStars = Int32.Parse(Regex.Match(query, @"\d+").Value);
                }
                else
                {
                    this.MaxStars = Int32.Parse(Regex.Match(query, @"\d+").Value);
                }
            }
            else
            {
                if (query.ToLower().Contains("years"))
                {
                    if (query.Trim().ToLower().StartsWith("not older than") || query.Trim().ToLower().StartsWith("last"))
                    {
                        int year = Int32.Parse(Regex.Match(query, @"\d+").Value);
                        TimeSpan span = new TimeSpan(365 * year, 0, 0, 0);
                        this.NewerThan = DateTime.Now.Subtract(span);
                    }
                    else if (query.Trim().ToLower().StartsWith("older than"))
                    {
                        int year = Int32.Parse(Regex.Match(query, @"\d+").Value);
                        TimeSpan span = new TimeSpan(365 * year, 0, 0, 0);
                        this.OlderThan = DateTime.Now.Subtract(span);
                    }
                }
                else
                {
                    if (query.Trim().ToLower().StartsWith("after") || query.Trim().ToLower().StartsWith("not older than"))
                    {
                        this.NewerThan = new DateTime(Int32.Parse(Regex.Match(query, @"\d+").Value), 12, 31);
                    }
                    else if (query.Trim().ToLower().StartsWith("older than"))
                    {
                        this.OlderThan = new DateTime(Int32.Parse(Regex.Match(query, @"\d+").Value), 01, 01);
                    }
                }
            }
        }
    }
}
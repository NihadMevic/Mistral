using Microsoft.AspNetCore.Mvc;
using System.Data;


namespace MistralMoviesAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private IConfiguration Configuration;
        private SqlHelper sql;
        public MovieController(IConfiguration _configuration)
        {
            Configuration = _configuration;
            sql = new SqlHelper();
            sql.ConnectionString = Configuration.GetConnectionString("LocalDb");
        }
        
        [HttpGet(Name = "GetMovie")]
        [BasicAuth]
        public IEnumerable<Movie> Get()
        {
            try
            {
                return Helper.GetMoviesArray(this.sql.ExecSql("exec MistralMovies.dbo.qMovies 10,1"), this.sql);
            }
            catch (Exception ex)//TODO HANDLE ERROR
            {
                return null;
            }
        }

        
        [HttpGet("{id}")]
        [BasicAuth]
        public IEnumerable<Movie> Get(int id)
        {
            try
            {
                return Helper.GetMoviesArray(this.sql.ExecSql("exec MistralMovies.dbo.qMovies 10," + id), this.sql);
            }
            catch (Exception ex)//TODO HANDLE ERROR
            {
                return null;
            }
        }
        [HttpGet("{id}/{count}")]
        [BasicAuth]
        public IEnumerable<Movie> Get(int id, int count)
        {
            try
            {
                return Helper.GetMoviesArray(this.sql.ExecSql("exec MistralMovies.dbo.qMovies "+ count +"," + id), this.sql);
            }
            catch (Exception ex)//TODO HANDLE ERROR
            {
                return null;
            }
        }

        [HttpGet("{id}/{count}/{query}")]
        [BasicAuth]
        public IEnumerable<Movie> Get(int id,int count,string query)
        {
            try
            {
                MovieFilter filter = new MovieFilter(query);
                return Helper.GetMoviesArray(this.sql.ExecSql("exec MistralMovies.dbo.qMoviesSearch "+ count + "," + 
                                                        id+",'" + filter.Query + "'," + filter.MinStars + "," + 
                                                        filter.MaxStars + ",'" + filter.sNewerThan + "','"+filter.sOlderThan+"'"),this.sql);
            }
            catch (Exception ex)//TODO HANDLE ERROR
            {
                return null;
            }
        }
        // POST api/<MovieController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        
        [HttpPut]
        [BasicAuth]
        public void Put( [FromBody] MovieRatingRequest value)
        {
            try
            {
                this.sql.ExecSql("exec MistralMovies.dbo.qRateMovie " + value.Rating + ", " + value.MovieId);
            }
            catch(Exception e)
            {
               
            }
        }

        // DELETE api/<MovieController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
        
    }
}

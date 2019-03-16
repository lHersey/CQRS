using System.Net;
using System.Threading.Tasks;
using CQRSExample.Application.Models;
using CQRSExample.Application.Movies.Commands.CreateMovie;
using CQRSExample.Application.Movies.Models;
using CQRSExample.Application.Movies.Queries.GetMovieList;
using Microsoft.AspNetCore.Mvc;

namespace CQRSExample.WebAPI.Controllers
{
    [Route("/api/movies")]
    public class MoviesController : BaseController
    {
        [HttpGet]
        [ProducesResponseType(typeof(QueryResultResource<KeyValuePairResource>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllMovies([FromQuery]GetMovieListQuery query)
        {
            return Ok(await Mediator.Send(query));
        }

        [HttpPost]
        [ProducesResponseType(typeof(MovieResource), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> SaveNewGenre(CreateMovieCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
    }
}
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using CQRSExample.Application.Genres.Queries;
using Microsoft.AspNetCore.Mvc;
using CQRSExample.Application.Genres.Commands;
using CQRSExample.Application.Models;
using CQRSExample.Application.Genres.Commands.CreateGenre;
using CQRSExample.Application.Genres.Queries.GetGenreDetails;
using CQRSExample.Application.Genres.Commands.DeleteGenre;
using CQRSExample.Application.Genres.Commands.UpdateGenre;

namespace CQRSExample.WebAPI.Controllers
{
    [Route("/api/genres")]
    public class GenresController : BaseController
    {
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<KeyValuePairResource>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllGenres([FromQuery]GetGenreListQuery query)
        {
            return Ok(await Mediator.Send(query));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(IEnumerable<KeyValuePairResource>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetGenreDetail(int id)
        {
            return Ok(await Mediator.Send(new GetGenreDetailsQuery { GenreId = id }));
        }

        [HttpPost]
        [ProducesResponseType(typeof(KeyValuePairResource), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> SaveNewGenre(CreateGenreCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(KeyValuePairResource), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> DeleteGenre(int id)
        {
            return Ok(await Mediator.Send(new DeleteGenreCommand { Id = id }));
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(KeyValuePairResource), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> DeleteGenre([FromRoute]int id, [FromBody]UpdateGenreCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

    }
}
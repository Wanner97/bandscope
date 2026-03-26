using BandScope.Common.DTOs;
using BandScope.Logic.Interfaces;
using BandScope.Server.Mapper;
using Microsoft.AspNetCore.Mvc;

namespace BandScope.Server.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class GenreController : ControllerBase
    {
        private readonly IGenreLogic _genreLogic;

        public GenreController(IGenreLogic genreLogic)
        {
            _genreLogic = genreLogic;
        }

        [HttpGet]
        public ActionResult GetAllGenres()
        {
            var genreModelList = _genreLogic.GetAllGenres();
            var genreDtoList = new List<GenreDto>();

            foreach (var genre in genreModelList)
            {
                genreDtoList.Add(ModelToDtoMapper.MapToGenreDto(genre));
            }

            return Ok(genreDtoList);
        }

        [HttpPost("new")]
        public ActionResult CreateGenre(GenreDto genreDto)
        {
            var genreModel = DtoToModelMapper.MapToGenreModel(genreDto);

            var createdGenre = _genreLogic.CreateGenre(genreModel);

            var returnValue = ModelToDtoMapper.MapToGenreDto(createdGenre);

            return Created(string.Empty, returnValue);
        }
    }
}

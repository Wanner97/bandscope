using BandScope.Common.DTOs;
using BandScope.Logic.Interfaces;
using BandScope.Server.External;
using BandScope.Server.Mapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BandScope.Server.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlbumController : ControllerBase
    {
        private readonly IAlbumLogic _albumLogic;
        private readonly ITheAudioDbClient _theAudioDbClient;

        public AlbumController(IAlbumLogic albumLogic, ITheAudioDbClient theAudioDbClient)
        {
            _albumLogic = albumLogic;
            _theAudioDbClient = theAudioDbClient;
        }

        [Authorize]
        [HttpPost("new")]
        public ActionResult CreateAlbum(AlbumDto albumDto)
        {
            var albumModel = DtoToModelMapper.MapToNewAlbumModel(albumDto);

            var createdAlbum = _albumLogic.CreateAlbum(albumModel);

            var returnValue = ModelToDtoMapper.MapToAlbumDto(createdAlbum);

            return Created(string.Empty, returnValue);
        }

        [HttpGet("{artistId:int}")]
        public ActionResult GetAlbumsByArtist(int artistId)
        {
            var albumModels = _albumLogic.GetAlbumsByArtist(artistId);

            var albumDtos = new List<AlbumDto>();

            foreach (var album in albumModels)
            {
                albumDtos.Add(ModelToDtoMapper.MapToAlbumDto(album));
            }

            return Ok(albumDtos);
        }

        [HttpGet("audiodb/{audioDbId:int}")]
        public async Task<ActionResult> GetAlbumsByAudioDbId(int audioDbId)
        {
            var audioDbAlbums = await _theAudioDbClient.SearchAlbumsByArtistAsync(audioDbId);

            var albumDtos = new List<AlbumDto>();

            foreach (var theAudioDbAlbum in audioDbAlbums)
            {
                albumDtos.Add(AudioDbResultToDtoMapper.MapToNewAlbumDto(theAudioDbAlbum));
            }

            return Ok(albumDtos);
        }
    }
}

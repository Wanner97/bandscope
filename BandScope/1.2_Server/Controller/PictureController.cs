using BandScope.Common.DTOs;
using BandScope.Logic.Interfaces;
using BandScope.Server.Mapper;
using Microsoft.AspNetCore.Mvc;

namespace BandScope.Server.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class PictureController : ControllerBase
    {
        private readonly IPictureLogic _pictureLogic;

        public PictureController(IPictureLogic pictureLogic)
        {
            _pictureLogic = pictureLogic;
        }

        [HttpPost("new")]
        public ActionResult CreatePicture(NewPictureDto pictureDto)
        {
            var pictureModel = DtoToModelMapper.MapToNewPictureModel(pictureDto);

            var createdPicture = _pictureLogic.CreatePicture(pictureModel);

            var returnValue = ModelToDtoMapper.MapToPictureIdDto(createdPicture);

            return Created(string.Empty, returnValue);
        }
    }
}

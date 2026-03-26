using BandScope.Common.DTOs;
using BandScope.Logic.Interfaces;
using BandScope.Server.Mapper;
using Microsoft.AspNetCore.Mvc;

namespace BandScope.Server.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class StyleController : ControllerBase
    {
        private readonly IStyleLogic _styleLogic;

        public StyleController(IStyleLogic styleLogic)
        {
            _styleLogic = styleLogic;
        }

        [HttpGet]
        public ActionResult GetAllStyles()
        {
            var styleModelList = _styleLogic.GetAllStyles();
            var styleDtoList = new List<StyleDto>();

            foreach (var style in styleModelList)
            {
                styleDtoList.Add(ModelToDtoMapper.MapToStyleDto(style));
            }

            return Ok(styleDtoList);
        }

        [HttpPost("new")]
        public ActionResult CreateStyle(StyleDto styleDto)
        {
            var styleModel = DtoToModelMapper.MapToStyleModel(styleDto);

            var createdStyle = _styleLogic.CreateStyle(styleModel);

            var returnValue = ModelToDtoMapper.MapToStyleDto(createdStyle);

            return Created(string.Empty, returnValue);
        }
    }
}

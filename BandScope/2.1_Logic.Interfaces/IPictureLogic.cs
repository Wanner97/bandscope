using BandScope.Common.Models;

namespace BandScope.Logic.Interfaces
{
    public interface IPictureLogic
    {
        Picture CreatePicture(Picture picture);
        Picture GetPictureById(int pictureId);
    }
}

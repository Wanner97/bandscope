using BandScope.Common.Models;

namespace BandScope.DataAccess.Interfaces
{
    public interface IPictureDataAccess
    {
        Picture CreatePicture(Picture picture);
        Picture GetPictureById(int pictureId);
    }
}

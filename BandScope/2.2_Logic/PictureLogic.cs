using BandScope.Common.Models;
using BandScope.DataAccess.Interfaces;
using BandScope.Logic.Interfaces;
using BandScope.Logic.Validators;
using FluentValidation;

namespace BandScope.Logic
{
    public class PictureLogic : IPictureLogic
    {
        private readonly IPictureDataAccess _pictureDataAccess;

        public PictureLogic(IPictureDataAccess pictureDataAccess)
        {
            _pictureDataAccess = pictureDataAccess;
        }

        public Picture CreatePicture(Picture picture)
        {
            new PictureValidator(false).ValidateAndThrow(picture);

            return _pictureDataAccess.CreatePicture(picture);
        }

        public Picture GetPictureById(int pictureId)
        {
            if (pictureId <= 0)
            {
                throw new ValidationException("pictureId must be greater than 0.");
            }

            return _pictureDataAccess.GetPictureById(pictureId);
        }
    }
}

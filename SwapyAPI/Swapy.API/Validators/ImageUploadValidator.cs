using FluentValidation;
using Swapy.Common.DTO.Products.Requests.Commands;

namespace Swapy.API.Validators
{
    public class ImageUploadValidator : AbstractValidator<UploadImageCommandDTO>
    {
        public ImageUploadValidator()
        {
            RuleFor(x => x.ProductId)
            .NotEmpty()
                .WithMessage("ProductId field is required")
            .Matches(@"^[0-9A-Fa-f]{8}[-]?([0-9A-Fa-f]{4}[-]?){3}[0-9A-Fa-f]{12}$")
                .WithMessage("ProductId field has invalid format")
            .WithErrorCode("InvalidProductIdFormat");

            RuleForEach(x => x.Paths)
            .NotEmpty()
                .WithMessage("Paths field is required")
            .Matches(@"^[0-9A-Fa-f]{8}[-]?([0-9A-Fa-f]{4}[-]?){3}[0-9A-Fa-f]{12}.(?:jpg|jpeg|png)$")
                .WithMessage("Paths field has invalid format")
            .WithErrorCode("InvalidPathsFormat");
        }
    }
}

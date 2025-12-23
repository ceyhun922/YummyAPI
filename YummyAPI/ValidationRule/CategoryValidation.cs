using FluentValidation;
using YummyAPI.DTOs.CategoryDTO;

namespace YummyAPI.ValidationRule
{
    public class CategoryValidation : AbstractValidator<CreateCategoryDto>
    {
        public CategoryValidation()
        {
            RuleFor(x=>x.CategoryName).NotEmpty().WithMessage("Boş Ola bilmez");
        }
    }
}
using FluentValidation;
using Kodlama.io.Devs.Application.Features.ProgrammingLanguages.Commands.DeleteProgrammingLanguage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kodlama.io.Devs.Application.Features.ProgrammingLanguages.Queries.GetByIdProgrammingLanguage
{
    public class GetByIdProgrammingLanguageQueryValidator : AbstractValidator<GetByIdProgrammingLanguageQuery>
    {
        public GetByIdProgrammingLanguageQueryValidator()
        {
            RuleFor(c => c.Id).NotNull();
            RuleFor(c => c.Id).NotEqual(0);
        }
    }
}
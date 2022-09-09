using Core.CrossCuttingConcerns.Exceptions;
using Kodlama.io.Devs.Application.Features.ProgrammingTechnologies.Rules;
using Kodlama.io.Devs.Application.Services.Repositories;
using Kodlama.io.Devs.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kodlama.io.Devs.Application.Features.ProgrammingTechnologies.Commands.DeleteProgrammingTechnology
{
    public class DeleteProgrammingTechnologyCommand : IRequest<Unit>
    {
        public int Id { get; set; }

        public class DeleteProgrammingTechnologyHandler : IRequestHandler<DeleteProgrammingTechnologyCommand, Unit>
        {
            private readonly IProgrammingTechnologyRepository _programmingTechnologyRepository;
            private readonly ProgrammingTechnologyBusinessRules _programmingTechnologyBusiness;

            public DeleteProgrammingTechnologyHandler(IProgrammingTechnologyRepository programmingTechnologyRepository, ProgrammingTechnologyBusinessRules programmingTechnologyBusinessRules)
            {
                _programmingTechnologyRepository = programmingTechnologyRepository;
                _programmingTechnologyBusiness = programmingTechnologyBusinessRules;
            }

            public async Task<Unit> Handle(DeleteProgrammingTechnologyCommand request, CancellationToken cancellationToken)
            {
                ProgrammingTechnology programmingTechnology = await _programmingTechnologyRepository.GetAsync(b => b.Id == request.Id);

                _programmingTechnologyBusiness.ProgrammingTechnologyShouldExistWhenRequested(programmingTechnology);

                var deletedResult = await _programmingTechnologyRepository.DeleteAsync(programmingTechnology);

                if (deletedResult == null) throw new DatabaseExecutionException("An error occurred executing the database");

                return Unit.Value;
            }
        }
    }
}

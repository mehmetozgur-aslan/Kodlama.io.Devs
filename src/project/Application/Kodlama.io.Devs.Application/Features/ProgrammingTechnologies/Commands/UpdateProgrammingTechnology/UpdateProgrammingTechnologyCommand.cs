using AutoMapper;
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

namespace Kodlama.io.Devs.Application.Features.ProgrammingTechnologies.Commands.UpdateProgrammingTechnology
{
    public class UpdateProgrammingTechnologyCommand : IRequest<Unit>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ProgrammingLanguageId { get; set; }

        public class UpdateProgrammingTechnologyHandler : IRequestHandler<UpdateProgrammingTechnologyCommand, Unit>
        {
            private readonly IProgrammingTechnologyRepository _programmingTechnologyRepository;
            private readonly IMapper _mapper;
            private readonly ProgrammingTechnologyBusinessRules _programmingTechnologyBusiness;

            public UpdateProgrammingTechnologyHandler(IProgrammingTechnologyRepository programmingTechnologyRepository, IMapper mapper, ProgrammingTechnologyBusinessRules programmingTechnologyBusinessRules)
            {
                _programmingTechnologyRepository = programmingTechnologyRepository;
                _mapper = mapper;
                _programmingTechnologyBusiness = programmingTechnologyBusinessRules;
            }

            public async Task<Unit> Handle(UpdateProgrammingTechnologyCommand request, CancellationToken cancellationToken)
            {
                ProgrammingTechnology programmingTechnology = await _programmingTechnologyRepository.GetAsync(b => b.Id == request.Id);

                _programmingTechnologyBusiness.ProgrammingTechnologyShouldExistWhenRequested(programmingTechnology);

                programmingTechnology.Name = request.Name;
                programmingTechnology.ProgrammingLanguageId = request.ProgrammingLanguageId;

                ProgrammingTechnology updatedProgrammingLanguage = await _programmingTechnologyRepository.UpdateAsync(programmingTechnology);

                if (updatedProgrammingLanguage == null) throw new DatabaseExecutionException("An error occurred executing the database");

                return Unit.Value;
            }
        }
    }
}

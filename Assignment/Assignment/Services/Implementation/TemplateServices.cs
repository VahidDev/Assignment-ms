using Assignment.Services.Abstraction;
using AutoMapper;
using DomainModels.Dtos;
using DomainModels.Models.Entities;
using Repository.RepositoryServices.Abstraction;

namespace Assignment.Services.Implementation
{
    public class TemplateServices : ITemplateServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public TemplateServices(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<bool> CreateAsync(IReadOnlyCollection<CreateTemplateDto>templates)
        {
            await _unitOfWork.TemplateRepository
                .AddRangeAsync(_mapper.Map<IReadOnlyCollection<Template>>(templates));
            await _unitOfWork.CompleteAsync();
            return true;
        }

        public async Task<bool> UpdateAsync(UpdateTemplateDto templateDto)
        {
            Template template = _mapper.Map<Template>(templateDto);
            _unitOfWork.TemplateRepository.Update(template);
            await _unitOfWork.CompleteAsync();
            return true;
        }
    }
}

using Assignment.Factory;
using Assignment.Services.Abstraction;
using AutoMapper;
using DomainModels.Dtos;
using DomainModels.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Repository.RepositoryServices.Abstraction;

namespace Assignment.Services.Implementation
{
    public class TemplateServices : ITemplateServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IJsonFactory _jsonFactory;
        public TemplateServices(IUnitOfWork unitOfWork, IMapper mapper
            , IJsonFactory jsonFactory)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _jsonFactory = jsonFactory; 
        }
        public async Task<JsonResult> CreateAsync(CreateTemplateDto templateDto)
        {
            if (await _unitOfWork.TemplateRepository.AnyAsync(t=>t.Name == templateDto.Name))
            {
                return _jsonFactory.CreateJson(StatusCodes.Status400BadRequest,
                    $"The template named {templateDto.Name} already exists");
            }
            foreach (CreateFilterDto filterDto in templateDto.Filters)
            {
                if(filterDto.Value.Length== 0)
                {
                    return _jsonFactory.CreateJson(StatusCodes.Status400BadRequest
                        ,$"No value was provided for filter: {filterDto.Requirement}");
                }
            }
            await _unitOfWork.TemplateRepository
                .AddAsync(_mapper.Map<Template>(templateDto));
            await _unitOfWork.CompleteAsync();
            return _jsonFactory.CreateJson(StatusCodes.Status200OK);
        }

        public async Task<JsonResult> DeleteAsync(int id)
        {
            Template template = await _unitOfWork.TemplateRepository
                .GetByIdAsync(id, new List<string> { nameof(Filter) + "s" });
            if (template == null)
                return _jsonFactory.CreateJson(StatusCodes.Status404NotFound);
            _unitOfWork.TemplateRepository.Delete(template);
            await _unitOfWork.CompleteAsync();
            return _jsonFactory.CreateJson(StatusCodes.Status200OK);
        }

        public async Task<JsonResult> GetAllTemplatesAsync()
        {
            return _jsonFactory.CreateJson(StatusCodes.Status200OK, 
                _mapper.Map<List<GetTemplateDto>>(await _unitOfWork.TemplateRepository
                .GetAllAsync(new List<string> { nameof(Filter) + "s" })));
        }

        public async Task<JsonResult> UpdateAsync(UpdateTemplateDto updatedTemplate)
        {
            Template dbTemplate = await _unitOfWork.TemplateRepository
                .GetTemplatesWithFiltersAsNoTrackingAsync(t => !t.IsDeleted
                && t.Id==updatedTemplate.Id);

            if (dbTemplate == null)
                return _jsonFactory.CreateJson(StatusCodes.Status404NotFound);

            ICollection<UpdateFilterDto> updatedFilters = updatedTemplate.Filters;
            Template template=_mapper.Map<Template>(updatedTemplate);
            foreach (Filter dbFilter in dbTemplate.Filters)
            {
                if (!updatedFilters.Any(f => f.Id == dbFilter.Id))
                {
                    dbFilter.IsDeleted=true;
                    template.Filters.Add(dbFilter);
                }
            }
            _unitOfWork.TemplateRepository.Update(template);
            await _unitOfWork.CompleteAsync();
            return _jsonFactory.CreateJson(StatusCodes.Status200OK);
        }
    }
}

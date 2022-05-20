using Assignment.Constants;
using Assignment.Factory;
using Assignment.Services.Abstraction;
using AutoMapper;
using DomainModels.Dtos;
using DomainModels.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Repository.RepositoryServices.Abstraction;
using System.Collections.Generic;

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
                return _jsonFactory.CreateJson(StatusCodes.Status400BadRequest
                    ,"The same named template already exists");
            }

            Template template = _mapper.Map<Template>(templateDto);
            template.Filters= new List<Filter>();
            foreach (CreateFilterDto filterDto in templateDto.Filters)
            {
                if(filterDto.Value.Length== 0)
                {
                    return _jsonFactory.CreateJson(400
                        ,$"No value was provided for filter: {filterDto.Requirement}");
                }
                Filter filter = _mapper.Map<Filter>(filterDto);
                if (filterDto.Value.Length == 1)
                {
                    filter.Value =filterDto.Value[0]+"";
                }
                else
                {
                    filter.Value = filterDto.Value[0]
                         + DbValueSeperatorConstants.TripleDashSeperator
                         + filterDto.Value[1];
                }
                template.Filters.Add(filter);
            }
            await _unitOfWork.TemplateRepository
                .AddAsync(template);
            await _unitOfWork.CompleteAsync();
            return _jsonFactory.CreateJson(StatusCodes.Status200OK);
        }

        public async Task<JsonResult> DeleteAsync(int id)
        {
            Template template = await _unitOfWork.TemplateRepository
                .GetByIdAsync(id, new List<string> { nameof(Filter) + "s" });
            if (template == null)
                return _jsonFactory.CreateJson(StatusCodes.Status200OK);
            _unitOfWork.TemplateRepository.Delete(template);
            await _unitOfWork.CompleteAsync();
            return _jsonFactory.CreateJson(StatusCodes.Status200OK);
        }

        public async Task<JsonResult> GetAllTemplatesAsync()
        {
            ICollection<Template> templates = (await _unitOfWork.TemplateRepository
                .GetAllAsync(new List<string> { nameof(Filter) + "s" })).ToList();
            List<GetTemplateDto> templateDtos = new();
            foreach (Template template in templates)
            {
                GetTemplateDto getTemplateDto = _mapper.Map<GetTemplateDto>(template);
                getTemplateDto.Filters = new List<GetFilterDto>();
                foreach (Filter filter in template.Filters)
                {
                    GetFilterDto filterDto =_mapper.Map<GetFilterDto>(filter);
                    filterDto.Value = filter.Value
                        .Split(DbValueSeperatorConstants.TripleDashSeperator);
                    getTemplateDto.Filters.Add(filterDto);
                }
                templateDtos.Add(getTemplateDto);
            }
            return _jsonFactory.CreateJson(StatusCodes.Status200OK,templateDtos);
        }

        public async Task<JsonResult> UpdateAsync(UpdateTemplateDto updatedTemplate)
        {
            Template dbTemplate = await _unitOfWork.TemplateRepository
                .GetTemplatesWithFiltersAsNoTrackingAsync(t => !t.IsDeleted);

            if (dbTemplate == null)
                return _jsonFactory.CreateJson(StatusCodes.Status404NotFound);

            ICollection<UpdateFilterDto> updatedFilters = updatedTemplate.Filters;

            List<Filter> deletedFilters = new();
            Template template=_mapper.Map<Template>(updatedTemplate);
            template.Filters=new List<Filter>();
            foreach (UpdateFilterDto filterDto in updatedFilters)
            {
                Filter filter=_mapper.Map<Filter>(filterDto);
                filter.Value = String
                    .Join(DbValueSeperatorConstants.TripleDashSeperator,filterDto.Value);
                template.Filters.Add(filter);
            }
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

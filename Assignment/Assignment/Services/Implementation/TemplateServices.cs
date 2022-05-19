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
                return _jsonFactory.CreateJson(StatusCodes.Status400BadRequest
                    ,"The same named template already exists");
            }
            await _unitOfWork.TemplateRepository
                .AddAsync(_mapper.Map<Template>(templateDto));
            await _unitOfWork.CompleteAsync();
            return _jsonFactory.CreateJson(StatusCodes.Status201Created);
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
            return _jsonFactory.CreateJson(StatusCodes.Status200OK,
                _mapper.Map<List<GetTemplateDto>>(await _unitOfWork.TemplateRepository
                .GetAllAsync(new List<string> { nameof(Filter) + "s" })));
        }

        public async Task<JsonResult> UpdateAsync(UpdateTemplateDto templateDto)
        {
            if (!await _unitOfWork.TemplateRepository
              .AnyAsync(t => t.Id == templateDto.Id))
                return _jsonFactory.CreateJson(StatusCodes.Status404NotFound);
            await _unitOfWork.TemplateRepository
                .UpdateWithFiltersAsync(_mapper.Map<Template>(templateDto));
            await _unitOfWork.CompleteAsync();
            return _jsonFactory.CreateJson(StatusCodes.Status200OK);
        }
    }
}

using Assignment.Constants.FileConstants;
using Assignment.Services.Abstraction;
using Assignment.Utilities.FileUtilities;
using Assignment.Utilities.ServicesUtilities.MapperUtilities;
using AutoMapper;
using DomainModels.Models.Entities;
using Repository.RepositoryServices.Abstraction;

namespace Assignment.Services.Implementation
{
    internal class RoleOfferServices : IRoleOfferServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IFileServices _fileServices;

        public RoleOfferServices(IUnitOfWork unitOfWork, IMapper mapper
            ,IFileServices fileServices)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _fileServices = fileServices;
        }
        public async Task<string> ValidateExcelFileThenWriteToDbAsync(IFormFile file)
        {
            if(!file.IsFileTypeSupported(FileTypeConstants.ExcelFileContentType))
            {
                return ($"{FileErrorMessageConstants.NotSupportedFile}: " +
                    $"{file.ContentType}");
            }
            // Get All RoleOffers from excel file
            ICollection<RoleOffer> excelRoleOffers = _fileServices
                .ReadCollectionFromExcelFile<RoleOffer>(file);

            // the array of roleOffer ids given from the excel helps to filter db
            int[] roleOfferIds= excelRoleOffers.Select(r=>r.RoleOfferId).ToArray();

            // filter db by given roleOffer ids from excel
            List<RoleOffer> dbRoleOffers = _unitOfWork.RoleOfferRepository
                    .GetAllAsNoTracking(r => !r.IsDeleted
                    && roleOfferIds.Contains(r.RoleOfferId),
                    new List<string> 
                    { nameof(Venue), nameof(Location), nameof(JobTitle)
                    ,nameof(FunctionalArea) }).ToList();

            List<RoleOffer> list=new();
            foreach (RoleOffer newExcelRoleOffer in excelRoleOffers)
            {
                RoleOffer? dbRoleOffer = dbRoleOffers
                    .FirstOrDefault(r => r.RoleOfferId == newExcelRoleOffer.RoleOfferId);
                if (dbRoleOffer != null)
                {
                    // if it exists in db then we update
                    RoleOfferCustomMapper
                        .MapDbRoleOfferToExcelRoleOfferId
                        (ref dbRoleOffer, newExcelRoleOffer, _mapper);
                }
                list.Add(newExcelRoleOffer);
            }
            _unitOfWork.RoleOfferRepository.UpdateRange(list);
            await _unitOfWork.CompleteAsync();
            return "true";
        }
    }
}

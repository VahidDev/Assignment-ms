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
            ICollection<RoleOffer> roleOffers = _fileServices
                .ReadCollectionFromExcelFile<RoleOffer>(file);

            // the array of roleOffer ids given from the excel helps to filter db
            int[] roleOfferIds=roleOffers.Select(r=>r.RoleOfferId).ToArray();

            // filter db by given roleOffer ids from excel
            List<RoleOffer> dbRoleOffers = _unitOfWork.RoleOfferRepository
                    .GetAllAsNoTracking(r => !r.IsDeleted
                    && roleOfferIds.Contains(r.RoleOfferId),
                    new List<string> 
                    { nameof(Venue), nameof(Location), nameof(JobTitle)
                    ,nameof(FunctionalArea) }).ToList();

            List<RoleOffer> roleOfferList=new();
            foreach (RoleOffer newExcelRoleOffer in roleOffers)
            {
                RoleOffer? dbRoleOffer =dbRoleOffers.FirstOrDefault(r => !r.IsDeleted 
                    && r.RoleOfferId == newExcelRoleOffer.RoleOfferId);
                if (dbRoleOffer != null)
                {
                    // if it exists in db then we update
                    RoleOfferCustomMapper
                        .MapDbRoleOfferIdsToExcelRoleOfferIds
                        (ref dbRoleOffer, newExcelRoleOffer, _mapper);
                }
                roleOfferList.Add(newExcelRoleOffer);
            }
            _unitOfWork.RoleOfferRepository.UpdateRange(roleOfferList);
            await _unitOfWork.CompleteAsync();
            return "true";
        }
    }
}

using TestRentaCar.Buisness.Abstractions;
using TestRentaCar.Buisness.Dtos.Maintenance;
using TestRentaCar.Buisness.Helper;
using TestRentaCarDataAccess.Abstractions;
using TestRentaCarDataAccess.Entities;
using TestRentaCarDataAccess.Enums;
using TestRentaCarDataAccess.Model;
using TestRentaCarSln.Buisness.Dtos.Common;
using TestRentaCarSln.DataAccess.Abstractions.Base;

namespace TestRentaCar.Buisness.Implementations
{
    public class MaintenanceService : IMaintenanceService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMaintenanceRepository _maintenanceRepository;

        public MaintenanceService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _maintenanceRepository = _unitOfWork.GetRepository<IMaintenanceRepository>();
        }
        public async Task<GenericResponceModel<bool>> CreateAsync(PostMaintenanceDto postMaintenanceDto)
        {
            GenericResponceModel<bool> responce = new GenericResponceModel<bool>()
            {
                Data = false,
                StatusCode = 400,
                Message = new List<string>()
            };

            Maintenance maintenance = new()
            {
                RentalId = postMaintenanceDto.RentalId,
                MaintenanceDate = postMaintenanceDto.MaintenanceDate,
                TotalCost = postMaintenanceDto.TotalCost,
                InsuranceCoverage = postMaintenanceDto.InsuranceCoverage
            };

            await _maintenanceRepository.CreateAsync(maintenance);

            int effectedRow = await _unitOfWork.SaveAsync();

            if (effectedRow > 0)
            {
                responce.Data = true;
                responce.StatusCode = 201;
                responce.Message.Add("Bakım əlavə olundu.");
                return responce;
            }
            else
            {
                responce.Message.Add("Bakım əlavə olunarkən xəta baş verdi.");
                return responce;
            }
        }

        public async Task<GenericResponceModel<bool>> DeleteAsync(DeleteType deleteType, int id)
        {
            GenericResponceModel<bool> responceModel = new()
            {
                Data = false,
                StatusCode = 400,
                Message = new List<string>()
            };

            if (id <= 0)
            {
                responceModel.Message.Add("Id sıfırdan böyük olmalıdır.");
                return responceModel;
            }

            if (deleteType == 0)
            {
                responceModel.Message.Add("Deletetype-i qeyd et.");
                return responceModel;
            }
            else if (deleteType == DeleteType.SoftDelete) await _maintenanceRepository.SoftDeleteAsync(id);
            else if (deleteType == DeleteType.HardDelete) await _maintenanceRepository.HardDeleteAsync(id);

            int effectedRow = await _unitOfWork.SaveAsync();

            if (effectedRow > 0)
            {
                responceModel.Data = true;
                responceModel.StatusCode = 200;
                responceModel.Message.Add("Bakım silindi.");
                return responceModel;
            }

            responceModel.Message.Add("Bakım silinərkən xəta baş verdi.");
            return responceModel;
        }

        public async Task<GenericResponceModel<PaginatedResult<IEnumerable<GetMaintenanceDto>>>> GetAllAsync(PaginationRequest paginationRequest, int? rentalId, DateTime minMaintentanceDate, DateTime maxMaintenanceDate)
        {
            GenericResponceModel<PaginatedResult<IEnumerable<GetMaintenanceDto>>> model = new()
            {
                Data = null,
                StatusCode = 404,
                Message = new List<string>()
            };

            var filter = PredicateBuilder.True<Maintenance>();

            if (rentalId.HasValue && rentalId > 0) filter = filter.And(x => x.RentalId == rentalId);

            if(minMaintentanceDate != DateTime.MinValue) filter = filter.And(x => x.MaintenanceDate >= minMaintentanceDate);

            if (maxMaintenanceDate != DateTime.MinValue) filter = filter.And(x => x.MaintenanceDate <= maxMaintenanceDate);

            var maintenances = await _maintenanceRepository.GetMaintenancesAsync(paginationRequest, filter);

            if(!maintenances.Data.Any())
            {
                model.Message.Add("Bakımlar tapılmadı.");
                return model;
            }

            model.Data = new PaginatedResult<IEnumerable<GetMaintenanceDto>>()
            {
                Data = maintenances.Data.Select(x => new GetMaintenanceDto()
                {
                    Id = x.Id,
                    RentalId = x.RentalId,
                    MaintenanceDate = x.MaintenanceDate,
                    TotalCost = x.TotalCost,
                    InsuranceCoverage = x.InsuranceCoverage
                }),
                PageNum = paginationRequest.PageNumber,
                PageSize = paginationRequest.PageSize,
                TotalCount = maintenances.TotalCount
            };
            model.StatusCode = 200;
            model.Message.Add("Bakımlar tapıldı.");
            return model;

        }

        public async Task<GenericResponceModel<GetMaintenanceDto>> GetByIdAsync(int id)
        {
            GenericResponceModel<GetMaintenanceDto> model = new()
            {
                Data = null,
                StatusCode = 404,
                Message = new List<string>()
            };

            if(id <= 0)
            {
                model.Message.Add("Id sıfırdan böyük olmalıdır.");
                return model;
            }

            Maintenance maintenance = await _maintenanceRepository.GetByIdAsync(id);

            if(maintenance is null)
            {
                model.Message.Add("Bakım tapılmadı.");
                return model;
            }

            GetMaintenanceDto getMaintenanceDto = new()
            {
                Id = maintenance.Id,
                RentalId = maintenance.RentalId,
                MaintenanceDate = maintenance.MaintenanceDate,
                TotalCost = maintenance.TotalCost,
                InsuranceCoverage = maintenance.InsuranceCoverage
            };

            model.Data = getMaintenanceDto;
            model.StatusCode = 200;
            model.Message.Add("Bakım tapıldı.");
            return model;

        }

        public async Task<GenericResponceModel<bool>> UpdateAsync(UpdateMaintenanceDto updateMaintenanceDto)
        {
            GenericResponceModel<bool> model = new()
            {
                Data = false,
                StatusCode = 400,
                Message = new List<string>()
            };

            Maintenance maintenance = await _maintenanceRepository.GetByIdAsync(updateMaintenanceDto.Id);

            if(maintenance is null)
            {
                model.Message.Add("Bakım tapılmadı.");
                return model;
            }

            maintenance.InsuranceCoverage = updateMaintenanceDto.InsuranceCoverage;
            maintenance.MaintenanceDate = updateMaintenanceDto.MaintenanceDate;
            maintenance.TotalCost = updateMaintenanceDto.TotalCost;

            _maintenanceRepository.Update(maintenance);

            int effectedRow = await _unitOfWork.SaveAsync();

            if(effectedRow > 0)
            {
                model.StatusCode = 200;
                model.Data = true;
                model.Message.Add("Bakım yeniləndi.");
                return model;
            }

            model.Message.Add("Bakım yenilənərkən xəta baş verdi.");
            return model;
        }
    }
}

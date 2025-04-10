using AutoMapper;
using TestRentaCar.Buisness.Abstractions;
using TestRentaCar.Buisness.Dtos.Insurance;
using TestRentaCar.Buisness.Helper;
using TestRentaCarDataAccess.Abstractions;
using TestRentaCarDataAccess.Entities;
using TestRentaCarDataAccess.Enums;
using TestRentaCarDataAccess.Model;
using TestRentaCarSln.Buisness.Dtos.Common;
using TestRentaCarSln.DataAccess.Abstractions.Base;

namespace TestRentaCar.Buisness.Implementations
{
    public class InsuranceSevice : IInsuranceService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IInsuranceRepository _insuranceRepository;
        private readonly IMapper _mapper;

        public InsuranceSevice(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _insuranceRepository = _unitOfWork.GetRepository<IInsuranceRepository>();
        }

        public async Task<GenericResponceModel<bool>> CreateAsync(PostInsuranceDto postInsuranceDto)
        {
            GenericResponceModel<bool> model = new()
            {
                Data = false,
                StatusCode = 400,
                Message = new List<string>()
            };
            Insurance insurance = new()
            {
                PolicyName =postInsuranceDto.CarInsuranceType.ToString(),
                Price = postInsuranceDto.Price
            };

            await _insuranceRepository.CreateAsync(insurance);

            int effectedRow = await _unitOfWork.SaveAsync();

            if (effectedRow > 0)
            {
                model.Data = true;
                model.StatusCode = 201;
                model.Message.Add("created successfully");
                return model;
            }

            model.Message.Add(model.Data + " not created");
            return model;
        }

        public async Task<GenericResponceModel<bool>> DeleteAsync(int id, DeleteType deleteType)
        {
            GenericResponceModel<bool> model = new()
            {
                Data = false,
                StatusCode = 400,
                Message = new List<string>()

            };

            if (id <= 0)
            {
                model.Message.Add("Id is not valid");
                return model;
            }

            if (deleteType == 0)
            {
                model.Message.Add("deletetype not valid");
                return model;
            }
            if (deleteType == DeleteType.SoftDelete)
            {
                await _insuranceRepository.SoftDeleteAsync(id);
            }
            if (deleteType == DeleteType.HardDelete)
            {
                await _insuranceRepository.HardDeleteAsync(id);
            }

            int effectedRow = await _unitOfWork.SaveAsync();

            if (effectedRow > 0)
            {
                model.Data = true;
                model.StatusCode = 200;
                model.Message.Add("Insurance deleted successfully");
                return model;
            }

            model.Message.Add("Insurance not deleted");
            return model;
        }

        public async Task<GenericResponceModel<PaginatedResult<IEnumerable<GetInsuranceDto>>>> GetAllAsync(PaginationRequest paginationRequest, CarInsuranceType? carInsuranceType, int? minPrice, int? maxPrice)
        {

            GenericResponceModel<PaginatedResult<IEnumerable<GetInsuranceDto>>> model = new()
            {
                Data = null,
                StatusCode = 404,
                Message = new List<string>()
            };

            var filter = PredicateBuilder.True<Insurance>();

            if (carInsuranceType is not null && carInsuranceType != 0)
                filter = filter.And(x => x.PolicyName == carInsuranceType.ToString());
            if (minPrice.HasValue && minPrice > 0)
                filter = filter.And(x => x.Price >= minPrice);
            if (maxPrice.HasValue && maxPrice > 0)
                filter = filter.And(x => x.Price <= maxPrice);

            var data = await _insuranceRepository.GetInsurances(paginationRequest, filter);

            if(!data.Data.Any())
            {
                model.Message.Add("Data doesnt found");
                return model;
            }

            IEnumerable<GetInsuranceDto> getInsuranceDtos = _mapper.Map<IEnumerable<GetInsuranceDto>>(data.Data);

            model.Data = new PaginatedResult<IEnumerable<GetInsuranceDto>>()
            {
                TotalCount = data.TotalCount,
                PageNum = paginationRequest.PageNumber,
                PageSize = paginationRequest.PageSize,
                Data = getInsuranceDtos
            };
            model.StatusCode = 200;
            model.Message.Add("Data found");
            return model;

        }

        public async Task<GenericResponceModel<GetInsuranceDto>> GetById(int id)
        {
            GenericResponceModel<GetInsuranceDto> model = new()
            {
                Data = null,
                StatusCode = 404,
                Message = new List<string>()
            };

            Insurance insurance = await _insuranceRepository.GetByIdAsync(id);

            if (insurance is null)
            {
                model.Message.Add("Insurance not found");
                return model;
            }

            GetInsuranceDto getInsuranceDto = _mapper.Map<GetInsuranceDto>(insurance);

            model.Data = getInsuranceDto;
            model.StatusCode = 200;
            model.Message.Add("Insurance found");
            return model;

        }

        public async Task<GenericResponceModel<bool>> UpdateAsync(UpdateInsuranceDto updateInsuranceDto)
        {
            GenericResponceModel<bool> model = new()
            {
                Data = false,
                StatusCode = 400,
                Message = new List<string>()
            };

            Insurance insurance = await _insuranceRepository.GetByIdAsync(updateInsuranceDto.Id);

            if(insurance is null)
            {
                model.StatusCode = 404;
                model.Message.Add("Insurance not found");
                return model;
            }

            insurance.PolicyName = updateInsuranceDto.CarInsuranceType.ToString();
            insurance.Price = updateInsuranceDto.Price;

            _insuranceRepository.Update(insurance);
            int effectedRow = await _unitOfWork.SaveAsync();

            if(effectedRow > 0)
            {
                model.Data = true;
                model.StatusCode = 200;
                model.Message.Add("Insurance updated successfully");
                return model;
            }
            model.Message.Add("Insurance not updated");
            return model;
        }
    }
}

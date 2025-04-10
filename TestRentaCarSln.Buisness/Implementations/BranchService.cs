using AutoMapper;
using TestRentaCar.Buisness.Abstractions;
using TestRentaCar.Buisness.Dtos.Branch;
using TestRentaCarDataAccess.Abstractions;
using TestRentaCarDataAccess.Entities;
using TestRentaCarDataAccess.Enums;
using TestRentaCarDataAccess.Model;
using TestRentaCarSln.Buisness.Dtos.Common;
using TestRentaCarSln.DataAccess.Abstractions.Base;

namespace TestRentaCar.Buisness.Implementations
{
    public class BranchService : IBranchService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBranchRepository _branchRepository;
        private readonly IMapper _mapper;

        public BranchService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _branchRepository = _unitOfWork.GetRepository<IBranchRepository>();
            _mapper = mapper;
        }

        public async Task<GenericResponceModel<PostBranchDto>> CreateAsync(PostBranchDto postBranchDto)
        {
            GenericResponceModel<PostBranchDto> model = new()
            {
                Data = null,
                StatusCode = 400,
                Message = new List<string>()
            };

            Branch branch = new Branch()
            {
                Name = postBranchDto.Name,
                Address = postBranchDto.Address
            };

            await _branchRepository.CreateAsync(branch);

            int effectedRow = await _unitOfWork.SaveAsync();

            if(effectedRow > 0)
            {
                model.Data = postBranchDto;
                model.StatusCode = 201;
                model.Message.Add("Branch created successfully");
                return model;
            }

            model.Message.Add("Branch not created");
            return model;
        }

        public async Task<GenericResponceModel<bool>> DeleteAsync(DeleteType deleteType ,int id)
        {
            GenericResponceModel<bool> model = new()
            {
                Data = false,
                StatusCode = 400,
                Message = new List<string>()
            };

            if (id <= 0)
                return model;

            if (deleteType == DeleteType.SoftDelete)
                await _branchRepository.SoftDeleteAsync(id);
            else if (deleteType == DeleteType.HardDelete)
                await _branchRepository.HardDeleteAsync(id);
            else if(deleteType == 0)
            {
                model.Message.Add("Delete type not selected");
            }

            int effectedRow = await _unitOfWork.SaveAsync();

            if(effectedRow > 0)
            {
                model.Data = true;
                model.StatusCode = 200;
                model.Message.Add("Branch deleted successfully");
                return model;
            }

            model.Message.Add("Branch not deleted");
            return model;
        }

        public async Task<GenericResponceModel<PaginatedResult<IEnumerable<GetBranchDto>>>> GetAllAsync(PaginationRequest paginationRequest)
        {
            GenericResponceModel<PaginatedResult<IEnumerable<GetBranchDto>>> model = new()
            {
                Data = null,
                StatusCode = 404,
                Message = new List<string>()
            };

            var data = await _branchRepository.GetBranchsAsync(paginationRequest);

            if (!data.Data.Any())
            {
                model.Message.Add("Branch not found");
                return model;
            }

            IEnumerable<GetBranchDto> getBranchDtos = _mapper.Map<IEnumerable<GetBranchDto>>(data.Data);

            model.Data = new PaginatedResult<IEnumerable<GetBranchDto>>()
            {
                Data = getBranchDtos,
                TotalCount = data.TotalCount,
                PageNum = paginationRequest.PageNumber,
                PageSize = paginationRequest.PageSize
            };
            model.StatusCode = 200;
            model.Message.Add("Branch found");
            return model;

        }

        public async Task<GenericResponceModel<GetBranchDto>> GetByIdAsync(int id)
        {
            GenericResponceModel<GetBranchDto> model = new()
            {
                Data = null,
                StatusCode = 404,
                Message = new List<string>()
            };

            if (id <= 0)
            {
                model.Message.Add("Id not selected");
                return model;
            }

            Branch branch = await _branchRepository.GetByIdAsync(id);

            if (branch is null)
            {
                model.Message.Add("Branch not found");
                return model;
            }
            GetBranchDto getBranchDto = new()
            {
                Id = branch.Id,
                Name = branch.Name,
                Address = branch.Address
            };

            model.Data = getBranchDto;
            model.StatusCode = 200;
            model.Message.Add("Branch found");
            return model;

        }

        public async Task<GenericResponceModel<bool>> UpdateAsync(UpdateBranchDto updateBranchDto)
        {
            GenericResponceModel<bool> model = new()
            {
                Data = false,
                StatusCode = 400,
                Message = new List<string>()
            };

            Branch branch = await _branchRepository.GetByIdAsync(updateBranchDto.Id);

            if (branch is null)
            {
                model.Message.Add("Branch not found");
                return model;
            }
            branch.Name = updateBranchDto.Name;
            branch.Address = updateBranchDto.Address;

            _branchRepository.Update(branch);

            int effectedRow = await _unitOfWork.SaveAsync();

            if(effectedRow > 0)
            {
                model.Data = true;
                model.StatusCode = 200;
                model.Message.Add("Branch updated");
                return model;
            }

            model.Message.Add("Branch not updated");
            return model;
        }
    }
}

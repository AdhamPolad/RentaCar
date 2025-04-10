using AutoMapper;
using TestRentaCar.Buisness.Abstractions;
using TestRentaCar.Buisness.Dtos.Brand;
using TestRentaCarDataAccess.Model;
using TestRentaCarSln.Buisness.Dtos.Brand;
using TestRentaCarSln.Buisness.Dtos.Common;
using TestRentaCarSln.DataAccess.Abstractions;
using TestRentaCarSln.DataAccess.Abstractions.Base;
using TestRentaCarSln.DataAccess.Entities;

namespace TestRentaCar.Buisness.Implementations
{
    public class BrandService : IBrandService
    {
        private readonly IBrandRepository _brandRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public BrandService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _brandRepository = _unitOfWork.GetRepository<IBrandRepository>();
        }

        public async Task<GenericResponceModel<PostBrandDto>> CreateAsync(PostBrandDto postBrandDto)
        {
            GenericResponceModel<PostBrandDto> model = new()
            {
                Data = null,
                StatusCode = 400,
                Message = new List<string>()
            };

            Brand brand = _mapper.Map<Brand>(postBrandDto);

            await _brandRepository.CreateAsync(brand);

            int effectedRow = await _unitOfWork.SaveAsync();

            if (effectedRow > 0)
            {
                model.Data = postBrandDto;
                model.StatusCode = 201;
                model.Message.Add(model.Data.Name + " created successfully");
                return model;
            }
            model.Message.Add("Brand not created");
            return model;
        }

        public async Task<GenericResponceModel<bool>> DeleteAsync(int id)
        {
            GenericResponceModel<bool> model = new()
            {
                Data = false,
                StatusCode = 400,
                Message = new List<string>()
            };

            await _brandRepository.SoftDeleteAsync(id);

            int effectedRow = await _unitOfWork.SaveAsync();

            if (effectedRow > 0)
            {
                model.Data = true;
                model.StatusCode = 200;
                model.Message.Add("Brand deleted successfully");
                return model;
            }
            model.Message.Add(model.Data + " not deleted");
            return model;
        }

        public async Task<GenericResponceModel<PaginatedResult<IEnumerable<GetBrandDto>>>> GetAllAsync(PaginationRequest paginationRequest)
        {
            GenericResponceModel<PaginatedResult<IEnumerable<GetBrandDto>>> model = new()
            {
                Data = null,
                StatusCode = 404,
                Message = new List<string>()
            };

            var brands = await _brandRepository.GetBrandsAsync(paginationRequest);

            if (brands.Data.Any())
            {
                IEnumerable<GetBrandDto> getBrandDtos = _mapper.Map<IEnumerable<GetBrandDto>>(brands.Data);
                model.Data = new PaginatedResult<IEnumerable<GetBrandDto>>()
                {
                    Data = getBrandDtos,
                    TotalCount = brands.TotalCount,
                    PageNum = paginationRequest.PageNumber,
                    PageSize = paginationRequest.PageSize
                };
                model.StatusCode = 200;
                model.Message.Add("Brands found");
                return model;
            }
            else
            {
                model.Message.Add("not found");
                return model;
            }

        }

        public async Task<GenericResponceModel<GetBrandDto>> GetByIdAsync(int id)
        {
            GenericResponceModel<GetBrandDto> model = new()
            {
                Data = null,
                StatusCode = 404,
                Message = new List<string>()
            };

            Brand brand = await _brandRepository.GetByIdAsync(id);

            GetBrandDto getBrandDto = _mapper.Map<GetBrandDto>(brand);

            if (getBrandDto is not null)
            {
                model.Data = getBrandDto;
                model.StatusCode = 200;
                model.Message.Add("Brand found");
                return model;
            }
            model.Message.Add("Brand not found");
            return model;
        }

        public async Task<GenericResponceModel<bool>> UpdateAsync(int id, UpdateBrandDto updateBrandDto)
        {
            GenericResponceModel<bool> model = new()
            {
                Data = false,
                StatusCode = 400,
                Message = new List<string>()
            };

            Brand brand = await _brandRepository.GetByIdAsync(id);

            brand.Name = updateBrandDto.Name;

            _brandRepository.Update(brand);

            int effectedRow = await _unitOfWork.SaveAsync();

            if (effectedRow > 0)
            {
                model.Data = true;
                model.StatusCode = 200;
                model.Message.Add("Brand updated successfully");
                return model;
            }
            model.Message.Add("Brand not updated");
            return model;
        }
    }
}

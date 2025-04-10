using AutoMapper;
using TestRentaCar.Buisness.Abstractions;
using TestRentaCar.Buisness.Dtos.Model;
using TestRentaCar.Buisness.Helper;
using TestRentaCarDataAccess.Abstractions;
using TestRentaCarDataAccess.Entities;
using TestRentaCarDataAccess.Model;
using TestRentaCarSln.Buisness.Dtos.Car;
using TestRentaCarSln.Buisness.Dtos.Common;
using TestRentaCarSln.DataAccess.Abstractions;
using TestRentaCarSln.DataAccess.Abstractions.Base;
using TestRentaCarSln.DataAccess.Entities;

namespace TestRentaCar.Buisness.Implementations
{
    public class ModelService : IModelService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IModelRepository _modelRepository;
        private readonly IMapper _mapper;
        private readonly IBrandRepository _brandRepository;

        public ModelService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _modelRepository = _unitOfWork.GetRepository<IModelRepository>();
            _brandRepository = _unitOfWork.GetRepository<IBrandRepository>();
        }

        public async Task<GenericResponceModel<Dictionary<string, int>>> GetModelsCountByBrand()
        {
            GenericResponceModel<Dictionary<string, int>> model = new()
            {
                Data = null,
                StatusCode = 404,
                Message = new List<string>()
            };

            var result = await _modelRepository.GetModelsCountByBrand();

            if (result is not null)
            {
                model.Data = result;
                model.StatusCode = 200;
                model.Message.Add("Models count by brand");
                return model;
            }

            model.Message.Add("Models not found");
            return model;
        }

        public async Task<GenericResponceModel<bool>> UpdateAsync(int id, UpdateModelDto updateModelDto)
        {
            GenericResponceModel<bool> model = new() { Data = false, StatusCode = 400, Message = new List<string>() };

            if (id <= 0)
            {
                model.Message.Add("Id not selected");
                return model;
            }

            Model carModel = await _modelRepository.GetByIdAsync(id);

            if (carModel is null)
            {
                model.Message.Add("Model not found");   
                return model;
            }

            carModel.Year = updateModelDto.Year;
            carModel.Name = updateModelDto.Name;
            carModel.BrandId = updateModelDto.BrandId;

            _modelRepository.Update(carModel);

            int effectedRow = await _unitOfWork.SaveAsync();

            if (effectedRow > 0)
            {
                model.Data = true;
                model.StatusCode = 200;
                model.Message.Add("Model updated");
                return model;
            }

            model.Message.Add("Model not updated");
            return model;

        }

        public async Task<GenericResponceModel<PostModelDto>> CreateAsync(PostModelDto postModelDto)
        {
            GenericResponceModel<PostModelDto> model = new()
            {
                Data = null,
                StatusCode = 400,
                Message = new List<string>()
            };

            var transaction = await _unitOfWork.BeginTransactionAsync();

            try
            {
                Model carModel = _mapper.Map<Model>(postModelDto);

                int brandId = postModelDto.BrandDto.Id;

                Brand brand = await _brandRepository.GetByIdAsync(brandId);

                if (brand is not null)
                {
                    model.Message.Add("Brand found");
                    carModel.Brand = brand;
                }

                if (brandId == 0)
                {
                    carModel.Brand = new Brand()
                    {
                        Name = postModelDto.BrandDto.Name
                    };
                }
                bool isCreated = await _modelRepository.CreateAsync(carModel);

                if (!isCreated)
                {
                    model.Message.Add("Model not created");
                    return model;
                }

                int effectedRow = await _unitOfWork.SaveAsync();
                await transaction.CommitAsync();

                if (effectedRow > 0)
                {
                    model.Data = postModelDto;
                    model.StatusCode = 200;
                    model.Message.Add("Model created");
                    return model;
                }

                return model;
            }
            catch (Exception exp)
            {
                model.Message.Add(exp.Message);
                await transaction.RollbackAsync();
                return model;
            }
            finally
            {
                await transaction.DisposeAsync();
            }
            
        }

        public async Task<GenericResponceModel<bool>> DeleteAsync(int id)
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

            await _modelRepository.SoftDeleteAsync(id);

            int effectedRow = await _unitOfWork.SaveAsync();

            if (effectedRow > 0)
            {
                model.Data = true;
                model.StatusCode = 200;
                model.Message.Add("Model deleted successfully");
                return model;
            }

            model.Message.Add("Model not deleted"); 
            return model;

        }

        public async Task<GenericResponceModel<GetModelDto>> GetByIdAsync(int id)
        {
            GenericResponceModel<GetModelDto> model = new()
            {
                Data = null,
                StatusCode = 404,
                Message = new List<string>()
            };

            if (id <= 0)
            {
                model.Message.Add("Id is not valid");
                return model;
            }

            var carModel = await _modelRepository.GetByIdAsync(id);

            if (carModel is null)
            {
                model.Message.Add("Model not found");
                return model;
            }

            GetModelDto getModelDto = _mapper.Map<GetModelDto>(carModel);
            model.Data = getModelDto;
            model.StatusCode = 200;
            model.Message.Add("Model found");
            return model;

        }

        public async Task<GenericResponceModel<PaginatedResult<IEnumerable<GetModelDto>>>> GetModelsAsync(PaginationRequest paginationRequest, int? brandId, int? minYear, int? maxYear)
        {
            GenericResponceModel<PaginatedResult<IEnumerable<GetModelDto>>> model = new()
            {
                Data = null,
                StatusCode = 404,
                Message = new List<string>()
            };

            var filter = PredicateBuilder.True<Model>();

            if (brandId.HasValue && brandId > 0)
            {
                filter = filter.And(x => x.BrandId == brandId);
            }
            if (minYear.HasValue && minYear > 0)
            {
                filter = filter.And(x => x.Year >= minYear);
            }
            if (maxYear.HasValue && maxYear > 0)
            {
                filter = filter.And(x => x.Year <= maxYear);
            }

            var models = await _modelRepository.GetModelsAsync(paginationRequest, filter);

            if (!models.Data.Any())
            {
                model.Message.Add("Models not found");
                return model;
            }

            IEnumerable<GetModelDto> getModelDtos = _mapper.Map<IEnumerable<GetModelDto>>(models.Data);

            model.Data = new PaginatedResult<IEnumerable<GetModelDto>>()
            {
                Data = getModelDtos,
                PageSize = paginationRequest.PageSize,
                PageNum = paginationRequest.PageNumber,
                TotalCount = models.TotalCount
            };
            model.StatusCode = 200;
            model.Message.Add("Models found");
            return model;

        }

    }
}

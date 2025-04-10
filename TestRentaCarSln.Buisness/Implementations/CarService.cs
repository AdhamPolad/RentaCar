using AutoMapper;
using TestRentaCar.Buisness.Dtos.Car;
using TestRentaCar.Buisness.Helper;
using TestRentaCarDataAccess.Abstractions;
using TestRentaCarDataAccess.Entities;
using TestRentaCarDataAccess.Enums;
using TestRentaCarDataAccess.Model;
using TestRentaCarSln.Buisness.Abstractions;
using TestRentaCarSln.Buisness.Dtos.Car;
using TestRentaCarSln.Buisness.Dtos.Common;
using TestRentaCarSln.DataAccess.Abstractions;
using TestRentaCarSln.DataAccess.Abstractions.Base;
using TestRentaCarSln.DataAccess.Entities;

namespace TestRentaCarSln.Buisness.Implementations
{
    public class CarService : ICarService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICarRepository _carRepository;
        private readonly IMapper _mapper;
        private readonly IEnginRepository _enginRepository;

        public CarService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _carRepository = _unitOfWork.GetRepository<ICarRepository>();
            _mapper = mapper;
            _enginRepository = _unitOfWork.GetRepository<IEnginRepository>();
        }

        public async Task<GenericResponceModel<bool>> UpdateAsync(int id, UpdateCarDto updateCarDto, CarCatagory carCatagory)
        {
            var transaction = await _unitOfWork.BeginTransactionAsync();

            GenericResponceModel<bool> model = new()
            {
                Data = false,
                StatusCode = 400,
                Message = new List<string>()
            };

            try
            {
                CarDetails carDetails = _mapper.Map<CarDetails>(updateCarDto.UpdateCarDetail);
                carDetails.Transmision = updateCarDto.UpdateCarDetail.Transmision;

                Car car = await _carRepository.GetByIdAsync(id);

                car.CarCatagoryId = Convert.ToInt32(carCatagory);
                car.InsuranceId = updateCarDto.InsuranceId;
                car.BranchId = updateCarDto.BranchId;
                car.LicensePlate = updateCarDto.LicensePlate;
                car.ModelId = updateCarDto.ModelId;
                car.PricePerDay = updateCarDto.PricePerDay;
                car.CarDetails = carDetails;

                bool isUpdated = _carRepository.Update(car);

                int effectedRow = await _unitOfWork.SaveAsync();
                await transaction.CommitAsync();

                if (effectedRow > 0)
                {
                    model.StatusCode = 200;
                    model.Data = true;
                    model.Message.Add("Car updated");
                    return model;
                }

                model.Message.Add("Car not updated");
                model.StatusCode = 400;
                return model;

            }
            catch (Exception exp)
            {
                model.Message.Add("Error occured" + exp.Message);
                await transaction.RollbackAsync();
                return model;
            }
            finally
            {
                await transaction.DisposeAsync();
            }
            
        }

        public async Task<GenericResponceModel<GetCarDto>> GetCheapestCar()
        {
            GenericResponceModel<GetCarDto> model = new()
            {
                Data = null,
                StatusCode = 404,
                Message = new List<string>()
            };

            Car? car = await _carRepository.GetCheapestCar();
            if (car is null)
            {
                model.Message.Add("not found");
                return model;
            }

            GetCarDto getCarDto = _mapper.Map<GetCarDto>(car);

            model.Data = getCarDto;
            model.StatusCode = 200;
            model.Message.Add(model.Data.LicensePlate + " found");
            return model;

        }

        public async Task<GenericResponceModel<PostCarDto>> CreateAsync(PostCarDto postCarDto)
        {
            var transaction = await _unitOfWork.BeginTransactionAsync();

            GenericResponceModel<PostCarDto> model = new()
            {
                Data = null,
                StatusCode = 400,
                Message = new List<string>()
            };

            try
            {
                int carCatagoryId = Convert.ToInt32(postCarDto.CarCatagory);

                CarDetails carDetails = _mapper.Map<CarDetails>(postCarDto.CarDetails);
                carDetails.Transmision = postCarDto.CarDetails.Transmision;

                int enginId = postCarDto.CarDetails.PostEnginDto.Id;

                if (enginId == 0)
                    carDetails.Engine = new Engine()
                    {
                        EnginType = postCarDto.CarDetails.PostEnginDto.EnginType,
                        EnginCapacity = postCarDto.CarDetails.PostEnginDto.EnginCapacity
                    };

                Engine engine = await _enginRepository.GetByIdAsync(enginId);

                if (engine is not null)
                    carDetails.EngineId = enginId;

                Car car = new()
                {
                    ModelId = postCarDto.ModelId,
                    PricePerDay = postCarDto.PricePerDay,
                    IsAvailable = postCarDto.IsAvailable,
                    LicensePlate = postCarDto.LicensePlate,
                    CarDetails = carDetails,
                    CarCatagoryId = carCatagoryId,
                    BranchId = postCarDto.BranchId,
                    InsuranceId = postCarDto.InsuranceId

                };

                bool isCreated = await _carRepository.CreateAsync(car);

                if (!isCreated)
                {
                    return model;
                }

                int effectedRow = await _unitOfWork.SaveAsync();
                await transaction.CommitAsync();

                if (effectedRow > 0)
                {
                    model.Message.Add("Car created");
                    model.Data = postCarDto;
                    model.StatusCode = 201;
                    return model;
                }

                model.Message.Add("Car not created");
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
                model.Message.Add("Id not selected");
                return model;
            }

            await _carRepository.SoftDeleteAsync(id);
            int effectedRow = await _unitOfWork.SaveAsync();

            if (effectedRow > 0)
            {
                model.StatusCode = 200;
                model.Data = true;
                model.Message.Add("Car deleted");
                return model;
            }
            else
            {
                model.Message.Add(model.Data + " not deleted");
                return model;
            }
        }


        public async Task<GenericResponceModel<PaginatedResult<IEnumerable<GetCarDto>>>> GetAviableCarsAsync(PaginationRequest paginationRequest,
                                                int? modelId, CarCatagory? carCatagory, int? enginId)
        {
            GenericResponceModel<PaginatedResult<IEnumerable<GetCarDto>>> model = new()
            {
                Data = null,
                StatusCode = 404,
                Message = new List<string>()
            };
            int? carCatagoryId = Convert.ToInt32(carCatagory);

            var filter = PredicateBuilder.True<Car>();
            filter = filter.And(c => c.IsAvailable);

            if (modelId.HasValue && modelId != 0)
            {
                filter = filter.And(c=> c.ModelId == modelId);
            }
            if (carCatagoryId.HasValue && carCatagoryId != 0)
            {
                filter = filter.And(c => c.CarCatagoryId == carCatagoryId);
            }
            if(enginId.HasValue && enginId != 0)
            {
                filter = filter.And(c => c.CarDetails.EngineId == enginId);
            }

            var cars = await _carRepository.GetCarsAsync(paginationRequest, filter);

            if (!cars.Data.Any())
            {
                model.Message.Add("not found");
                return model;
            }

            IEnumerable<GetCarDto> result = _mapper.Map<IEnumerable<GetCarDto>>(cars.Data);

            model.Data = new PaginatedResult<IEnumerable<GetCarDto>>()
            {
                Data = result,
                PageNum = paginationRequest.PageNumber,
                PageSize = paginationRequest.PageSize,
                TotalCount = cars.TotalCount
            };

            model.StatusCode = 200;
            model.Message.Add("Cars found");
            return model;

        }

        public async Task<GenericResponceModel<GetCarDto>> GetByIdAsync(int id)
        {
            GenericResponceModel<GetCarDto> model = new()
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

            Car car = await _carRepository.GetCarById(id);

            if (car is null)
            {
                model.Message.Add("Car not found");
                return model;
            }

            GetCarDto carDto = _mapper.Map<GetCarDto>(car);

            model.Data = carDto;
            model.StatusCode = 200;
            model.Message.Add("Car found");
            return model;

        }

    }
}

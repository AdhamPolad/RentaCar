using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestRentaCarSln.Buisness.Abstractions;
using TestRentaCarSln.Buisness.Dtos.Car;
using TestRentaCarSln.Buisness.Dtos.Common;
using TestRentaCarSln.DataAccess.Abstractions;
using TestRentaCarSln.DataAccess.Abstractions.Base;
using TestRentaCarSln.DataAccess.Entities;
using TestRentaCarSln.DataAccess.Implementations;
using TestRentaCarSln.DataAccess.Implementations.Base;

namespace TestRentaCarSln.Buisness.Implementations
{
    public class CarService : ICarService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICarRepository _carRepository;
        private readonly IMapper _mapper;

        public CarService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _carRepository = _unitOfWork.GetRepository<ICarRepository>();
            _mapper = mapper;
        }

        public async Task<GenericResponceModel<PostCarDto>> CreateAsync(PostCarDto postCarDto)
        {
            GenericResponceModel<PostCarDto> model = new()
            {
                Data = null,
                StatusCode = 400
            };
            Car car = _mapper.Map<Car>(postCarDto);

            await _carRepository.CreateAsync(car);
            int effectedRow = await _unitOfWork.SaveAsync();

            if(effectedRow > 0)
            {
                model.Data = postCarDto;
                model.StatusCode = 200;
            }
            return model;

        }

        public Task<GenericResponceModel<bool>> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<GenericResponceModel<IEnumerable<GetCarDto>>> GetAllAsync()
        {
            GenericResponceModel<IEnumerable<GetCarDto>> model = new()
            {
                Data = null,
                StatusCode = 404
            };

            var cars = await _carRepository.GetAll().ToListAsync();

            IEnumerable<GetCarDto> result = _mapper.Map<IEnumerable<GetCarDto>>(cars);

            if (cars.Any())
            {
                model.Data = result;
                model.StatusCode = 200;
            }
            return model;

        }

        public Task<GenericResponceModel<GetCarDto>> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}

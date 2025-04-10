using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TestRentaCar.Buisness.Abstractions;
using TestRentaCar.Buisness.Dtos.Customer;
using TestRentaCarDataAccess.Enums;
using TestRentaCarSln.Buisness.Dtos.Common;
using TestRentaCarSln.DataAccess.Abstractions;
using TestRentaCarSln.DataAccess.Abstractions.Base;
using TestRentaCarSln.DataAccess.Entities;

namespace TestRentaCar.Buisness.Implementations
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CustomerService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _customerRepository = _unitOfWork.GetRepository<ICustomerRepository>();
        }

        public async Task<GenericResponceModel<bool>> DeleteAsync(DeleteType deleteType, int id)
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
                model.Message.Add("Delete type is not valid");
                return model;
            }
            if (deleteType == DeleteType.SoftDelete)
                await _customerRepository.SoftDeleteAsync(id);
            else if (deleteType == DeleteType.HardDelete)
                await _customerRepository.HardDeleteAsync(id);

            int effectedRow = await _unitOfWork.SaveAsync();

            if (effectedRow > 0)
            {
                model.Data = true;
                model.StatusCode = 200;
                model.Message.Add("Customer deleted successfully");
                return model;
            }

            model.Message.Add("Customer not deleted");
            return model;
        }

        public async Task<GenericResponceModel<IEnumerable<GetCustomerDto>>> GetAllAsync()
        {
            GenericResponceModel<IEnumerable<GetCustomerDto>> model = new()
            {
                Data = null,
                StatusCode = 404,
                Message = new List<string>()
            };

            var customers = await _customerRepository.GetAll().ToListAsync();

            if (customers is null)
            {
                model.Message.Add("Customers not found");
                return model;
            }

            IEnumerable<GetCustomerDto> result = _mapper.Map<IEnumerable<GetCustomerDto>>(customers);

            if (customers != null)
            {
                model.Data = result;
                model.StatusCode = 200;
            }
            model.Message.Add("Customers found");
            return model;
        }

        public async Task<GenericResponceModel<GetCustomerDto>> GetByIdAsync(int id)
        {
            GenericResponceModel<GetCustomerDto> model = new()
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

            Customer customer = await _customerRepository.GetByIdAsync(id);

            if (customer is null) { model.Message.Add("Customer not found"); return model; }

            GetCustomerDto getCustomerDto = _mapper.Map<GetCustomerDto>(customer);

            model.Data = getCustomerDto;
            model.StatusCode = 200;
            model.Message.Add("Customer found");
            return model;

        }

        public async Task<GenericResponceModel<bool>> UpdateAsync(UpdateCustomerDto updateCustomerDto)
        {
            GenericResponceModel<bool> model = new()
            {
                Data = false,
                StatusCode = 400,
                Message = new List<string>()
            };

            Customer customer = await _customerRepository.GetByIdAsync(updateCustomerDto.Id);

            if (customer is null)
            {
                model.Message.Add("Customer not found");
                return model;
            }

            customer.FullName = updateCustomerDto.FullName;
            customer.PhoneNumber = updateCustomerDto.PhoneNumber;
            customer.DriverLisenceNumber = updateCustomerDto.DriverLisenceNumber;
            customer.Address = updateCustomerDto.Address;

            _customerRepository.Update(customer);

            int effectedRow = await _unitOfWork.SaveAsync();

            if (effectedRow > 0)
            {
                model.Data = true;
                model.StatusCode = 200;
                model.Message.Add("Customer updated");
                return model;
            }

            model.Message.Add("Customer not updated");
            return model;
        }



    }
}

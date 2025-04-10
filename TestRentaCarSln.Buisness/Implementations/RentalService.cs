using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Runtime.ConstrainedExecution;
using System.Security.Claims;
using TestRentaCar.Buisness.Abstractions;
using TestRentaCar.Buisness.Dtos.Rental;
using TestRentaCar.Buisness.Helper;
using TestRentaCarDataAccess.Abstractions;
using TestRentaCarDataAccess.Enums;
using TestRentaCarDataAccess.Model;
using TestRentaCarSln.Buisness.Dtos.Common;
using TestRentaCarSln.DataAccess.Abstractions;
using TestRentaCarSln.DataAccess.Abstractions.Base;
using TestRentaCarSln.DataAccess.Entities;

namespace TestRentaCar.Buisness.Implementations
{
    public class RentalService : IRentalService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IRentalRepository _rentalRepository;
        private readonly ICarRepository _carRepository;
        private readonly IPaymentRepository _paymentRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ICustomerRepository _customerRepository;
        private readonly ILogger<RentalService> _logger;

        public RentalService(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor, ILogger<RentalService> logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _rentalRepository = _unitOfWork.GetRepository<IRentalRepository>();
            _carRepository = _unitOfWork.GetRepository<ICarRepository>();
            _paymentRepository = _unitOfWork.GetRepository<IPaymentRepository>();
            _httpContextAccessor = httpContextAccessor;
            _customerRepository = _unitOfWork.GetRepository<ICustomerRepository>();
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<GenericResponceModel<RentalReponceDto>> UpdateAsync(UpdateRentalDto updateRentalDto)
        {
            GenericResponceModel<RentalReponceDto> model = new()
            {
                Data = new(),
                StatusCode = 400,
                Message = new List<string>()
            };
            var transaction = await _unitOfWork.BeginTransactionAsync();

            try
            {
                Rental rental = await _rentalRepository.GetByIdAsync(updateRentalDto.Id);

                if (rental == null || rental.Status != RentalStatus.Active.ToString())
                {
                    model.Data.Message = "İcarə tapılmadı və ya artıq tamamlanıb.";
                    return model;
                }

                string userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                Customer customer = await _customerRepository.GetByUserId(userId);

                if (rental.CustomerId != customer.Id)
                {
                    model.Data.Message = "Her bir user ancaq oz sifarisin update ede biler";
                    return model;
                }

                Car oldCar = await _carRepository.GetByIdAsync(rental.CarId);
                oldCar.IsAvailable = true;
                _carRepository.Update(oldCar);

                Car car = await _carRepository.GetByIdAsync(updateRentalDto.CarId);
                decimal dailyRate = car.PricePerDay;
                decimal totalPrice = CalculateTotalPrice(dailyRate, updateRentalDto.RentalDate, updateRentalDto.ReturnDate);
                car.IsAvailable = false;
                _carRepository.Update(car);

                rental.CarId = updateRentalDto.CarId;
                rental.RentalDate = updateRentalDto.RentalDate;
                rental.ReturnDate = updateRentalDto.ReturnDate;
                rental.TotalPrice = totalPrice;

                _rentalRepository.Update(rental);

                Payment payment = await _paymentRepository.GetByRentId(updateRentalDto.Id);

                payment.Amount = totalPrice;
                _paymentRepository.Update(payment);

                int effectedRow = await _unitOfWork.SaveAsync();
                await transaction.CommitAsync();

                if (effectedRow > 0)
                {
                    model.Data.Message = "Rental ugurla deyisdirlidi";
                    model.StatusCode = 200;
                    model.Message.Add("Rental updated");
                    return model;
                }

                model.Message.Add("Rental not updated");
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
                await _rentalRepository.SoftDeleteAsync(id);
            else if (deleteType == DeleteType.HardDelete)
                await _rentalRepository.HardDeleteAsync(id);

            int effectedRow = await _unitOfWork.SaveAsync();

            if (effectedRow > 0)
            {
                model.Data = true;
                model.StatusCode = 200;
                model.Message.Add("Rental deleted successfully");
                return model;
            }

            model.Message.Add("Rental not deleted");
            return model;
        }

        public async Task<GenericResponceModel<GetRentalDto>> GetByIdAsync(int id)
        {
            GenericResponceModel<GetRentalDto> model = new()
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

            Rental rental = await _rentalRepository.GetRentalByIdAsync(id);

            if (rental == null)
            {
                model.Message.Add("Rental not found");
                return model;
            }

            GetRentalDto getRentalDto = _mapper.Map<GetRentalDto>(rental);

            model.Data = getRentalDto;
            model.StatusCode = 200;
            model.Message.Add("Rental found");
            return model;
        }

        public async Task<GenericResponceModel<RentalReponceDto>> ReturnCar(int rentalId)
        {
            GenericResponceModel<RentalReponceDto> model = new()
            {
                Data = new(),
                StatusCode = 400,
                Message = new List<string>()
            };
            var transaction = await _unitOfWork.BeginTransactionAsync();

            try
            {
                var rental = await _rentalRepository.GetByIdAsync(rentalId);
                if (rental == null || rental.Status != RentalStatus.Active.ToString())
                {
                    model.Data.Message = "İcarə tapılmadı və ya artıq tamamlanıb.";
                    return model;
                }

                Car car = await _carRepository.GetByIdAsync(rental.CarId);

                if (rental.ReturnDate < DateTime.Now)
                {
                    decimal totalPrice = await ApplyLateFees(car.PricePerDay, rentalId);  //Gecikmə Cəriməsinin Hesablanması (ApplyLateFees metodu)
                    _logger.LogError($"Cerime: {totalPrice - rental.TotalPrice}");
                    model.Data.Message = $"Gecikdiyiniz ucun cerime {totalPrice - rental.TotalPrice},";
                    rental.PenaltyAmount = totalPrice - rental.TotalPrice;
                    rental.TotalPrice = totalPrice;
                    await _paymentRepository.ProcessPayment(rentalId, totalPrice);
                }

                if (car != null)
                {
                    car.IsAvailable = true;
                    model.Message.Add("Car is available");
                    _carRepository.Update(car);
                }

                rental.Status = RentalStatus.Completed.ToString();
                _rentalRepository.Update(rental);

                Payment payment = await _paymentRepository.GetByRentId(rentalId);
                payment.Status = PaymentStatus.Completed.ToString();
                _paymentRepository.Update(payment);

                int effectedRow = await _unitOfWork.SaveAsync();
                await transaction.CommitAsync();

                if (effectedRow > 0)
                {
                    model.Data.Message += $" Ugurla masin geri qaytarildi umumi mebleg {rental.TotalPrice}";
                    model.StatusCode = 200;
                    model.Message.Add("Car returned");
                    return model;
                }

                model.Message.Add("Car not returned");
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

        private async Task<decimal> ApplyLateFees(decimal carPricePerDay, int rentalId)
        {
            Rental rental = await _rentalRepository.GetByIdAsync(rentalId);
            decimal totalPrice = rental.TotalPrice;
            int overdueDays = (DateTime.Now - rental.ReturnDate).Days;  //gecikme gunlerin sayi

            if (overdueDays > 0)
            {
                decimal penalty = (overdueDays * carPricePerDay) * 1.1m;
                totalPrice += penalty;
            }
            return totalPrice;
        }

        public async Task<GenericResponceModel<RentalReponceDto>> RentCar(PostRentalDto postRentalDto)
        {
            GenericResponceModel<RentalReponceDto> model = new()
            {
                Data = new(),
                StatusCode = 400,
                Message = new List<string>()
            };

            var transaction = await _unitOfWork.BeginTransactionAsync();

            try
            {

                string userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                Customer customer = await _customerRepository.GetByUserId(userId);

                if (customer is null)
                {
                    model.Data.Message = "Sadəcə customer icarə götürə bilər";
                    return model;
                }

                Car car = await _carRepository.GetByIdAsync(postRentalDto.CarId);

                if (car == null || !car.IsAvailable)
                {
                    model.Data.Message = "Bu avtomobil icarəyə verilə bilməz.";
                    return model;
                }

                if (_rentalRepository.HasActiveRental(customer.Id))
                {
                    model.Data.Message = "Musterinin aktiv icaresi var";
                    return model;
                }

                decimal dailyRate = car.PricePerDay;
                decimal totalPrice = CalculateTotalPrice(dailyRate, postRentalDto.RentalDate, postRentalDto.ReturnDate);

                var rental = new Rental()
                {
                    CarId = postRentalDto.CarId,
                    CustomerId = customer.Id,
                    RentalDate = postRentalDto.RentalDate,
                    ReturnDate = postRentalDto.ReturnDate,
                    TotalPrice = totalPrice,
                    Status = RentalStatus.Active.ToString(),
                    Payment = new Payment()
                    {
                        Amount = totalPrice,
                        Status = PaymentStatus.Pending.ToString(),
                        PaymentDate = DateTime.Now,
                        PaymentMethod = postRentalDto.PaymentMethod.ToString(),
                    }

                };

                car.IsAvailable = false;
                _carRepository.Update(car);
                await _rentalRepository.CreateAsync(rental);

                int effectedRow = await _unitOfWork.SaveAsync();
                await transaction.CommitAsync();

                if (effectedRow > 0)
                {
                    model.Data.Message = "Kiraye ugurludur";
                    model.StatusCode = 201;
                    model.Message.Add("Rental created");
                    return model;
                }

                model.Message.Add("Rental not created");
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

        public async Task<GenericResponceModel<RentalReponceDto>> CancelRental(int rentalid)
        {
            GenericResponceModel<RentalReponceDto> model = new()
            {
                Data = new(),
                StatusCode = 400,
                Message = new List<string>()
            };

            var transaction = await _unitOfWork.BeginTransactionAsync();

            try
            {
                var rental = await _rentalRepository.GetByIdAsync(rentalid);

                if (rental == null && rental.Status != RentalStatus.Active.ToString())
                {
                    model.Data.Message = "Rental not found or already processed.";
                    return model;
                }

                string userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                Customer customer = await _customerRepository.GetByUserId(userId);

                if (rental.CustomerId != customer.Id)
                {
                    model.Data.Message = "Her bir user ancaq oz sifarisin legv ede biler";
                    return model;
                }

                Car car = await _carRepository.GetByIdAsync(rental.CarId);
                car.IsAvailable = true;
                _carRepository.Update(car);

                rental.Status = RentalStatus.Cancelled.ToString();
                _rentalRepository.Update(rental);

                var payment = await _paymentRepository.GetByRentId(rentalid);

                payment.Status = PaymentStatus.Cancelled.ToString();
                _paymentRepository.Update(payment);

                int effectedRow = await _unitOfWork.SaveAsync();
                await transaction.CommitAsync();

                if (effectedRow > 0)
                {
                    model.Data.Message = "Rental is canceled";
                    model.StatusCode = 200;
                    model.Message.Add("Rental canceled");
                }

                model.Message.Add("Rental not canceled");
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


        private decimal CalculateTotalPrice(decimal dailyRate, DateTime rentalDate, DateTime returnDate)
        {
            int days = (returnDate - rentalDate).Days;
            return days * dailyRate;
        }

        public async Task<GenericResponceModel<PaginatedResult<IEnumerable<GetRentalDto>>>> GetAllAsync(PaginationRequest paginationRequest, int? carId, int? customerId, RentalStatus? rentalStatus)
        {
            GenericResponceModel<PaginatedResult<IEnumerable<GetRentalDto>>> model = new()
            {
                Data = new(),
                StatusCode = 404,
                Message = new List<string>()
            };

            var filter = PredicateBuilder.True<Rental>();
            string? status = rentalStatus.ToString();

            if (carId.HasValue && carId > 0) filter = filter.And(x => x.CarId == carId);

            if (customerId.HasValue && customerId > 0) filter = filter.And(x => x.CustomerId == customerId);

            if (status is not null && status != "") filter = filter.And(x => x.Status == status);

            var rentals = await _rentalRepository.GetRentalsAsync(paginationRequest, filter);

            if (rentals is null)
            {
                model.Message.Add("Rentals not found");
                return model;
            }

            IEnumerable<GetRentalDto> getRentalDtos = _mapper.Map<IEnumerable<GetRentalDto>>(rentals.Data);

            model.Data = new PaginatedResult<IEnumerable<GetRentalDto>>
            {
                Data = getRentalDtos,
                TotalCount = rentals.TotalCount,
                PageNum = rentals.PageNum,
                PageSize = rentals.PageSize,
            };

            model.StatusCode = 200;
            model.Message.Add("Rentals found");
            return model;

        }
    }
}

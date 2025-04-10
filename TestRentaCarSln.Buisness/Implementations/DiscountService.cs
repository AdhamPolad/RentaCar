using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TestRentaCar.Buisness.Abstractions;
using TestRentaCar.Buisness.Dtos.Discount;
using TestRentaCar.Buisness.Dtos.Rental;
using TestRentaCar.Buisness.Helper;
using TestRentaCarDataAccess.Abstractions;
using TestRentaCarDataAccess.Entities;
using TestRentaCarDataAccess.Enums;
using TestRentaCarDataAccess.Model;
using TestRentaCarSln.Buisness.Dtos.Common;
using TestRentaCarSln.DataAccess.Abstractions;
using TestRentaCarSln.DataAccess.Abstractions.Base;
using TestRentaCarSln.DataAccess.Entities;

namespace TestRentaCar.Buisness.Implementations
{
    public class DiscountService : IDiscountService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDiscountRepository _discountRepository;
        private readonly IDiscountCustomerRepository _discountCustomerRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ICustomerRepository _customerRepository;
        private readonly IRentalRepository _rentalRepository;
        private readonly IPaymentRepository _paymentRepository;
        private readonly IMapper _mapper;
        public DiscountService(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _discountCustomerRepository = _unitOfWork.GetRepository<IDiscountCustomerRepository>();
            _discountRepository = _unitOfWork.GetRepository<IDiscountRepository>();
            _httpContextAccessor = httpContextAccessor;
            _customerRepository = _unitOfWork.GetRepository<ICustomerRepository>();
            _rentalRepository = _unitOfWork.GetRepository<IRentalRepository>();
            _paymentRepository = _unitOfWork.GetRepository<IPaymentRepository>();
            _mapper = mapper;
        }

        public async Task<GenericResponceModel<PaginatedResult<IEnumerable<GetDiscountDto>>>> GetAllAsync(PaginationRequest paginationRequest, bool? isActive)
        {
            GenericResponceModel<PaginatedResult<IEnumerable<GetDiscountDto>>> model = new()
            {
                Data = null,
                StatusCode = 404,
                Message = new List<string>()
            };

            var filter = PredicateBuilder.True<Discount>();

            if (isActive.HasValue) filter = filter.And(x => x.IsActive == isActive);

            var result = await _discountRepository.GetDiscountsAsync(paginationRequest, filter);

            if (!result.Data.Any())
            {
                model.Message.Add("Endirim tapılmadı");
                return model;
            }

            var discounts = result.Data;

            IEnumerable<GetDiscountDto> getDiscountDtos = discounts.Select(discount => new GetDiscountDto
            {
                Code = discount.Code,
                StartDate = discount.StartDate,
                EndDate = discount.EndDate,
                IsActive = discount.IsActive,
                DiscountAmount = discount.DiscountAmount,
                GetDiscountCustomers = discount.DiscountCustomers
                            .Select(dc => new GetDiscountCustomer
                            {
                                CustomerId = dc.CustomerId,
                                DiscountId = dc.DiscountId,
                                IsUsed = dc.IsUsed,
                            }).ToList()
            }).ToList();

            model.Data = new PaginatedResult<IEnumerable<GetDiscountDto>>
            {
                Data = getDiscountDtos,
                TotalCount = result.TotalCount,
                PageNum = paginationRequest.PageNumber,
                PageSize = paginationRequest.PageSize,
            };

            model.StatusCode = 200;
            model.Message.Add(model.Data.Data.Count() + " endirim tapıldı");
            return model;

        }

        public async Task<GenericResponceModel<bool>> UpdateAsync(UpdateDiscountDto updateDiscountDto)
        {
            GenericResponceModel<bool> model = new()
            {
                Data = false,
                StatusCode = 400,
                Message = new List<string>()
            };

            Discount discount = await _discountRepository.GetByIdAsync(updateDiscountDto.Id);

            if (discount == null)
            {
                model.Message.Add("Endirim tapılmadı");
                return model;
            }

            discount.StartDate = updateDiscountDto.StartDate;
            discount.EndDate = updateDiscountDto.EndDate;
            discount.DiscountAmount = updateDiscountDto.DiscountAmount;
            discount.IsActive = updateDiscountDto.IsActive;

            _discountRepository.Update(discount);

            int effectedRow = await _unitOfWork.SaveAsync();

            if (effectedRow > 0)
            {
                model.Data = true;
                model.StatusCode = 200;
                model.Message.Add("Endirim uğurla yeniləndi");
                return model;
            }

            model.Message.Add(model.Data + " endirim yenilənmədi");
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

            if (id <= 0)
            {
                model.Message.Add(model.Data + " Id is not valid");
                return model;
            }

            await _discountRepository.HardDeleteAsync(id);

            int effectedRow = await _unitOfWork.SaveAsync();

            if (effectedRow > 0)
            {
                model.Data = true;
                model.StatusCode = 200;
                model.Message.Add("Endirim uğurla silindi");
                return model;
            }

            model.Message.Add("Endirim silinmədi");
            return model;
        }

        public async Task<GenericResponceModel<bool>> DeActiveDiscount(int id)
        {
            GenericResponceModel<bool> model = new GenericResponceModel<bool>
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

            Discount discount = await _discountRepository.GetByIdAsync(id);

            if (discount == null)
            {
                model.Message.Add("Discount not found");
                return model;
            }

            discount.IsActive = false;
            _discountRepository.Update(discount);

            int effectedRow = await _unitOfWork.SaveAsync();

            if (effectedRow > 0)
            {
                model.Data = true;
                model.StatusCode = 200;
                model.Message.Add("Discount deactive");
                return model;
            }

            model.Message.Add("Discount not deactive");
            return model;
        }


        public async Task<GenericResponceModel<GetDiscountDto>> GetByIdAsync(int id)
        {
            GenericResponceModel<GetDiscountDto> model = new()
            {
                Data = null,
                StatusCode = 404,
                Message = new List<string>()
            };

            if (id <= 0)
            {
                model.Message.Add(model.Data + " Id is not valid");
                return model;
            }

            Discount discount = await _discountRepository.GetDiscountByIdAsync(id);

            if (discount is null)
            {
                model.Message.Add("Discount not found");
                return model;
            }

            GetDiscountDto getDiscountDto = new GetDiscountDto()
            {
                Code = discount.Code,
                StartDate = discount.StartDate,
                EndDate = discount.EndDate,
                IsActive = discount.IsActive,
                DiscountAmount = discount.DiscountAmount,
                GetDiscountCustomers = discount.DiscountCustomers
                                            .Select(dc => new GetDiscountCustomer()
                                            {
                                                CustomerId = dc.CustomerId,
                                                DiscountId = dc.DiscountId,
                                                IsUsed = dc.IsUsed,
                                            }).ToList()
            };

            model.Data = getDiscountDto;
            model.StatusCode = 200;
            model.Message.Add("Discount found");
            return model;

        }

        public async Task<GenericResponceModel<RentalReponceDto>> ApplyAsync(string code, int rentalId)
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
                if (string.IsNullOrEmpty(code))
                {
                    model.Data.Message = "Endirim kodu boş ola bilməz.";
                    return model;
                }

                string userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                Customer customer = await _customerRepository.GetByUserId(userId);

                if (customer == null)
                {
                    model.Data.Message = "Sadəcə müştəri apply edə bilər";
                }

                Discount? discount = await _discountRepository.GetByCode(code);

                if (discount == null)
                {
                    model.Data.Message = "Belə bir endirim tapılmadı.";
                    return model;
                }

                if (!discount.IsActive || discount.StartDate > DateTime.UtcNow || discount.EndDate < DateTime.UtcNow)
                {
                    model.Data.Message = "Endirim artıq keçərliliyini itirib.";
                    return model;
                }

                // İstifadəçi bu endirimi artıq tətbiq edibsə, ikinci dəfə icazə vermə
                DiscountCustomer? discountCustomer = await _discountCustomerRepository.GetByCustomerIdAndDiscountIdAsync(customer.Id, discount.Id);
                if (discountCustomer != null && discountCustomer.IsUsed)
                {
                    model.Data.Message = "Bu endirimi artıq mövcud deil.";
                    return model;
                }

                Rental rental = await _rentalRepository.GetByIdAsync(rentalId);

                if (rental is null || rental.Status != RentalStatus.Active.ToString())
                {
                    model.Data.Message = "Bu kiraye artiq keçerli deil";
                }

                if (rental.DiscountAmount == null)
                {
                    rental.DiscountAmount = 0;
                }

                rental.DiscountAmount += discount.DiscountAmount;
                rental.TotalPrice -= discount.DiscountAmount;

                _rentalRepository.Update(rental);

                Payment payment = await _paymentRepository.GetByRentId(rentalId);

                if (payment != null)
                {
                    payment.Amount -= discount.DiscountAmount;
                    _paymentRepository.Update(payment);
                }

                discountCustomer.IsUsed = true;
                _discountCustomerRepository.Update(discountCustomer);

                int effectedRow = await _unitOfWork.SaveAsync();
                await transaction.CommitAsync();

                if (effectedRow > 0)
                {
                    model.Data.Message = "Endirim uğurla tətbiq olundu.";
                    model.StatusCode = 200;
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
        public async Task<GenericResponceModel<bool>> CreateAsync(PostDiscountDto postDiscountDto)
        {
            GenericResponceModel<bool> model = new()
            {
                Data = false,
                StatusCode = 400,
                Message = new List<string>()
            };
            var transaction = await _unitOfWork.BeginTransactionAsync();

            try
            {
                Discount discount = new()
                {
                    Code = Guid.NewGuid().ToString(),
                    DiscountAmount = postDiscountDto.DiscountAmount,
                    StartDate = postDiscountDto.StartDate,
                    EndDate = postDiscountDto.EndDate,
                    IsActive = true
                };
                int customerId = postDiscountDto.CustomerId;

                DiscountCustomer discountCustomer = new()
                {
                    Discount = discount,
                    CustomerId = customerId,
                    IsUsed = false
                };

                await _discountCustomerRepository.CreateAsync(discountCustomer);

                int effectedRow = await _unitOfWork.SaveAsync();
                await transaction.CommitAsync();

                if (effectedRow > 0)
                {
                    model.Data = true;
                    model.StatusCode = 200;
                    model.Message.Add("Endirim uğurla yaradıldı");
                    return model;
                }

                model.Message.Add(model.Data + " endirim yaradılmadı");
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
    }
}

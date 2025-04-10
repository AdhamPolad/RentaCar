using AutoMapper;
using TestRentaCar.Buisness.Abstractions;
using TestRentaCar.Buisness.Dtos.Payment;
using TestRentaCar.Buisness.Helper;
using TestRentaCarDataAccess.Abstractions;
using TestRentaCarDataAccess.Enums;
using TestRentaCarDataAccess.Model;
using TestRentaCarSln.Buisness.Dtos.Common;
using TestRentaCarSln.DataAccess.Abstractions.Base;
using TestRentaCarSln.DataAccess.Entities;

namespace TestRentaCar.Buisness.Implementations
{
    public class PaymentService : IPaymentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPaymentRepository _paymentRepository;
        private readonly IMapper _mapper;

        public PaymentService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _paymentRepository = _unitOfWork.GetRepository<IPaymentRepository>();
            _mapper = mapper;
        }

        public async Task<GenericResponceModel<bool>> ConfirmPaymentAsync(int paymentId)
        {
            GenericResponceModel<bool> model = new()
            {
                Data = false,
                StatusCode = 400,
                Message = new List<string>()
            };

            if(paymentId <= 0)
            {
                model.Message.Add("Payment not selected");
                return model;
            }

            Payment payment = await _paymentRepository.GetByIdAsync(paymentId);

            if (payment == null || payment.Status != PaymentStatus.Pending.ToString())
            {
                model.Message.Add("Payment not found or not pending");
                return model;
            }

            payment.Status = PaymentStatus.Completed.ToString();
            _paymentRepository.Update(payment);

            int effectedRow = await _unitOfWork.SaveAsync();

            if(effectedRow > 0)
            {
                model.Data = true;
                model.StatusCode = 200;
                model.Message.Add("Payment confirmed");
                return model;
            }

            model.Message.Add("Payment not confirmed");
            return model;
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
                model.Message.Add(model.Data + "Id is not valid");
                return model;
            }
            if(deleteType == 0)
            {
                model.Message.Add("Delete type is not valid");
                return model;
            }

            if (deleteType == DeleteType.SoftDelete) await _paymentRepository.SoftDeleteAsync(id);

            if(deleteType == DeleteType.HardDelete) await _paymentRepository.HardDeleteAsync(id);

            int effectedRow = await _unitOfWork.SaveAsync();

            if (effectedRow > 0)
            {
                model.Data = true;
                model.StatusCode = 200;
                model.Message.Add("Payment deleted successfully");
            }

            return model;
        }

        public async Task<GenericResponceModel<PaginatedResult<IEnumerable<GetPaymentDto>>>> GetAllAsync(PaginationRequest paginationRequest, PaymentStatus? paymentStatus, PaymentFilterDto paymentFilterDto)
        {
            GenericResponceModel<PaginatedResult<IEnumerable<GetPaymentDto>>> model = new()
            {
                Data = null,
                StatusCode = 404,
                Message = new List<string>()
            };

            var filter = PredicateBuilder.True<Payment>();
            string? status = paymentStatus.ToString();
            int? rentalId = paymentFilterDto.rentalId;
            DateTime? startDate = paymentFilterDto.startDate;
            DateTime? endDate = paymentFilterDto.endDate;

            if (status is not null && status != "") filter = filter.And(x => x.Status == status);

            if(rentalId.HasValue && rentalId > 0) filter = filter.And(x=>x.RentalId == rentalId);

            if (startDate.HasValue) filter = filter.And(x => x.PaymentDate >= startDate);

            if(endDate.HasValue) filter = filter.And(x => x.PaymentDate <= endDate);

            var paginatedPayments = await _paymentRepository.GetPaymentsAsync(paginationRequest, filter);

            if (!paginatedPayments.Data.Any())
            {
                model.Message.Add("Payments not found");
                return model;
            }

            IEnumerable<GetPaymentDto> getPaymentDtos = _mapper.Map<IEnumerable<GetPaymentDto>>(paginatedPayments.Data);

            model.Data = new PaginatedResult<IEnumerable<GetPaymentDto>>
            {
                Data = getPaymentDtos,
                TotalCount = paginatedPayments.TotalCount,
                PageNum = paginationRequest.PageNumber,
                PageSize = paginationRequest.PageSize,
            };
            model.StatusCode = 200;
            model.Message.Add("Payments found");
            return model;

        }

        public async Task<GenericResponceModel<GetPaymentDto>> GetById(int id)
        {
            GenericResponceModel<GetPaymentDto> model = new()
            {
                Data = null,
                StatusCode = 404,
                Message = new List<string>()
            };

            Payment payment = await _paymentRepository.GetByIdAsync(id);

            if (payment == null)
            {
                model.Message.Add(model.Data + "Payment not found");
                return model;
            }

            GetPaymentDto getPaymentDto = _mapper.Map<GetPaymentDto>(payment);

            model.Data = getPaymentDto;
            model.StatusCode = 200;
            model.Message.Add("Payment found");
            return model;

        }

        public async Task<GenericResponceModel<bool>> UpdateAsync(UpdatePaymentDto updatePaymentDto)
        {
            GenericResponceModel<bool> model = new()
            {
                Data = false,
                StatusCode = 400,
                Message = new List<string>()
            };

            Payment payment = await _paymentRepository.GetByIdAsync(updatePaymentDto.Id);

            if (payment == null || updatePaymentDto.PaymentStatus == null || updatePaymentDto == null)
            {
                model.Message.Add("Payment not found or status is not valid");
                return model;
            }
            payment.Amount = updatePaymentDto.Amount;
            payment.PaymentDate = updatePaymentDto.PaymentDate;
            payment.Status = updatePaymentDto.PaymentStatus.ToString();
            payment.PaymentMethod = updatePaymentDto.PaymentMethod.ToString();

            _paymentRepository.Update(payment);
            int effectedRow = await _unitOfWork.SaveAsync();

            if(effectedRow > 0)
            {
                model.Data = true;
                model.StatusCode = 200;
                model.Message.Add("Payment updated");
                return model;
            }

            model.Message.Add("Payment not updated");
            return model;
        }
    }
}

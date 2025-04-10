using TestRentaCar.Buisness.Dtos.Payment;
using TestRentaCarDataAccess.Enums;
using TestRentaCarDataAccess.Model;
using TestRentaCarSln.Buisness.Dtos.Common;

namespace TestRentaCar.Buisness.Abstractions
{
    public interface IPaymentService
    {
        Task<GenericResponceModel<bool>> ConfirmPaymentAsync(int paymentId);
        Task<GenericResponceModel<PaginatedResult<IEnumerable<GetPaymentDto>>>> GetAllAsync(PaginationRequest? paginationRequest, PaymentStatus? paymentStatus, PaymentFilterDto paymentFilterDto);
        Task<GenericResponceModel<bool>> UpdateAsync(UpdatePaymentDto updatePaymentDto);
        Task<GenericResponceModel<bool>> DeleteAsync(DeleteType deleteType, int id);
        Task<GenericResponceModel<GetPaymentDto>> GetById(int id);
    }
}

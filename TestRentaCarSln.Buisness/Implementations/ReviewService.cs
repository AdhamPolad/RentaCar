using AutoMapper;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using TestRentaCar.Buisness.Abstractions;
using TestRentaCar.Buisness.Dtos.Rental;
using TestRentaCar.Buisness.Dtos.Review;
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
    public class ReviewService : IReviewService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IReviewRepository _reviewRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;
        private readonly IRentalRepository _rentalRepository;
        private readonly ICustomerRepository _customerRepository;

        public ReviewService(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
            _reviewRepository = _unitOfWork.GetRepository<IReviewRepository>();
            _mapper = mapper;
            _rentalRepository = _unitOfWork.GetRepository<IRentalRepository>();
            _customerRepository = _unitOfWork.GetRepository<ICustomerRepository>();
        }

        public async Task<GenericResponceModel<RentalReponceDto>> CreateAsync(PostReviewDto postReviewDto)
        {
            GenericResponceModel<RentalReponceDto> model = new()
            {
                Data = new(),
                StatusCode = 400,
                Message = new List<string>()
            };

            string userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            Customer customer = await _customerRepository.GetByUserId(userId);

            bool rentalExists = await _rentalRepository.RentalExists(customer.Id, postReviewDto.CarId);

            if (!rentalExists)
            {
                model.Data.Message = "User ancaq icareye goturduyu avtomobile rey yaza biler";
                return model;
            }

            Review review = new Review()
            {
                UserId = userId,
                Rating = Convert.ToInt32(postReviewDto.ReviewRating),
                CarId = postReviewDto.CarId,
                Comment = postReviewDto.Comment
            };

            await _reviewRepository.CreateAsync(review);

            int effectedRow = await _unitOfWork.SaveAsync();

            if (effectedRow > 0)
            {
                model.Data.Message = "Rey yazildi";
                model.StatusCode = 201;
                model.Message.Add("Review created");
                return model;
            }

            model.Message.Add("Review not created");
            return model;

        }

        public async Task<GenericResponceModel<bool>> DeleteAsync(int id, DeleteType deleteType)
        {
            GenericResponceModel<bool> model = new()
            {
                Data = false,
                StatusCode = 400,
                Message = new List<string>()
            };

            if (id <= 0) { model.Message.Add("Id is not valid"); return model; }

            if (deleteType == 0) { model.Message.Add("Delete type is not valid"); return model; }

            if (deleteType == DeleteType.SoftDelete)
                await _reviewRepository.SoftDeleteAsync(id);

            if (deleteType == DeleteType.HardDelete)
                await _reviewRepository.HardDeleteAsync(id);

            int effectedRow = await _unitOfWork.SaveAsync();

            if (effectedRow > 0)
            {
                model.StatusCode = 200;
                model.Data = true;
                model.Message.Add("Review deleted successfully");
                return model;
            }
            model.Message.Add("Review not deleted");
            return model;
        }

        public async Task<GenericResponceModel<PaginatedResult<IEnumerable<GetReviewDto>>>> GetAllAsync(PaginationRequest paginationRequest, int? carId, ReviewRating? reviewRating)
        {
            GenericResponceModel<PaginatedResult<IEnumerable<GetReviewDto>>> model = new()
            {
                Data = null,
                StatusCode = 404,
                Message = new List<string>()
            };

            var filter = PredicateBuilder.True<Review>();
            int? rating = Convert.ToInt32(reviewRating);

            if (carId.HasValue && carId > 0)
                filter = filter.And(x => x.CarId == carId);
            if (rating.HasValue && rating > 0)
                filter = filter.And(x => x.Rating == rating);

            var reviews = await _reviewRepository.GetReviewsAsync(paginationRequest, filter);

            if (!reviews.Data.Any()) { model.Message.Add("Reviews not found"); return model; }

            IEnumerable<GetReviewDto> getReviewDtos = _mapper.Map<IEnumerable<GetReviewDto>>(reviews.Data);

            model.Data = new PaginatedResult<IEnumerable<GetReviewDto>>()
            {
                Data = getReviewDtos,
                TotalCount = reviews.TotalCount,
                PageNum = paginationRequest.PageNumber,
                PageSize = paginationRequest.PageSize
            };
            model.StatusCode = 200;
            model.Message.Add("Reviews found");
            return model;

        }

        public async Task<GenericResponceModel<GetReviewDto>> GetById(int id)
        {
            GenericResponceModel<GetReviewDto> model = new()
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
            Review review = await _reviewRepository.GetByIdAsync(id);

            if (review is null)
            {
                model.Message.Add("Review not found");
                return model;
            }
            GetReviewDto getReviewDto = _mapper.Map<GetReviewDto>(review);

            model.Data = getReviewDto;
            model.StatusCode = 200;
            model.Message.Add("Review found");
            return model;

        }

        public async Task<GenericResponceModel<bool>> UpdateAsync(UpdateReviewDto updateReviewDto)
        {
            GenericResponceModel<bool> model = new()
            {
                Data = false,
                StatusCode = 400,
                Message = new List<string>()
            };

            Review review = await _reviewRepository.GetByIdAsync(updateReviewDto.Id);

            if (review is null)
            {
                model.Message.Add("Review not found");
                return model;
            }
            review.Comment = updateReviewDto.Comment;
            review.Rating = Convert.ToInt32(updateReviewDto.ReviewRating);

            _reviewRepository.Update(review);

            int effectedRow = await _unitOfWork.SaveAsync();

            if (effectedRow > 0)
            {
                model.Data = true;
                model.StatusCode = 200;
                model.Message.Add("Review updated");
                return model;
            }

            model.Message.Add("Review not updated");
            return model;
        }

    }
}

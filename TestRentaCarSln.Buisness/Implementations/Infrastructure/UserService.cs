using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TestRentaCar.Buisness.Abstractions.Infrastructure;
using TestRentaCar.Buisness.Dtos.User;
using TestRentaCarDataAccess.Entities.Identity;
using TestRentaCarDataAccess.Enums;
using TestRentaCarSln.Buisness.Dtos.Common;
using TestRentaCarSln.DataAccess.Abstractions;
using TestRentaCarSln.DataAccess.Abstractions.Base;
using TestRentaCarSln.DataAccess.Entities;

namespace TestRentaCar.Buisness.Implementations.Infrastructure
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICustomerRepository _customerRepository;

        public UserService(IMapper mapper, UserManager<AppUser> userManager, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _customerRepository = _unitOfWork.GetRepository<ICustomerRepository>();
        }

        public async Task<GenericResponceModel<bool>> AssignRoleToUserAsync(string userId, string[] roles)
        {
            AppUser appUser = await _userManager.FindByIdAsync(userId);
            GenericResponceModel<bool> model = new()
            {
                Data = false,
                StatusCode = 400,
                Message = new List<string>()
            };

            if (appUser is not null)
            {
                var userRoles = await _userManager.GetRolesAsync(appUser);
                var removeResult = await _userManager.RemoveFromRolesAsync(appUser, userRoles);

                if (removeResult.Succeeded)
                    await _userManager.AddToRolesAsync(appUser, roles);
                else
                    return model;

                model.Data = true;
                model.StatusCode = 200;
                model.Message.Add("Roles assigned successfully");
                return model;
            }

            model.Message.Add("User not found");
            return model;
        }

        public async Task<GenericResponceModel<bool>> DeleteUserAsync(string userIdOrName)
        {
            var model = new GenericResponceModel<bool>()
            {
                Data = false,
                StatusCode = 400,
                Message = new List<string>()
            };

            AppUser user = await _userManager.FindByNameAsync(userIdOrName);

            if (user is null)
                user = await _userManager.FindByIdAsync(userIdOrName);

            if (user is null)
                return model;

            var result = await _userManager.DeleteAsync(user);

            if (result.Succeeded)
            {
                model.Data = true;
                model.StatusCode = 200;
                model.Message.Add("User deleted successfully");
                return model;
            }

            model.Message.Add("User not deleted");
            return model;

        }

        public async Task<GenericResponceModel<List<GetUserDto>>> GetAllAsync()
        {
            GenericResponceModel<List<GetUserDto>> model = new()
            {
                Data = null,
                StatusCode = 404,
                Message = new List<string>()
            };
            var users = await _userManager.Users.ToListAsync();

            try
            {
                if (users is not null && users.Count > 0)
                {
                    var data = _mapper.Map<List<GetUserDto>>(users);

                    model.Data = data;
                    model.StatusCode = 200;
                    model.Message.Add("Users found");
                    return model;
                }
                else
                {
                    model.Message.Add("Users not found");
                    return model;
                }

            }
            catch (Exception ex)
            {
                //log

                return model;
            }

        }

        public async Task<GenericResponceModel<string[]>> GetRolesToUserAsync(string userIdOrName)
        {
            AppUser appUser = await _userManager.FindByNameAsync(userIdOrName);

            if (appUser is null)
                appUser = await _userManager.FindByIdAsync(userIdOrName);

            var model = new GenericResponceModel<string[]>()
            {
                Data = null,
                StatusCode = 400,
                Message = new List<string>()
            };

            if (appUser is null)
            {
                model.Message.Add("User not found");
                return model;
            }

            var userRoles = await _userManager.GetRolesAsync(appUser);

            model.Data = userRoles.ToArray();
            model.StatusCode = 200;
            model.Message.Add("Roles found");
            return model;
        }

        public async Task<GenericResponceModel<CreateUserResponceDto>> RegisterCustomer(CreateCustomerUserDto createCustomerUserDto, Roles role)
        {
            var transaction = await _unitOfWork.BeginTransactionAsync();

            var model = new GenericResponceModel<CreateUserResponceDto>()
            {
                Message = new List<string>(),
            };

            try
            {
                string id = Guid.NewGuid().ToString();

                var result = await _userManager.CreateAsync(new AppUser()
                {
                    Id = id,
                    UserName = createCustomerUserDto.User.UserName,
                    FirstName = createCustomerUserDto.User.FirstName,
                    Lastname = createCustomerUserDto.User.LastName,
                    Email = createCustomerUserDto.User.Email,
                    Birthdate = createCustomerUserDto.User.BirthDate
                }, createCustomerUserDto.User.Passwords);

                model.Data = new CreateUserResponceDto()
                {
                    Succeeded = result.Succeeded
                };
                model.Message.Add("User not created");
                model.StatusCode = 400;

                if (result.Succeeded)
                {
                    model.Message.Add("User created successfully");
                    model.StatusCode = 201;
                }

                if (!result.Succeeded)
                {
                    model.Data.Message = string.Join(" \n ", result.Errors.Select(error => $"{error.Code} - {error.Description}"));
                    return model;
                }

                Customer customer = new()
                {
                    UserId = id,
                    FullName = $"{createCustomerUserDto.User.FirstName} {createCustomerUserDto.User.LastName}",
                    PhoneNumber = createCustomerUserDto.Customer.PhoneNumber,
                    Address = createCustomerUserDto.Customer.Address,
                    DriverLisenceNumber = createCustomerUserDto.Customer.DriverLisenceNumber,
                };

                await _customerRepository.CreateAsync(customer);
                int effectedRow = await _unitOfWork.SaveAsync();
                await transaction.CommitAsync();

                if (effectedRow < 0)
                {
                    model.Data = null;
                    model.Message.Add("Customer not created");
                    model.StatusCode = 400;
                }

                AppUser appUser = await _userManager.FindByNameAsync(createCustomerUserDto.User.UserName);

                if (appUser is null)
                    appUser = await _userManager.FindByEmailAsync(createCustomerUserDto.User.Email);

                if (appUser is null)
                    appUser = await _userManager.FindByIdAsync(id);

                if (appUser != null)
                    await _userManager.AddToRoleAsync(appUser, role.ToString());

                return model;
            }
            catch (Exception exp)
            {
                model.Message.Add("User not created" + exp.Message);
                await  transaction.RollbackAsync();
                return model;
            }
            finally
            {
                await transaction.DisposeAsync();
            }

        }



        public async Task<GenericResponceModel<CreateUserResponceDto>> CreateAsync(CreateUserDto createUserDto)
        {
            var model = new GenericResponceModel<CreateUserResponceDto>()
            {
                Message = new List<string>(),
            };

            string id = Guid.NewGuid().ToString();

            var result = await _userManager.CreateAsync(new AppUser()
            {
                Id = id,
                UserName = createUserDto.UserName,
                FirstName = createUserDto.FirstName,
                Lastname = createUserDto.LastName,
                Email = createUserDto.Email
            }, createUserDto.Passwords);

            model.Data = new CreateUserResponceDto()
            {
                Succeeded = result.Succeeded
            };
            model.StatusCode = 400;

            if (result.Succeeded)
            {
                model.Message.Add("User created successfully");
                model.StatusCode = 201;
            }

            if (!result.Succeeded)
            {
                model.Message.Add("User not created");
                model.Data.Message = string.Join(" \n ", result.Errors.Select(error => $"{error.Code} - {error.Description}"));
            }

            AppUser appUser = await _userManager.FindByNameAsync(createUserDto.UserName);

            if (appUser is null)
                appUser = await _userManager.FindByEmailAsync(createUserDto.Email);

            if (appUser is null)
                appUser = await _userManager.FindByIdAsync(id);

            if (appUser != null)
                await _userManager.AddToRoleAsync(appUser, "User");

            return model;
        }

        public async Task<GenericResponceModel<bool>> UpdateRefreshToken(string refreshToken, AppUser user, DateTime accessTokenDate)
        {
            var model = new GenericResponceModel<bool>()
            {
                Data = false,
                StatusCode = 400,
                Message = new List<string>()
            };

            if (user != null)
            {
                user.RefreshToken = refreshToken;
                user.RefreshTokenEndTime = accessTokenDate.AddMinutes(10);
                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    model.Data = true;
                    model.StatusCode = 200;
                    model.Message.Add("Refresh token updated successfully");
                    return model;
                }

            }

            model.Message.Add("Refresh token not updated");
            return model;

        }

        public async Task<GenericResponceModel<bool>> UpdateUserAsync(UpdateUserDto updateUserDto)
        {
            var model = new GenericResponceModel<bool>()
            {
                Data = false,
                StatusCode = 400,
                Message = new List<string>()
            };

            AppUser user = await _userManager.FindByIdAsync(updateUserDto.UserId);
            if (user is null)
                user = await _userManager.FindByNameAsync(updateUserDto.UserName);

            if (user is null)
                return model;

            user.UserName = updateUserDto.UserName;
            user.Email = updateUserDto.Email;
            user.FirstName = updateUserDto.FirstName;
            user.Lastname = updateUserDto.LastName;
            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                model.Data = true;
                model.StatusCode = 200;
                model.Message.Add("User updated successfully");
                return model;
            }

            model.Message.Add("User not updated");
            return model;
        }
    }
}

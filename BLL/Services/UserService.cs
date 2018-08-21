using BLL.DTO;
using BLL.Infrastructure;
using BLL.Interfaces;
using Domain.Entities;
using Domain.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class UserService : IUserService
    {
        EFUnitOfWork Database { get; set; }

        public UserService(EFUnitOfWork uow)
        {
            Database = uow;
        }

        /// <summary>
        /// Register User
        /// </summary>
        /// <param name="userDto"></param>
        /// <returns></returns>
        public async Task<OperationDetails> Create(UserDTO userDto)
        {
            ApplicationUser user = await Database.UserManager.FindByEmailAsync(userDto.Email);  //chack if user allredy exist
             
            if (user == null)
            {
                user = new ApplicationUser { Email = userDto.Email, UserName = userDto.Email };
                await Database.UserManager.CreateAsync(user, userDto.Password);

                await Database.UserManager.AddToRoleAsync(user.Id, userDto.Role);   // add role to Db

                // create client profile
                Client client   = new Client { Id = user.Id, Address = userDto.Address, Name = userDto.Name };

                Database.ClientManager.Create(client);

                await Database.SaveAsync();
                return new OperationDetails(true, "Registration successful", "");
            }
            else
            {
                return new OperationDetails(false, "User  with the same Email is allredy exist", "Email");
            }
        }

        public async Task<string> Authenticate(UserDTO userDto)
        {
            // find User
            ApplicationUser user = await Database.UserManager.FindAsync(userDto.Email, userDto.Password);
            string role = null;

            if (user != null)
            {
                IList<string> rols = await Database.UserManager.GetRolesAsync(user.Id);  // get user's role  

                foreach (var item in rols)
                {
                    role = item;
                }
            }
            return role;    //return user role , null if user dosen't exist
        }


        /// <summary>
        /// start Initialization BD
        /// </summary>
        /// <param name="adminDto"></param>
        /// <param name="roles"></param>
        /// <returns></returns>
        public async Task SetInitialData(UserDTO adminDto, List<string> roles)
        {
            foreach (string roleName in roles)
            {
                var role = await Database.RoleManager.FindByNameAsync(roleName);
                if (role == null)
                {
                    role = new ApplicationRole { Name = roleName };
                    await Database.RoleManager.CreateAsync(role);
                }
            }
            await Create(adminDto);
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }

}

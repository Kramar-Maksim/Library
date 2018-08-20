using BLL.DTO;
using BLL.Infrastructure;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IUserService : IDisposable
    {
        Task<OperationDetails> Create(UserDTO userDto);
        Task<string> Authenticate(UserDTO userDto);
        Task SetInitialData(UserDTO adminDto, List<string> roles);
    }
}

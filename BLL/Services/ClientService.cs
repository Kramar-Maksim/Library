using AutoMapper;
using BLL.DTO;
using BLL.Interfaces;
using Domain.Entities;
using Domain.UnitOfWork;
using System;
using System.Collections.Generic;
using BLL.Infrastructure;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class ClientService : IClientService
    {
        EFUnitOfWork Database { get; set; }

        public ClientService(EFUnitOfWork uow)
        {
            Database = uow;
        }


        public ClientDTO MyProfile(int? id)                  //get information about user
        {
            //if (id == null)
            //    throw new ArgumentNullException();

            //Client client = Database.Clients.Get(id);

            //if (client == null)
            //    throw new ArgumentNullException();

            //var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Client, ClientDTO>()).CreateMapper();
            //return mapper.Map<Client, ClientDTO>(Database.Clients.Get(client.Id));
            return new ClientDTO();
        }

        public IEnumerable<ClientDTO> GetItems()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Client, ClientDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<Client>, List<ClientDTO>>(Database.Clients.GetAll());
        }

        public async Task CreateItem(ClientDTO ItemDTO)
        {
            if (ItemDTO == null)
                throw new ArgumentNullException();

            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ClientDTO, Client>()).CreateMapper();
            Database.Clients.Create(mapper.Map<ClientDTO, Client>(ItemDTO));
            await Database.SaveAsync();
        }

        public async Task EditItem(ClientDTO ItemDTO)
        {
            if (ItemDTO == null)
                throw new ArgumentNullException();

            Database.Clients.Update(Mapper.Map<ClientDTO, Client>(ItemDTO));
            await Database.SaveAsync();
        }

        public ClientDTO GetItem(int? id)
        {
            if (id == null)
                throw new ArgumentNullException();

            Client client = Database.Clients.Get(id.Value);

            if (client == null)
                throw new ArgumentNullException();

            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Client, ClientDTO>()).CreateMapper();
            return mapper.Map<Client, ClientDTO>(Database.Clients.Get(id.Value));
        }

        public async Task DeleteItem(int? id)
        {
            Database.Clients.Delete(id.Value);
            await Database.SaveAsync();
        }

    }
}

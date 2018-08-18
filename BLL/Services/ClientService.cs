using AutoMapper;
using BLL.DTO;
using BLL.Interfaces;
using Domain.Entities;
using Domain.UnitOfWork;
using System;
using System.Collections.Generic;
using BLL.Infrastructure;

namespace BLL.Services
{
    public class ClientService : IClientService
    {
        IUnitOfWork Database { get; set; }

        public ClientService(IUnitOfWork uow)
        {
            Database = uow;
        }


        public ClientDTO MyProfile(int? name)                  //get information about user
        {
            if (name == null)
                throw new ArgumentNullException();

            Client client = Database.Clients.Get(name.Value);

            if (client == null)
                throw new ArgumentNullException();

            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Client, ClientDTO>()).CreateMapper();
            return mapper.Map<Client, ClientDTO>(Database.Clients.Get(client.ClientID));
        } 

        public IEnumerable<ClientDTO> GetItems()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Client, ClientDTO>()).CreateMapper();

            return mapper.Map<IEnumerable<Client>, List<ClientDTO>>(Database.Clients.GetAll()); 
        }

        public void CreateItem(ClientDTO ItemDTO)
        {
            if (ItemDTO == null)
                throw new ArgumentNullException();
             
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ClientDTO, Client>()).CreateMapper();
            Database.Clients.Create(mapper.Map<ClientDTO, Client>(ItemDTO));
            Database.SaveAsync();
        }

        public void EditItem(ClientDTO ItemDTO)
        {
            if (ItemDTO == null)
                throw new ArgumentNullException();

            Database.Clients.Update(Mapper.Map<ClientDTO, Client>(ItemDTO));
            Database.SaveAsync();
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

        public void DeleteItem(int? id)
        { 
            Database.Clients.Delete(id);
            Database.SaveAsync();
        }
    }
}

using AutoMapper;
using BLL.DTO;
using BLL.Interfaces;
using Domain.Entities;
using Domain.UnitOfWork;
using System;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class OrderService : IOrderService
    {
        EFUnitOfWork Database { get; set; }

        public OrderService(EFUnitOfWork uow)
        {
            Database = uow;
        }


        public async Task MakeOrder(BookDTO book, string clientName)          // Client make order
        {
            ApplicationUser user = await Database.UserManager.FindByNameAsync(clientName);
            if (user != null)
            {
                ClientDTO client = new ClientDTO()
                {
                    Adress = user.ClientProfile.Address,
                    ClientID = user.ClientProfile.Id,
                    Email = user.Email,
                    Name = user.ClientProfile.Name,
                    IdentityId = user.Id,
                    PhoneNumber = user.PhoneNumber
                };

                Order order = new Order()
                {
                    OrderedBook = Mapper.Map<BookDTO, Book>(book),               // shows which book client whants to order
                    ClientOrder = Mapper.Map<ClientDTO, Client>(client),  // shows who order the book
                    IsGiven = false,                                             // showing that order is waiting for librarian accepting
                    OrderDate = DateTime.Now
                };

                Database.Orders.Create(order);                                  //adding order to database, where librarian can accept the order 
                await Database.SaveAsync();
            }
        }

    }
}

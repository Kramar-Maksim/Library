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

        /// <summary>
        /// Client make order
        /// </summary>
        /// <param name="bookId"></param>
        /// <param name="clientEmail"></param>
        /// <returns></returns>
        public async Task MakeOrder(int bookId, string clientEmail)          
        {
            ApplicationUser user = await Database.UserManager.FindByEmailAsync(clientEmail);
            Book book = Database.Books.Get(bookId);
             
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
                    IsGiven = false,                                        // showing that order is waiting for librarian accepting
                    OrderDate = DateTime.Now,

                    ClientOrder_Id = client.ClientID,                       // shows who order the book
                    OrderBook_Id = book.BookId                              // shows which book client whants to order
                };

                Database.Orders.Create(order);                              //adding order to database, where librarian can accept the order  
                await Database.SaveAsync();
            }
         
        }

    }
}

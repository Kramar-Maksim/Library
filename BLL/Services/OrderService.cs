using System;
using AutoMapper;
using Domain.UnitOfWork;
using BLL.Interfaces;
using BLL.DTO;
using Domain.Entities;

namespace BLL.Services
{
    public class OrderService : IOrderService
    {
        IUnitOfWork Database { get; set; }

        public OrderService(IUnitOfWork uow)
        {
            Database = uow;
        }


        public void MakeOrder(BookDTO book, ClientDTO client)         // Client make order
        {
            Order order = new Order()
            {
                OrderedBook = Mapper.Map<BookDTO, Book>(book),        // shows which book client whants to order
                ClientOrder = Mapper.Map<ClientDTO, Client>(client),  // shows who order the book
                IsGiven = false,                                      // showing that order is waiting for librarian accepting
                OrderDate = DateTime.Now
            };
             
            Database.Orders.Create(order);                           //adding order to database, where librarian can accept the order 
            Database.SaveAsync();
        }
         
    }
}

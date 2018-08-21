using AutoMapper;
using BLL.DTO;
using BLL.Interfaces;
using Domain.Entities;
using Domain.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class LibrarianService : ILibrarian, IService<ClientDTO>
    {
        EFUnitOfWork Database { get; set; }

        public LibrarianService(EFUnitOfWork uow)
        {
            Database = uow;
        }

        /// <summary>
        /// Get books from database
        /// </summary>
        /// <returns></returns>
        public IEnumerable<BookDTO> GetBookList()                                      
        {
            IEnumerable<Book> books = Database.Books.GetAll();

            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Book, BookDTO>()).CreateMapper(); 
            return mapper.Map<IEnumerable<Book>, IEnumerable<BookDTO>>(books);
        }

        /// <summary>
        /// give book to client, from list of orders
        /// </summary>
        /// <param name="orderDto"></param>
        /// <returns></returns>
        public async Task GiveTheBook(OrderDTO orderDto)                                   
        {
           // int maxBooksInOneHand = 5;                                                  //max book that librarian can give to one person              
            bool BookInLibrary = false;
            IEnumerable<BookDTO> allBooks = GetBookList();

            foreach (var item in allBooks)                                                //Look for book id, to see if the Book is in the List             
                if (item.BookId == orderDto.OrderBook_Id)
                    BookInLibrary = true;
             
            Client client = Database.Clients.GetByIDstr(orderDto.ClientOrder_Id);        //geting from database objects(client, book, order) 
            Book curentBook = Database.Books.Get(orderDto.OrderBook_Id); 
            Order order = Database.Orders.Get(orderDto.OrderID);
             
            //if (client.OrderedBooks.Count() < maxBooksInOneHand)
            //    //my expeption TODO
            //    throw new Exception();                                                 // you cant give more books to this person limit is  " maxBooksInOneHand "
             
            if (BookInLibrary)
            {
                client.OrderedBooks.Add(curentBook);                                     //adding book to Clients Order List of Books 
                order.OrderDate = DateTime.Now;
                order.IsGiven = true;  

                Database.Clients.Update(client);                                        //updating Clients information in database
                Database.Orders.Update(order);
                await Database.SaveAsync();
            }
            else
                throw new ArgumentNullException();                                     //Book Not Found 
        }

        /// <summary>
        /// returns orders where you can find Client ID and book which was taken
        /// </summary>
        /// <returns></returns>
        public IEnumerable<OrderDTO> TracingOfDebtors()                                
        {
            IEnumerable<Order> Debtors = from orders in Database.Orders.GetAll()
                                         where orders.OrderDate.Day > DateTime.Now.Day + 7   //book was taken more then 7 days ago
                                         select orders;

            return Mapper.Map<IEnumerable<Order>, IEnumerable<OrderDTO>>(Debtors);
        }

        /// <summary>
        /// List of Orders thet are whaiting for librarian acception
        /// </summary>
        /// <returns></returns>
        public IEnumerable<OrderDTO> OrdersForAccepting()                               
        {
            IEnumerable<Order> NewOrders = from orders in Database.Orders.GetAll()
                                           where orders.IsGiven == false              //orders that are whaiting for acception
                                           select orders;

            return Mapper.Map<IEnumerable<Order>, IEnumerable<OrderDTO>>(NewOrders);
        }

        /// <summary>
        /// Get list of clients
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ClientDTO> GetItems()                                        
        {
            IEnumerable<Client> clients = Database.Clients.GetAll();
            return Mapper.Map<IEnumerable<Client>, IEnumerable<ClientDTO>>(clients);
        }

        /// <summary>
        /// get client by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ClientDTO GetItem(int? id)                                              
        {
            if (id == null)
                throw new ArgumentNullException();

            Client client = Database.Clients.Get(id.Value);

            if (client == null)
                throw new ArgumentNullException();

            return (Mapper.Map<Client, ClientDTO>(client));
        }


        /// <summary>
        /// Create new client
        /// </summary>
        /// <param name="ItemDTO"></param>
        /// <returns></returns>
        public async Task CreateItem(ClientDTO ItemDTO)                                
        {
            if (ItemDTO == null)
                throw new ArgumentNullException();

            Database.Clients.Create(Mapper.Map<ClientDTO, Client>(ItemDTO));
            await Database.SaveAsync();
        }

        /// <summary>
        /// change client profile information
        /// </summary>
        /// <param name="ItemDTO"></param>
        /// <returns></returns>
        public async Task EditItem(ClientDTO ItemDTO)                                  
        {
            if (ItemDTO == null)
                throw new ArgumentNullException();

            Database.Clients.Update(Mapper.Map<ClientDTO, Client>(ItemDTO));
            await Database.SaveAsync();
        }

        /// <summary>
        /// delete client by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task DeleteItem(int? id)                                           
        {
            Database.Clients.Delete(id.Value);
            await Database.SaveAsync();
        }
    }
}

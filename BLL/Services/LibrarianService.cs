using AutoMapper;
using BLL.DTO;
using BLL.Interfaces;
using Domain.Entities;
using Domain.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BLL.Services
{
    public class LibrarianService : ILibrarian, IService<ClientDTO>
    {
        IUnitOfWork Database { get; set; }

        public LibrarianService(IUnitOfWork uow)
        {
            Database = uow;
        }


        public IEnumerable<BookDTO> GetBookList()                        // Get books from database
        {
            IEnumerable<Book> books = Database.Books.GetAll();

            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Book, BookDTO>()).CreateMapper();
            var booksDTO = mapper.Map<IEnumerable<Book>, IEnumerable<BookDTO>>(books);

            return booksDTO;
        }
         
        public void GiveTheBook(OrderDTO orderDto)                       //give book to client, from list of orders
        {
            int maxBooksInOneHand = 5;                                                 //max book that librarian can give to one person              
            bool BookExist = false;
            IEnumerable<BookDTO> allBooks = GetBookList();

            foreach (var item in allBooks)                                             //Look for book id, to see if the Book is in the List             
                if (item.BookId == orderDto.OrderedBook.BookId)
                    BookExist = true;


            Client client = Database.Clients.Get(orderDto.Clientdto.ClientID);         //geting from database objects(client, book, order)
            Book curentBook = Database.Books.Get(orderDto.OrderedBook.BookId);
            Order order = Database.Orders.Get(orderDto.OrderID);

            if (client.OrderedBooks.Count() < maxBooksInOneHand)
                //my expeption TODO
                throw new Exception();                                                 // you cant give more books to this person limit is  " maxBooksInOneHand "


            if (BookExist)
            {
                client.OrderedBooks.Add(curentBook);                                   //adding book to Clients Order List of Books
                client.DesiredBooks.Remove(Mapper.Map<OrderDTO, Order>(orderDto));     //remove order from clients wish book

                order.OrderDate = DateTime.Now;
                order.IsGiven = true;

                Database.Clients.Update(client);                                       //updating Clients information in database
                Database.Orders.Update(order);
                Database.SaveAsync();
            }
            else
                throw new ArgumentNullException();                                    //Book Not Found 
        }

        public IEnumerable<OrderDTO> TracingOfDebtors()                               //returns orders where you can find Client ID and book which was taken
        { 
            IEnumerable<Order> Debtors = from orders in Database.Orders.GetAll()
                                         where orders.OrderDate.Day > DateTime.Now.Day + 7   //book was taken more then 7 days ago
                                         select orders;

            return Mapper.Map<IEnumerable<Order>, IEnumerable<OrderDTO>>(Debtors);
        }

        public IEnumerable<OrderDTO> OrdersForAccepting()                             // List of Orders thet are whaiting for librarian acception
        {
            IEnumerable<Order> NewOrders = from orders in Database.Orders.GetAll()
                                           where orders.IsGiven == false              //orders that are whaiting for acception
                                           select orders;

            return Mapper.Map<IEnumerable<Order>, IEnumerable<OrderDTO>>(NewOrders);
        }

        public IEnumerable<ClientDTO> GetItems()                                       //Get list of clients
        {
            IEnumerable<Client> clients = Database.Clients.GetAll();
            return Mapper.Map<IEnumerable<Client>, IEnumerable<ClientDTO>>(clients);
        }      

        public ClientDTO GetItem(int? id)                                              //get client by ID
        {
            if (id == null)
                throw new ArgumentNullException();

            Client client = Database.Clients.Get(id.Value);

            if (client == null)
                throw new ArgumentNullException();

            return (Mapper.Map<Client, ClientDTO>(client));
        }              

        public void CreateItem(ClientDTO ItemDTO)                                      //Create new client
        {
            if (ItemDTO == null)
                throw new ArgumentNullException();

            Database.Clients.Create(Mapper.Map<ClientDTO, Client>(ItemDTO));
            Database.SaveAsync();
        }    

        public void EditItem(ClientDTO ItemDTO)                                        // change client profile information
        {
            if (ItemDTO == null)
                throw new ArgumentNullException();

            Database.Clients.Update(Mapper.Map<ClientDTO, Client>(ItemDTO));
            Database.SaveAsync();
        }

        public void DeleteItem(int? id)                                                // delete client by id
        {
            Database.Clients.Delete(id.Value);
            Database.SaveAsync();
        }

        //public void GiveTheBook(int? Bookid, ClientDTO currentClientDto)
        //{
        //    if (Bookid == null)
        //        throw new ArgumentNullException();

        //    int maxBooksInOneHand = 5;                                          //max book thet librarian can give to one person
        //    bool BookExist = false;
        //    IEnumerable<BookDTO> allBooks = GetBookList();

        //    foreach (var item in allBooks)                                      //Look for book id, to see if the Book is in the List             
        //        if (item.BookId == Bookid.Value)
        //            BookExist = true;


        //    Client client = Database.Clients.Get(currentClientDto.ClientID);    //geting from databas client object
        //    Book curentBook = Database.Books.Get(Bookid.Value);

        //    if (client.OrderedBooks.Count() < maxBooksInOneHand)                //chacking how many books client olredy have            
        //        //my expeption TODO
        //        throw new Exception();                                          // you cant give more books to this person. limit is  " maxBooksInOneHand "



        //    if (BookExist)                                                          //if book is in the database
        //    {
        //        client.OrderedBooks.Add(curentBook);                                //adding book to Clients Order List of Books

        //        Database.Clients.Update(client);                                    //updating Clients information in database

        //        Database.Orders.Create(new Order()                                  //Create new Order
        //        {
        //            OrderDate = DateTime.Now,
        //            IsGiven = true                                                  //Current Order is served now
        //        });
        //        Database.Save();
        //    } 
        //    else
        //       // Book Not Found Validation exeption
        //       throw new ArgumentNullException(); 
        //}             // give book(id) to chosen client
    }
}

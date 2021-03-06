﻿using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Book
    {
        public int BookId { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [DataType(DataType.Date)]
        public DateTime Year { get; set; }

        [Required]
        public string Ganer { get; set; }

        [Required]
        public string Author { get; set; }


        /// <summary>
        /// Client Id who to the book
        /// </summary>
        public Client ClientId { get; set; }   
    }
}

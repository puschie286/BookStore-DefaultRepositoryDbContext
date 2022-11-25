﻿using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace BookStore.Books
{
    public interface IBookAppService :
        ICrudAppService< //Defines CRUD methods
            BookDto, //Used to show books
            Guid, //Primary key of the book entity
            PagedAndSortedResultRequestDto> //Used to create/update a book
    {
		
	}
}
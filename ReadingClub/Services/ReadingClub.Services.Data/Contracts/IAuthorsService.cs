﻿using System.Linq;

using ReadingClub.Data.Models;

namespace ReadingClub.Services.Data.Contracts
{
    public interface IAuthorsService
    {
        Author GetBookAuthorByName(string authorName);

        IQueryable<Author> GetAuthorsWithDeleted();
    }
}

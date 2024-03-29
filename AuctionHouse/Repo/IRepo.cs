﻿using AuctionHouse.Models;
using Nancy.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionHouse.Repo
{
    public interface IRepo<T> where T: Entity
    {
        List<T> List { get; }
        void Add(T entity);
        void Delete(T entity);
        List<T> Update(T entity, IUserIdentity user);
        T FindById(int id);
    }
}

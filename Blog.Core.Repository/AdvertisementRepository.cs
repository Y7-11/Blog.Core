using Blog.Core.IRepository;
using Blog.Core.Model.Models;
using Blog.Core.Repository.sugar;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Blog.Core.Repository
{
    public class AdvertisementRepository : IAdvertisementRepository
    {
        private DbContext context;
        private SqlSugarClient db;
        private SimpleClient<Advertisement> entityDB;
        internal SqlSugarClient Db
        {
            get { return db; }
            private set { db = value; }
        }
        public DbContext Context
        {
            get { return context; }
            set { context = value; }
        }
        public AdvertisementRepository()
        {
            DbContext.Init(BaseDBConfig.ConnectionString);
            context = DbContext.GetDbContext();
            db = context.Db;
            entityDB = context.GetEntityDB<Advertisement>(db);
        }
        public int Add(Advertisement model)
        {
            throw new NotImplementedException();
        }

        public bool Delete(Advertisement model)
        {
            throw new NotImplementedException();
        }

        public List<Advertisement> Query(Expression<Func<Advertisement, bool>> whereExpression)
        {
            throw new NotImplementedException();
        }

        public int Sum(int i, int j)
        {
            return i + j;
        }

        public bool Update(Advertisement model)
        {
            throw new NotImplementedException();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using car_rent_backend.common;
using Microsoft.EntityFrameworkCore;

namespace car_rent_backend.repository
{
    public abstract class RepositoryBase<M> : IRepositoryBase<M> where M : class
    {
        protected readonly DbContextOptions<ProjectContext> Options;

        protected RepositoryBase(DbContextOptions<ProjectContext> options)
        {
            Options = options;
        }

        public List<M> GetAll()
        {
            using var context = new ProjectContext(Options);

            var table = context.Set<M>();

            return table.ToList();
        }

        public M GetSingle(int id)
        {
            using var context = new ProjectContext(Options);

            var existing = context.Set<M>().Find(id);

            if (existing == null)
            {
                throw new NotFoundException("Entity not found");
            }

            return existing;
        }

        public M Save(M entity)
        {
            using var context = new ProjectContext(Options);

            var table = context.Set<M>();
            var attach = table.Attach(entity);
            context.SaveChanges();

            return attach.Entity;
        }

        public M Update(M entity)
        {
            using var context = new ProjectContext(Options);

            var table = context.Set<M>();
            var attach = table.Attach(entity);
            context.Entry(entity).State = EntityState.Modified;
            context.SaveChanges();

            return attach.Entity;
        }

        public M Delete(int id)
        {
            using var context = new ProjectContext(Options);

            var table = context.Set<M>();
            var existing = table.Find(id);

            try
            {
                table.Remove(existing);
                context.SaveChanges();
            }
            catch (ArgumentNullException e)
            {
                throw new NotFoundException("Entity not found", e);
            }
            catch (DbUpdateException e)
            {
                throw new CouldNotBeDeletedException("Entity could not be deleted", e);
            }

            return existing;
        }
    }
}

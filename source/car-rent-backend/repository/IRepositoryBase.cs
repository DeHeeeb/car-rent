using System.Collections.Generic;

namespace car_rent_backend.repository
{
    internal interface IRepositoryBase<M>
    {
        List<M> GetAll();

        M GetSingle(int id);

        M Save(M entity);

        M Update(M entity);

        M Delete(int id);
    }
}

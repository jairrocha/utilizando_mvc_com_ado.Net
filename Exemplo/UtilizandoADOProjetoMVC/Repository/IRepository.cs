using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UtilizandoADOProjetoMVC.Repository
{
    public interface IRepository<T>
    { 
        IEnumerable<T> RetornarTodos();
        T RetornarPorId(int? Id);
        void Inserir(T model);
        void Update(T model);
        void Delete(T model);


    }
}

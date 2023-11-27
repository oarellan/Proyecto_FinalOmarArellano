using System.Threading.Tasks;

namespace ProyectoFinal.OmarArellano.Infrastructure.Interface
{
    public interface IGenericRepository<T> where T: class
    {

        #region Métodos Asíncronos
        Task<bool> InsertAsync(T entity);
       
        #endregion
    }
}

// using BusinessLogic.Interfaces;
// using DataAccess.Interfaces;
//
// namespace BusinessLogic.Services;
//
// internal class GenericService<T> : IGenericService<T> where T : class
// {
//     protected readonly IGenericRepository<T> repository;
//
//     public GenericService(IGenericRepository<T> repository)
//     {
//         this.repository = repository;
//     }
//
//     public async Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
//     {
//         return await repository.GetByIdAsync(id, cancellationToken);
//     }
//     
//     public async Task DeleteByIdAsync(int id, CancellationToken cancellationToken = default)
//     {
//         await repository.DeleteByIdAsync(id, cancellationToken);
//     }
// }

using Microsoft.EntityFrameworkCore;

namespace Mc2.CrudTest.Application.Interfaces;

public interface IMc2DbContext
{
    DbSet<T> Set<T>() where T : class;
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken());
}
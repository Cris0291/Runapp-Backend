namespace RunnApp.Application.Common.Interfaces
{
    public interface IUnitOfWorkPattern
    {
        Task<int> CommitChangesAsync();
    }
}

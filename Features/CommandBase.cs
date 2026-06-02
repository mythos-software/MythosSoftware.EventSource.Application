using MythosSoftware.EventSource.Application.Interfaces.Repositories;
using MythosSoftware.EventSource.Domain.Entities;
using MythosSoftware.EventSource.Domain.Events;

namespace MythosSoftware.EventSource.Application.Features;

public abstract class CommandBase<T>(IGenericEventRepository<T> repository) where T : EntityBase
{
    #region Protected Methods
    
    protected async Task<string> HandleCreateAsync(T request, int userModifiedId, CancellationToken cancellationToken)
    {
        var audit = CreateAuditContext(userModifiedId);
        
        request.Create(request.Id, request, audit);
        await repository.SaveAsync(request, cancellationToken);
        
        return request.Id;
    }
    
    protected async Task<string> HandleUpdateAsync(T request, int userModifiedId, CancellationToken cancellationToken)
    {
        var audit = CreateAuditContext(userModifiedId);
        
        request.Update(request.Id, request, audit);
        await repository.SaveAsync(request, cancellationToken);
        
        return request.Id;
    }

    public async Task<string> HandleDeleteAsync(T request, int userModifiedId, CancellationToken cancellationToken)
    {
        var audit = CreateAuditContext(userModifiedId);
        
        request.Delete(request.Id, audit);
        await repository.SaveAsync(request, cancellationToken);
        
        return request.Id;
    }
    
    #endregion

    #region Private Methods

    private AuditContext CreateAuditContext(int userModifiedId)
    {
        var correlationId = Guid.NewGuid().ToString("N");
        var causationId = Guid.NewGuid().ToString("N");
        return AuditContext.From(userModifiedId.ToString(), correlationId, causationId);
    }

    #endregion
}
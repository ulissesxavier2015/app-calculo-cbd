using Api.Infrastructure.ExternalApi;

namespace Api.Domain.Interfaces;

public interface ICdbService
{

    public Task<CdbOutput> ObterTaxaCdb();

}

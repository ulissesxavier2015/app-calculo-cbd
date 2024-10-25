using System.ComponentModel.DataAnnotations;
using Api.Domain.Interfaces;
using Api.Infrastructure.ExternalApi.Config;
using Microsoft.Extensions.Options;

namespace Api.Infrastructure.ExternalApi;

public class CdbService: ICdbService {


    private readonly HttpClient _client;    

    public CdbService(HttpClient client, IOptions<CdbConfig> cdbConfig)
    {        
        _client = client;
        _client.DefaultRequestHeaders.Accept.Clear();
        _client.BaseAddress = new Uri(cdbConfig.Value.Url);        
    }

    public async Task<CdbOutput> ObterTaxaCdb()
    {
        var response = await _client.GetFromJsonAsync<CdbOutput>("cdb");

        if (response == null)
            throw new Exception("Nenhum cdb foi encontrado");

        return response;
    }
}

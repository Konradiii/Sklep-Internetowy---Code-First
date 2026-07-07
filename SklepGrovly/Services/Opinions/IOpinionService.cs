using SklepGrovly.DTOs.Opinions;

namespace SklepGrovly.Services.Opinions;

public interface IOpinionService
{
    Task CreateOpinion(int klientId, CreateOpinionDto dto, CancellationToken token);
    
    Task EditOpinion(int opinionId, int klientId, bool isAdmin, EditOpinionDto dto, CancellationToken token);
    
    Task DeleteOpinion(int opinionId, int klientId, bool isAdmin, CancellationToken token);


}
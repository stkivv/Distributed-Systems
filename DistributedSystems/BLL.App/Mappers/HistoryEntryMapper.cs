using AutoMapper;
using BLL.DTO;
using DAL.Base;

namespace BLL.App.Mappers;

public class HistoryEntryMapper : BaseMapper<BLL.DTO.HistoryEntry, Domain.HistoryEntry>
{
    public HistoryEntryMapper(IMapper mapper) : base(mapper)
    {
    }
}
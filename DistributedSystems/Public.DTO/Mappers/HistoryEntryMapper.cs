using AutoMapper;
using DAL.Base;

namespace Public.DTO.Mappers;

public class HistoryEntryMapper : BaseMapper<BLL.DTO.HistoryEntry, Public.DTO.v1.HistoryEntry>
{
    public HistoryEntryMapper(IMapper mapper) : base(mapper)
    {
    }
}
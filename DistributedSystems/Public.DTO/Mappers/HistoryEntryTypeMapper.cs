using AutoMapper;
using DAL.Base;

namespace Public.DTO.Mappers;

public class HistoryEntryTypeMapper : BaseMapper<BLL.DTO.HistoryEntryType, Public.DTO.v1.HistoryEntryType>
{
    public HistoryEntryTypeMapper(IMapper mapper) : base(mapper)
    {
    }
}
using AutoMapper;
using BLL.DTO;
using DAL.Base;

namespace BLL.App.Mappers;

public class HistoryEntryTypeMapper : BaseMapper<BLL.DTO.HistoryEntryType, Domain.HistoryEntryType>
{
    public HistoryEntryTypeMapper(IMapper mapper) : base(mapper)
    {
    }
}
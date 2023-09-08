using AutoMapper;
using BLL.DTO;
using DAL.Base;

namespace BLL.App.Mappers;

public class MonthMapper : BaseMapper<BLL.DTO.Month, Domain.Month>
{
    public MonthMapper(IMapper mapper) : base(mapper)
    {
    }
}
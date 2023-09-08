
using AutoMapper;
using BLL.DTO;
using DAL.Base;

namespace BLL.App.Mappers;

public class ReminderTypeMapper : BaseMapper<BLL.DTO.ReminderType, Domain.ReminderType>
{
    public ReminderTypeMapper(IMapper mapper) : base(mapper)
    {
    }
}
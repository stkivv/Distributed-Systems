
using AutoMapper;
using BLL.DTO;
using DAL.Base;

namespace BLL.App.Mappers;

public class PestTypeMapper : BaseMapper<BLL.DTO.PestType, Domain.PestType>
{
    public PestTypeMapper(IMapper mapper) : base(mapper)
    {
    }
}
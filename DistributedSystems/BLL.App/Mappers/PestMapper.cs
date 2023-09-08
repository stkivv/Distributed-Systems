using AutoMapper;
using BLL.DTO;
using DAL.Base;

namespace BLL.App.Mappers;

public class PestMapper : BaseMapper<BLL.DTO.Pest, Domain.Pest>
{
    public PestMapper(IMapper mapper) : base(mapper)
    {
    }
}
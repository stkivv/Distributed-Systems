
using AutoMapper;
using BLL.DTO;
using DAL.Base;

namespace BLL.App.Mappers;

public class TagMapper : BaseMapper<BLL.DTO.Tag, Domain.Tag>
{
    public TagMapper(IMapper mapper) : base(mapper)
    {
    }
}
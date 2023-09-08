using AutoMapper;
using DAL.Base;

namespace Public.DTO.Mappers;

public class TagMapper : BaseMapper<BLL.DTO.Tag, Public.DTO.v1.Tag>
{
    public TagMapper(IMapper mapper) : base(mapper)
    {
    }
}
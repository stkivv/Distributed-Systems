using AutoMapper;
using DAL.Base;

namespace Public.DTO.Mappers;

public class TagColorMapper : BaseMapper<BLL.DTO.TagColor, Public.DTO.v1.TagColor>
{
    public TagColorMapper(IMapper mapper) : base(mapper)
    {
    }
}
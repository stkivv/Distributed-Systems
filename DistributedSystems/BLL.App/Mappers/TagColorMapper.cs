
using AutoMapper;
using BLL.DTO;
using DAL.Base;

namespace BLL.App.Mappers;

public class TagColorMapper : BaseMapper<BLL.DTO.TagColor, Domain.TagColor>
{
    public TagColorMapper(IMapper mapper) : base(mapper)
    {
    }
}
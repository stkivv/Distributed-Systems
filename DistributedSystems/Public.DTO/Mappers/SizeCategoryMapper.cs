using AutoMapper;
using DAL.Base;

namespace Public.DTO.Mappers;

public class SizeCategoryMapper : BaseMapper<BLL.DTO.SizeCategory, Public.DTO.v1.SizeCategory>
{
    public SizeCategoryMapper(IMapper mapper) : base(mapper)
    {
    }
}
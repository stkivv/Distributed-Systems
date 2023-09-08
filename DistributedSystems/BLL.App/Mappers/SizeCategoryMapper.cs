
using AutoMapper;
using BLL.DTO;
using DAL.Base;

namespace BLL.App.Mappers;

public class SizeCategoryMapper : BaseMapper<BLL.DTO.SizeCategory, Domain.SizeCategory>
{
    public SizeCategoryMapper(IMapper mapper) : base(mapper)
    {
    }
}
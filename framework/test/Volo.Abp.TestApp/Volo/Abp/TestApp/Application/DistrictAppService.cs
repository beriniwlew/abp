﻿using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.TestApp.Domain;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.TestApp.Application.Dto;

namespace Volo.Abp.TestApp.Application;

//This is especially used to test the AbstractKeyCrudAppService
public class DistrictAppService : AbstractKeyCrudAppService<District, DistrictDto, DistrictKey>
{
    public DistrictAppService(IRepository<District> repository)
        : base(repository)
    {
    }

    protected async override Task DeleteByIdAsync(DistrictKey id)
    {
        await Repository.DeleteAsync(d => d.CityId == id.CityId && d.Name == id.Name);
    }

    protected async override Task<District> GetEntityByIdAsync(DistrictKey id)
    {
        return await AsyncExecuter.FirstOrDefaultAsync(
             (await Repository.GetQueryableAsync()).Where(d => d.CityId == id.CityId && d.Name == id.Name)
        );
    }
}

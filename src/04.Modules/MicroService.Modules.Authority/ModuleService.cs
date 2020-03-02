
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Data;
using Surging.Core.CPlatform.Ioc;
using Surging.Core.ProxyGenerator;
using MicroService.Data.Common;
using MicroService.Data.Extensions;
using MicroService.Data.Validation;
using MicroService.Common.Models;
using MicroService.IApplication.Authority;
using MicroService.IApplication.Authority.Dto;
using MicroService.IModules.Authority;
using Newtonsoft.Json;
using MicroService.IApplication.Authority.IDomainService;

namespace MicroService.Modules.Authority
 {
	/// <summary>
	/// Module -Module实现
	/// </summary>
	[ModuleName("Module")]
	public class ModuleService: ProxyServiceBase, IModuleService
	{
	    private readonly IModuleAppService _moduleAppService;
        private IModuleDomainService _moduleDomainService; 
		private readonly ApplicationEnginee _applicationEnginee;
        public ModuleService(IModuleAppService moduleAppService, IModuleDomainService moduleDomainService)
        {
            _moduleAppService = moduleAppService;
            _moduleDomainService= moduleDomainService;
            _applicationEnginee = new ApplicationEnginee();
        }
		/// <summary>
        /// 新增
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<JsonResponse> Create(ModuleRequestDto input)
        {
            return await SaveModule(input);

        }
     
        /// <summary>
        /// 批量新增
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<JsonResponse> BatchCreate(ModuleBatchRequestDto input)
        {
			foreach (var moduleRequestDto in input.ModuleRequestList)
            {
                moduleRequestDto.InitCreateRequest(input.Payload);
            }
            var resJson = await _applicationEnginee.TryTransactionAsync(async () =>
            {
               await _moduleAppService.BatchCreateAsync(input.ModuleRequestList);
            });
            return resJson;
        }
        /// <summary>
        /// 分页获取
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PageData> GetPageList(ModulePageRequestDto input)
        { 
			input.InitRequest();
            return await _moduleAppService.GetPageListAsync(input);
        }

        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<ModuleQueryDto> GetForModify(EntityQueryRequest input)
        {
			input.InitRequest();
            return await _moduleDomainService.GetModuleDetail(input);
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<JsonResponse> Modify(ModuleRequestDto input)
        {
            return await SaveModule(input);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<JsonResponse> Remove(EntityRequest input)
        {
			input.InitModifyRequest();
           
            return await _moduleDomainService.RemoveModule(input);
        }
        /// <summary>
        /// 获取全部
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<List<ModuleQueryDto>> GetAll(EntityRequest input)
        {
            return await _moduleAppService.GetAll();
        }
        // <summary>
        /// 获取全部
        /// </summary>
        /// <returns></returns>
        public async Task<IList<ModuleQueryDto>> GetModulesWithElements(EntityRequest input)
        {
            return await _moduleDomainService.GetModulesWithElements(input);
        }

        #region private
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        async Task<JsonResponse> SaveModule(ModuleRequestDto input)
        {
            input.InitCreateRequest();
            input.InitModifyRequest();
            foreach (var moduleRequestDto in input.ModuleElementActionRequests)
            {
                moduleRequestDto.InitCreateRequest(input.Payload);
                moduleRequestDto.InitModifyRequest(input.Payload);
            }
            var resJson = await _moduleDomainService.SaveModule(input);
            //var resJson = await _applicationEnginee.TryTransactionAsync(async () =>
            //{
            //    await _moduleAppService.CreateAsync(input);
            //});
            return resJson;
        }
        #endregion
    }
}
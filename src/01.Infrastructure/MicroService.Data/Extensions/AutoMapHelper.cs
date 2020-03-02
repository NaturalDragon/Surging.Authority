using AutoMapper;
using MicroService.Common.Models;
using MicroService.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace MicroService.Data.Ext
{
    public static class AutoMapHelper
    {
        /// <summary>
        /// 集合列表类型映射,默认字段名字一一对应
        /// </summary>
        /// <typeparam name="TDestination">转化之后的model，可以理解为viewmodel</typeparam>
        /// <typeparam name="TSource">要被转化的实体，Entity</typeparam>
        /// <param name="source">可以使用这个扩展方法的类型，任何引用类型</param>
        /// <returns>转化之后的实体列表</returns>
        public static IEnumerable<TDestination> MapToList<TSource, TDestination>(this IEnumerable<TSource> source)
            where TDestination : class
            where TSource : class
        {
            if (source == null) return new List<TDestination>();
            var config = new MapperConfiguration(cfg => cfg.CreateMap<TSource, TDestination>());
            var mapper = config.CreateMapper();
            return mapper.Map<List<TDestination>>(source);
        }
        public static TDestination MapToEntity<TSource, TDestination>(this TSource source,TDestination destination)
         where TDestination : Entity<string>
         where TSource : RequestData
        {

            var config = new MapperConfiguration(cfg =>
                cfg.CreateMap<TSource, TDestination>()
             //.ForMember(dest => dest., opt => opt.Ignore())
             .ForAllMembers(opt => opt.Condition((src, dest, sourceMember) => sourceMember != null )));

            var mapper = config.CreateMapper();

            return mapper.Map<TSource,TDestination>(source,destination);
        }


        public static TDestination MapToCreateEntity<TSource, TDestination>(this TSource source)
        where TDestination : Entity<string>
        where TSource : RequestData
        {

            var config = new MapperConfiguration(cfg =>
                cfg.CreateMap<TSource, TDestination>()
             .ForMember(dest => dest.ModifyDate, opt => opt.Ignore())
              .ForMember(dest => dest.ModifyUserId, opt => opt.Ignore())
               .ForMember(dest => dest.ModifyUserName, opt => opt.Ignore())
             .ForAllMembers(opt => opt.Condition((src, dest, sourceMember) => sourceMember != null)));

            var mapper = config.CreateMapper();

            return mapper.Map<TDestination>(source);
        }



        /// <summary>
        /// 集合列表类型映射,默认字段名字一一对应
        /// </summary>
        /// <typeparam name="TDestination">转化之后的model，可以理解为viewmodel</typeparam>
        /// <typeparam name="TSource">要被转化的实体，Entity</typeparam>
        /// <param name="source">可以使用这个扩展方法的类型，任何引用类型</param>
        /// <returns>转化之后的实体列表</returns>
        public static IEnumerable<TDestination> MapToCreateEntities<TSource, TDestination>(this IEnumerable<TSource> source)
            where TDestination : Entity<string>
            where TSource : RequestData
        {
            if (source == null) return new List<TDestination>();
            var config = new MapperConfiguration(cfg => cfg.CreateMap<TSource, TDestination>()
            .ForMember(dest => dest.ModifyDate, opt => opt.Ignore())
              .ForMember(dest => dest.ModifyUserId, opt => opt.Ignore())
                .ForMember(dest => dest.ModifyUserName, opt => opt.Ignore()));
            var mapper = config.CreateMapper();
            return mapper.Map<List<TDestination>>(source);
        }

        public static TDestination MapToModifyEntity<TSource, TDestination>(this TSource source, TDestination destination)
        where TDestination : Entity<string>
        where TSource : RequestData
        {

            var config = new MapperConfiguration(cfg =>
                cfg.CreateMap<TSource, TDestination>()
             .ForMember(dest =>dest.CreateDate, opt => opt.Ignore())
              .ForMember(dest => dest.CreateUserId, opt => opt.Ignore())
                .ForMember(dest => dest.CreateUserName, opt => opt.Ignore())
             .ForAllMembers(opt => opt.Condition((src, dest, sourceMember) => sourceMember != null)));

            var mapper = config.CreateMapper();

            return mapper.Map<TSource, TDestination>(source, destination);
        }

        public static IList<TDestination> MapToModifyEntities<TSource, TDestination>(this IList<TSource> sources, IList<TDestination> destinations)
       where TDestination : Entity<string>
       where TSource : BaseDto
        {

            var config = new MapperConfiguration(cfg =>
                cfg.CreateMap<TSource, TDestination>()
             .ForMember(dest => dest.CreateDate, opt => opt.Ignore())
               .ForMember(dest => dest.CreateUserId, opt => opt.Ignore())
                .ForMember(dest => dest.CreateUserName, opt => opt.Ignore())
             .ForAllMembers(opt => opt.Condition((src, dest, sourceMember) => sourceMember != null)));

            var mapper = config.CreateMapper();
            var destinationList = new List<TDestination>();
            foreach (var item in sources)
            {
                var destination = destinations.Where(d => d.Id == item.Id).FirstOrDefault();
                if (destination != null)
                {
                    var mapDestination = mapper.Map<TSource, TDestination>(item, destination);
                    destinationList.Add(mapDestination);
                }
               
            }
            return destinationList;//mapper.Map<IList<TSource>, IList<TDestination>>(sources, destinations);
        }
        public static TDestination MapEntity<TSource, TDestination>(this TSource source)
  where TDestination : RequestData
  where TSource : Entity<string>
        {

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TSource, TDestination>()
             .ForMember(dest => dest.Payload, opt => opt.Ignore()));
            var mapper = config.CreateMapper();

            return mapper.Map<TDestination>(source);
        }
      
      
        public static TDestination Map<TSource, TDestination>(this TSource source, TDestination destination,
           Expression<Func<TSource, object>> sourceMember, Action<ISourceMemberConfigurationExpression> memberOptions)
        {
            var configuration = new MapperConfiguration(x => x.CreateMap<TSource, TDestination>().ForSourceMember(sourceMember, memberOptions));
            var mapper = configuration.CreateMapper();
            var result = mapper.Map<TSource, TDestination>(source, destination);

            return result;
        }
    } 


}

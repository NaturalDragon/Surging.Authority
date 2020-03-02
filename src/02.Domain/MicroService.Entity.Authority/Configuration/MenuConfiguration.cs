
using MicroService.Data.Mapping;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

 namespace MicroService.Entity.Authority.Configuration
 {
	/// <summary>
	/// Menu -映射
	/// </summary>
	public class MenuConfigruation:EntityMappingConfiguration<Menu>
	{
  
         public override void Map(EntityTypeBuilder<Menu> b)
			{ 
			   b.ToTable("Menu").HasKey(p => p.Id);
					   				          
			   b.Property(p => p.Name).HasMaxLength(100); 
							 			   			
	      			   				          
			   b.Property(p => p.Icon).HasMaxLength(32); 
							 			   			
	      			   				          
			   b.Property(p => p.ModuleId).HasMaxLength(36); 
							 			   			
	      			   				          
			   b.Property(p => p.ParentId).HasMaxLength(36); 
							 			   			
	      		        b.Property(p => p.IsDelete).IsRequired(); 
                b.Property(p => p.CreateDate).IsRequired();
				b.Property(p => p.CreateUserId).IsRequired().HasMaxLength(36);
				b.Property(p => p.CreateUserName).IsRequired().HasMaxLength(32);
                b.Property(p => p.ModifyUserId).HasMaxLength(36);;
				b.Property(p => p.ModifyDate);
				b.Property(p => p.ModifyUserName).HasMaxLength(32);
			  }
	}
}
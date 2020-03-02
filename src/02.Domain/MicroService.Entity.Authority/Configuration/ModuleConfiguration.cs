
using MicroService.Data.Mapping;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

 namespace MicroService.Entity.Authority.Configuration
 {
	/// <summary>
	/// Module -映射
	/// </summary>
	public class ModuleConfigruation:EntityMappingConfiguration<Module>
	{
  
         public override void Map(EntityTypeBuilder<Module> b)
			{ 
			   b.ToTable("Module").HasKey(p => p.Id);
					   				          
			   b.Property(p => p.Name).HasMaxLength(100); 
							 			   			
	      			   				          
			   b.Property(p => p.Url).HasMaxLength(255); 
							 			   			
	      			   				           
			   b.Property(p => p.IsEnabled).IsRequired(); 
														
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

using MicroService.Data.Mapping;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

 namespace MicroService.Entity.Authority.Configuration
 {
	/// <summary>
	/// OrganizationMenu -映射
	/// </summary>
	public class OrganizationMenuConfigruation:EntityMappingConfiguration<OrganizationMenu>
	{
  
         public override void Map(EntityTypeBuilder<OrganizationMenu> b)
			{ 
			   b.ToTable("OrganizationMenu").HasKey(p => p.Id);
					   				        			   b.Property(p => p.OrganizationId).IsRequired().HasMaxLength(36); 
			            				   			
	      			   				        			   b.Property(p => p.MenuId).IsRequired().HasMaxLength(36); 
			            				   			
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
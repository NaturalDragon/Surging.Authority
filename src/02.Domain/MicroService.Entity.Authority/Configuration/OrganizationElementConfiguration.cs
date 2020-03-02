
using MicroService.Data.Mapping;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

 namespace MicroService.Entity.Authority.Configuration
 {
	/// <summary>
	/// OrganizationElement -映射
	/// </summary>
	public class OrganizationElementConfigruation:EntityMappingConfiguration<OrganizationElement>
	{
  
         public override void Map(EntityTypeBuilder<OrganizationElement> b)
			{ 
			   b.ToTable("OrganizationElement").HasKey(p => p.Id);
					   				        			   b.Property(p => p.OrganizationId).IsRequired().HasMaxLength(36); 
			            				   			
	      			   				        			   b.Property(p => p.ElementId).IsRequired().HasMaxLength(36); 
			            				   			
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

using MicroService.Data.Mapping;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

 namespace MicroService.Entity.Authority.Configuration
 {
	/// <summary>
	/// Employee -映射
	/// </summary>
	public class EmployeeConfigruation:EntityMappingConfiguration<Employee>
	{
  
         public override void Map(EntityTypeBuilder<Employee> b)
			{ 
			   b.ToTable("Employee").HasKey(p => p.Id);
					   				        			   b.Property(p => p.UserId).IsRequired().HasMaxLength(255); 
			            				   			
	      			   				        			   b.Property(p => p.Name).IsRequired().HasMaxLength(100); 
			            				   			
	      			   				        			   b.Property(p => p.Password).IsRequired().HasMaxLength(255); 
			            				   			
	      			   				        			   b.Property(p => p.Department).IsRequired().HasMaxLength(200); 
			            				   			
	      			   				        			   b.Property(p => p.Order).IsRequired().HasMaxLength(200); 
			            				   			
	      			   				          
			   b.Property(p => p.Position).HasMaxLength(100); 
							 			   			
	      			   				        			   b.Property(p => p.Mobile).IsRequired().HasMaxLength(64); 
			            				   			
	      			   				          
			   b.Property(p => p.Telephone).HasMaxLength(64); 
							 			   			
	      			   				           
			   b.Property(p => p.Gender).IsRequired(); 
														
	      			   				          
			   b.Property(p => p.Email).HasMaxLength(64); 
							 			   			
	      			   				          
			   b.Property(p => p.WorkPlace).HasMaxLength(255); 
							 			   			
	      			   				          
			   b.Property(p => p.EnglishName).HasMaxLength(64); 
							 			   			
	      			   				          
			   b.Property(p => p.Lang).HasMaxLength(64); 
							 			   			
	      			   				          
			   b.Property(p => p.Jobnumber).HasMaxLength(64); 
							 			   			
	      			   				          
			   b.Property(p => p.Salt).HasMaxLength(32); 
							 			   			
	      			   				          			   b.Property(p => p.IsHide); 
														
	      			   				          			   b.Property(p => p.IsSenior); 
														
	      			   				          			   b.Property(p => p.IsLeader); 
														
	      			   				          
			   b.Property(p => p.Extra).HasMaxLength(255); 
							 			   			
	      			   				          			   b.Property(p => p.ErrorCount); 
														
	      			   				          			   b.Property(p => p.ErrotTime); 
														
	      			   				          			   b.Property(p => p.IsLock); 
														
	      			   				          
			   b.Property(p => p.Avatar).HasMaxLength(255); 
							 			   			
	      			   				          
			   b.Property(p => p.LastLoginRoleId).HasMaxLength(36); 
							 			   			
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
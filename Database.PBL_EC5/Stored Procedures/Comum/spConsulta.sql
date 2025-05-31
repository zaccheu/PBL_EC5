create procedure [dbo].[spConsulta] 
( 
  @id int  , 
  @tabela varchar(max) 
) 
as 
begin 
   declare @sql varchar(max); 
   set @sql = 'select * from  ' + @tabela +  
        ' where id = ' + cast(@id as varchar(max)) 
   exec(@sql) 
end 
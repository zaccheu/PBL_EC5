create procedure spDelete 
( 
  @id int  , 
  @tabela varchar(max) 
) 
as 
begin 
   declare @sql varchar(max); 
   set @sql = ' delete ' + @tabela +  
       ' where id = ' + cast(@id as varchar(max)) 
   exec(@sql) 
end
 
 


create procedure spListagem 
( 
   @tabela varchar(max), 
   @ordem varchar(max)) 
as 
begin 
   exec('select * from ' + @tabela +  
        ' order by ' + @ordem) 
end 

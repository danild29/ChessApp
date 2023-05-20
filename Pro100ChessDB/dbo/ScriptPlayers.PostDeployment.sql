/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/
/*
if not exists (select 1 from dbo.[Players])
begin
    insert into dbo. [Players] (NickName, Email, Pasword)
    values ('Tom', 'tom@gamil.com', '111'),
    ('Ivan', 'ivan@gamil.com', '222');
    
    insert into dbo. [Players] (NickName, Email, Pasword, Rating, IsPremium)
    values ('Jack', 'jack@gamil.com', '333', 1000, 1),
    ('Luck', 'luck@gamil.com', '444', 500, 0);
end
*/
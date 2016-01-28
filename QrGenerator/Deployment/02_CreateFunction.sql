
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fu_CLR_GenerateQR]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT')) 
DROP FUNCTION [dbo].[fu_CLR_GenerateQR] 
GO 



CREATE FUNCTION [dbo].fu_CLR_GenerateQR(@Input nvarchar(4000)) RETURNS varbinary(max) 
EXTERNAL NAME  [ThoughtWorks.QRCode].[ThoughtWorks.QRCode.SqlServer].GenerateQR 
-- Assembly.Namespace.Method 

GO 


SELECT dbo.fu_CLR_GenerateQR('hello world') 

GO 

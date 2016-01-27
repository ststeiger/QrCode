

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fu_CLR_GenerateQR]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[fu_CLR_GenerateQR]
GO


IF  EXISTS (SELECT * FROM sys.assemblies asms WHERE asms.name = N'ThoughtWorks.QRCode' and is_user_defined = 1)
DROP ASSEMBLY [ThoughtWorks.QRCode]

GO


/*

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fu_CLR_GenerateQR]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[fu_CLR_GenerateQR]
GO



CREATE FUNCTION [dbo].fu_CLR_GenerateQR(@Input nvarchar(4000)) RETURNS varbinary(max)
EXTERNAL NAME  [ThoughtWorks.QRCode].[ThoughtWorks.QRCode.SqlServer].GenerateQR
-- assembly.namespace.Method
*/
 
SELECT dbo.fu_CLR_GenerateQR('hello world') 



Go 


--CREATE FUNCTION [dbo].Decrypt(@Input nvarchar(max)) RETURNS nvarchar(max)
--EXTERNAL NAME EDCLR.EDCLR.Decrypt; 
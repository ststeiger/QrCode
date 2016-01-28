
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fu_CLR_GenerateQR]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT')) 
DROP FUNCTION [dbo].[fu_CLR_GenerateQR] 

GO 


IF  EXISTS (SELECT * FROM sys.assemblies asms WHERE asms.name = N'ThoughtWorks.QRCode' and is_user_defined = 1) 
DROP ASSEMBLY [ThoughtWorks.QRCode] 

GO 

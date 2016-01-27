
USE YOUR_DATABASE
GO

sp_configure 'show advanced options', 1;
GO
RECONFIGURE;
GO
sp_configure 'clr enabled', 1;
GO
RECONFIGURE;
GO

ALTER DATABASE YOUR_DATABASE SET TRUSTWORTHY ON
GO

EXEC dbo.sp_changedbowner @loginame = N'sa', @map = false
GO



--Meldung 10301, Ebene 16, Status 1, Zeile 2
--Die ThoughtWorks.QRCode-Assembly verweist auf die system.drawing, version=4.0.0.0, culture=neutral,
 --publickeytoken=b03f5f7f11d50a3a.-Assembly, die in der aktuellen Datenbank nicht vorhanden ist. 
 --SQL Server hat versucht, die Assembly, auf die verwiesen wird, 
 --am gleichen Pfad wie die verweisende Assembly zu suchen und automatisch zu laden. 
 --Dieser Vorgang ist jedoch fehlgeschlagen 
 --(Ursache: 2(Das System kann die angegebene Datei nicht finden.)). 
 --Laden Sie die Assembly, auf die verwiesen wird, in die aktuelle Datenbank, und wiederholen Sie die Anforderung.

CREATE ASSEMBLY [System.Drawing]
 -- FROM 'C:\WINDOWS\Microsoft.NET\Framework\v2.0.50727\System.Drawing.dll'
 --FROM 'C:\Windows\Microsoft.NET\Framework64\v2.0.50727\System.Drawing.dll'
 FROM 'C:\Windows\assembly\GAC_MSIL\System.Drawing\2.0.0.0__b03f5f7f11d50a3a\System.Drawing.dll' 
 --FROM 'C:\Windows\Microsoft.Net\assembly\GAC_MSIL\System.Drawing\v4.0_4.0.0.0__b03f5f7f11d50a3a\System.Drawing.dll'
 WITH PERMISSION_SET = UNSAFE
GO

-- SELECT * FROM sys.dm_clr_properties WHERE name = 'directory' 

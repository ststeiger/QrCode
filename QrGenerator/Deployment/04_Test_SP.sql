
DECLARE @in_mandant varchar(3)
DECLARE @in_sprache varchar(2)
DECLARE @proc varchar(50)
DECLARE @in_standort varchar(36)
DECLARE @in_kunstler varchar(36)
DECLARE @in_kunstkategorie varchar(36)
DECLARE @in_stichtag varchar(50)


-- TODO: Set parameter values here.
SET @in_mandant = '0'
SET @in_sprache = 'DE'
SET @proc = 'administrator'
SET @in_standort = '00000000-0000-0000-0000-000000000000'
SET @in_kunstler = '00000000-0000-0000-0000-000000000000'
SET @in_kunstkategorie = '00000000-0000-0000-0000-000000000000'
SET @in_stichtag = '30.01.2015'



EXECUTE sp_RPT_DATA_KU_KunstlisteDetail_QR 
	 @in_mandant
	,@in_sprache
	,@proc
	,@in_standort
	,@in_kunstler
	,@in_kunstkategorie
	,@in_stichtag
;

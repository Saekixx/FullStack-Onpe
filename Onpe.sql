-- www.web.onpe.gob.pe/modElecciones/elecciones/elecciones2016/PRP2V2016/

set dateformat dmy
use master
if DB_ID ('Onpe') is not null
   drop database Onpe
go
create database Onpe
go
use Onpe

create table Departamento (
  idDepartamento int primary key identity,
  Detalle char(30) unique )

create table Provincia (
  idProvincia int primary key identity,
  idDepartamento int references Departamento,
  Detalle char(30) unique )
  
create table Distrito (
  idDistrito int primary key identity,
  idProvincia int references Provincia,
  Detalle char(50) not null )
  
create table Partido (
  idPartido int primary key identity,
  RazonSocial char(30) not null,
  CandidatoPresidencial char(30) not null,
  Imagen image )

create Table LocalVotacion (
  idLocalVotacion int primary key identity,
  idDistrito int references Distrito,
  RazonSocial char(100) not null,
  Direccion char(200) not null )

create table GrupoVotacion (
  idLocalVotacion int references LocalVotacion,
  idGrupoVotacion char(6) primary key,
  nCopia char(3) not null,
  idEstadoActa int not null,	-- 1. 'ACTA ELECTORAL NORMAL', 2. 'ACTA ELECTORAL RESUELTA'
  ElectoresHabiles int,
  TotalVotantes int,
  P1 int,
  P2 int,
  VotosBlancos int,
  VotosNulos int,
  VotosImpugnados int )
go


---------- Vistas ----------
create view vDepartamentos
	as select * from Departamento
go

create view vListaVotantes
  as select DE.idDepartamento, 
			DE.Detalle 'Departamento',
			PR.Detalle 'Provincia',
			DI.idDistrito, DI.Detalle 'Distrito',
			SUM( GV.TotalVotantes) 'TV',
			( SUM(GV.ElectoresHabiles) - SUM( GV.TotalVotantes) ) 'TA', SUM(GV.ElectoresHabiles) 'EH'
		from  Departamento DE, Provincia PR, Distrito DI, LocalVotacion LV, GrupoVotacion GV
		where DE.idDepartamento = PR.idDepartamento and
			  PR.idProvincia = DI.idProvincia and
			  DI.idDistrito = LV.idDistrito and 
			  LV.idLocalVotacion = GV.idLocalVotacion
		group by DE.idDepartamento, DE.Detalle, PR.Detalle, DI.idDistrito, DI.Detalle
go

create view vTotalVotos
  as select	SUM(TotalVotantes) 'Total Asistentes', 
			CONCAT ( CAST ( ( SUM(TotalVotantes) * 100.0 / SUM(ElectoresHabiles) ) as decimal (8,3) ), ' %' ) '% Total Asistentes',
			( SUM(ElectoresHabiles) - SUM(TotalVotantes) ) 'Total Ausentes',
			CONCAT ( CAST ( ( ( SUM(ElectoresHabiles) - SUM(TotalVotantes) ) * 100.0 / SUM(ElectoresHabiles) ) as decimal (8,3) ), ' %' ) '% Total Ausentes',
			SUM(ElectoresHabiles) 'Electores H biles' 
			from GrupoVotacion
go

---------- Procedimientos ----------

create procedure usp_getDepartamentos
  @inicio int,
  @fin int
   as select * from Departamento where idDepartamento BETWEEN @inicio and @fin
go   

create procedure usp_getProvincias
  @idDepartamento int
  as select idProvincia, Detalle from Provincia where idDepartamento = @idDepartamento
go

create procedure usp_getDistritos
  @idProvincia int
  as select idDistrito, Detalle from Distrito where idProvincia = @idProvincia
go

create procedure usp_getLocalesVotacion
  @idDistrito int
  as select idLocalVotacion, RazonSocial from LocalVotacion where idDistrito = @idDistrito
go

create procedure usp_getGruposVotacion
  @idLocalVotacion int
  as select idGrupoVotacion from GrupoVotacion where idLocalVotacion = @idLocalVotacion
go

create procedure usp_getGrupoVotacion
  @idGrupoVotacion char(6)
  as select DE.Detalle 'Departamento', PR.Detalle 'Provincia', DI.Detalle 'Distrito', LV.RazonSocial, LV.Direccion, GV.*
		from GrupoVotacion GV, LocalVotacion LV, Departamento DE, Provincia PR, Distrito DI
		where  LV.idDistrito = DI.idDistrito and PR.idProvincia = DI.idProvincia and DE.idDepartamento = PR.idDepartamento and
			   GV.idLocalVotacion = LV.idLocalVotacion and GV.idGrupoVotacion = @idGrupoVotacion
go

create procedure usp_getVotos
  @inicio int,
  @fin int
  as select TRIM(Departamento) 'DPD', SUM(TV) 'TV', CONCAT ( CAST ( ( SUM(TV) * 100.0 / SUM(EH) ) as decimal (8,3) ), ' %' ) 'PTV',
	   SUM(TA) 'TA', CONCAT ( CAST ( ( SUM(TA) * 100.0 / SUM(EH) ) as decimal (8,3) ), ' %' ) 'PTA', SUM(EH) 'EH' 
		from vListaVotantes 
		where idDepartamento BETWEEN @inicio and @fin
		group by idDepartamento, Departamento
		order by Departamento
go

create procedure usp_getVotosDepartamento
  @Departamento char(30)
  as select TRIM(Provincia) 'DPD', SUM(TV) 'TV', CONCAT ( CAST ( ( SUM(TV) * 100.0 / SUM(EH) ) as decimal (8,3) ), ' %' ) 'PTV',
		SUM(TA) 'TA', CONCAT ( CAST ( ( SUM(TA) * 100.0 / SUM(EH) ) as decimal (8,3) ), ' %' ) 'PTA',
		SUM(EH) 'EH' from vListaVotantes where Departamento = @Departamento group by Provincia
go

create procedure usp_getVotosProvincia
  @Provincia char(30)
  as select TRIM(Distrito) 'DPD', SUM(TV) 'TV', CONCAT ( CAST ( ( SUM(TV) * 100.0 / SUM(EH) ) as decimal (8,3) ), ' %' ) 'PTV',
		SUM(TA) 'TA', CONCAT ( CAST ( ( SUM(TA) * 100.0 / SUM(EH) ) as decimal (8,3) ), ' %' ) 'PTA',
		SUM(EH) 'EH' from vListaVotantes where Provincia = @Provincia group by Distrito
go

create procedure usp_getDistritosDepartamento
  @Departamento char(30)
  as select Di.idDistrito, Di.Detalle 'Distrito', P.Detalle 'Provincia' from Distrito Di, Provincia P, Departamento De
		where  De.idDepartamento = P.idDepartamento and P.idProvincia = Di.idProvincia and De.Detalle = @Departamento
go

create procedure usp_getLocalesVotacionDepartamento
  @Departamento char(30)
  as select LV.idLocalVotacion, LV.RazonSocial, Di.Detalle 'Distrito', P.Detalle 'Provincia' from LocalVotacion LV,  Distrito Di, Provincia P, Departamento De
		where  De.idDepartamento = P.idDepartamento and P.idProvincia = Di.idProvincia and De.Detalle = @Departamento and Di.idDistrito = LV.idDistrito
go

create procedure usp_EliminarUSP
  as begin
	declare @procedureName varchar(50)
	declare cur cursor for select name from sys.objects where type = 'p' and name like 'dbo.usp_%'

	open cur
	fetch next from cur into @procedureName
	while @@fetch_status = 0
		begin
			exec('drop procedure ' + @procedureName)
			fetch next from cur into @procedureName
		end
	close cur
	deallocate cur
  end
go

-- select * from Departamento
-- select * from LocalVotacion
-- select * from GrupoVotacion
-- DBCC CHECKIDENT (LocalVotacion, RESEED, 0)
-- delete GrupoVotacion
-- usp_getLocalesVotacionDepartamento 'AMERICA'

-- usp_getVotos 1,25
-- usp_getVotosDepartamento 'ASIA'
-- usp_getVotosProvincia 'JAPON'
-- usp_getGruposVotacion 1
-- usp_getGrupoVotacion '000001'
-- select * from vTotalVotos
/*
select 
	SUM(P1) AS Total_P1,
	CONCAT(ROUND((SUM(P1)*100.0)/(SUM(P1)+SUM(P2)),2),'%') AS Porcentaje_P1,
	SUM(P2) AS Total_P2,
	CONCAT(ROUND((SUM(P2)*100.0)/(SUM(P1)+SUM(P2)),2),'%') AS Porcentaje_P2,
    COUNT(*) AS Total_Actas,
	CONCAT(ROUND((COUNT(*)*100.0)/(SELECT COUNT(*) FROM GrupoVotacion),2),'%') AS Porcentaje_Actas,
    SUM(ElectoresHabiles) AS TotalElectoresHabiles,
	SUM(TotalVotantes) AS TotalCiudadanosVotaron,
	ROUND(AVG(TotalVotantes*1.0 / ElectoresHabiles * 100), 2) AS PorcentajeParticipacion,
    100 - ROUND(AVG(TotalVotantes * 100.0 / ElectoresHabiles), 2) AS PorcentajeAusentismo
	FROM GrupoVotacion

SELECT FORMAT(SUM(P1), 'N2') 'TotalVotosPPK', FORMAT(SUM(P2), 'N2') ' TotalVotosKeiko', CONCAT(CAST((SUM(TotalVotantes)-SUM(P1)) * 100.0 / SUM(TotalVotantes) AS decimal(10, 3)), ' %') '% VOTOS VÁLIDOSppk',
CONCAT(CAST((SUM(P2)) * 100.0 / SUM(TotalVotantes)  AS decimal(8, 3)), ' %' ) '% VOTOS VÁLIDOSkeyko',
format(COUNT(idEstadoActa),'N2') 'totalActas', format(COUNT(*),'N2')'totalActasProcesadas',format(COUNT(*),'N2') 'totalActasContabilizadas', FORMAT(SUM(ElectoresHabiles), 'N2')  'Electores Hábiles', FORMAT(SUM(TotalVotantes), 'N2') 'Ciudadanos que votaron ',
CONCAT ( CAST ( ( SUM(TotalVotantes) * 100.0 / SUM(TotalVotantes) ) as decimal (8,3) ), ' %' ) '% de participacion',
CONCAT ( CAST ( ( ( SUM(ElectoresHabiles) - SUM(TotalVotantes) ) * 100.0 / SUM(ElectoresHabiles) ) as decimal (8,3) ), ' %' ) '% de ausentismo'
FROM GrupoVotacion


select 
SUM(P1) 'NumeroP1', 
			CONCAT ( CAST ( ( SUM(P1) * 100.0 / SUM(P1+P2) ) as decimal (8,3) ), ' %' ) 'TotalP1',
			( SUM(P2) ) 'NumeroP2',
			CONCAT ( CAST ( ( SUM(P2) * 100.0 / SUM(P1+P2) ) as decimal (8,3) ), ' %' ) 'TotalP2',
			SUM(TotalVotantes) 'NumeroAsistentes', 
			CONCAT ( CAST ( ( SUM(TotalVotantes) * 100.0 / SUM(ElectoresHabiles) ) as decimal (8,3) ), ' %' ) 'TotalAsistentes',
			( SUM(ElectoresHabiles) - SUM(TotalVotantes) ) 'NumeroAusentes',
			CONCAT ( CAST ( ( ( SUM(ElectoresHabiles) - SUM(TotalVotantes) ) * 100.0 / SUM(ElectoresHabiles) ) as decimal (8,3) ), ' %' ) 'TotalAusentes',
			SUM(ElectoresHabiles) 'ElectoresHabiles'
			from GrupoVotacion


select	sum(p1) as 'Kambio', CONCAT ( CAST ( ( SUM(p1) * 100.0 / SUM(TotalVotantes) ) as decimal (8,3) ), ' %' ) 'Votok',sum(p2) as 'FP', CONCAT ( CAST ( ( SUM(p2) * 100.0 / SUM(TotalVotantes) ) as decimal (8,3) ), ' %' ) 'Votop',SUM(TotalVotantes) 'total', 
			CONCAT ( CAST ( ( SUM(TotalVotantes) * 100.0 / SUM(ElectoresHabiles) ) as decimal (8,3) ), ' %' ) 'Participacion',
			( SUM(ElectoresHabiles) - SUM(TotalVotantes) ) 'Ausentes',
			CONCAT ( CAST ( ( ( SUM(ElectoresHabiles) - SUM(TotalVotantes) ) * 100.0 / SUM(ElectoresHabiles) ) as decimal (8,3) ), ' %' ) 'TotalAusencia',
			SUM(ElectoresHabiles) 'ElectoresHabiles' ,count(*) as 'TotalActas', count(votosImpugnados) as 'Procesadas',count(votosImpugnados) as 'Contabilizadas'
			from GrupoVotacion
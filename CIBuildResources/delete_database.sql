USE [master]
GO

-- drop database
IF EXISTS(select * from sys.databases where name='SeleniumDemo')
DROP DATABASE [SeleniumDemo]
go
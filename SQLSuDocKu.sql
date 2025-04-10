create database SudockuLogin
Go
Use SudockuLogin

create table LoginInformation(
TK varchar(50) primary key,
MK varchar(64),
Email Nvarchar(150),
Quyen Nvarchar(50),
Score Int
);

ALTER TABLE LoginInformation
ADD FailedAttempts INT DEFAULT 0,
    LockoutEnd DATETIME NULL;
insert into LoginInformation (TK, MK, Email, Quyen) values ('nghipham0304','123','nghipham034@gmail.com','admin'),
							 ('phuchong0102','123','phuchong010204@gmail.com','admin'),
							  ('yennguyen1121','123','yennguyen1121@gmail.com','admin')

				
				select * from 	LoginInformation
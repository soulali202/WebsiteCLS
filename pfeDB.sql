use master 
go 
create database webCLS
use webCLS
go
create table Branche
  (
     ID_branche int primary key identity(1,1),
	 Nom_branche varchar(100)
  )
go 
create table Inscription 
  (
     id_part int primary key identity(1,1) ,
     Nom_part varchar(20) not null,
     Prenom_part varchar(20) not null,
	 Age_part int,
     Email_part varchar(100) ,
     Profil_part varchar(30),
     Adresse_part varchar(100),
     Tele_part varchar(20),
     Date_Inscr date default getDate(),
	 ID_branche int foreign key references Branche (ID_branche)
  )
go
create table Contact
  (
    id_msg int primary key identity(1,1) ,
	Nom_msg varchar(20) not null,
    Email_msg varchar(100) not null,
	Objet_msg varchar(200) ,
	Message_msg varchar(1000)
  )
go
create table Utilisateur
  (
    id_Uti int primary key identity(1,1) ,
	Nom_Uti varchar(100),
	motPasse varchar(10),
	Role varchar(10)
  )
 go
 create table Categorie
  (
     Id_Cat int primary key identity(1,1),
	 Nom_Cat varchar(50)
  )
 go
 create table Offre
  (
    Id_Off int primary key identity(1,1),
	Tittre_Off varchar(100),
	Descr_Off varchar(max),
	Id_Cat int foreign key references Categorie(Id_Cat)
  )

 select*from Inscription
  select*from Contact



  insert into Branche values('Francais'),('Anglais'),('Allemand'),('Calcul Mental'),('Cuisine')
  insert into Utilisateur values('Leiyla','Cls123','')
  select*from Utilisateur

  
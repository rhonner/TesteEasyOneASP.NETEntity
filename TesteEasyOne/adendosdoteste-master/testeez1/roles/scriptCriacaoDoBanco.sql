drop table administrativo
delete from userRoles where UserRoleId > 3
DBCC CHECKIDENT ('UserRoles', RESEED, 3)
delete from users where UserId > 3
DBCC CHECKIDENT ('users', RESEED, 3)
go
drop table consulta    --drop = deletar tabela inteira
go
drop table medico		--drop = deletar tabela inteira
go
drop table especialidade	--drop = deletar tabela inteira
go
drop table paciente			--drop = deletar tabela inteira
go
create table administrativo(	--criar table com nome "administrativo"
id int primary key identity(1,1),	--campo da tabela com nome "id" do tipo inteiro, este campo será o identificador da tabela, por isso será chave primária (primary key). será identity para gerar valores automáricos (não preciso inserir neste campo, ele irá começar no 1 e erá aumentar de 1 em 1 com cada registro adicionado)
nome varchar(150) not null,			--campo da tabela com nome "nome" do tipo varchar(150)(string com espaço para 150 characteres) e será not null (se eu for fazer um registro, o campo nome nao pode estar vazio)
email varchar(100) not null,		--mesma coisa com tamanho do campo de 100
idUser int,							--campo da tabela idUser do tipo inteiro
constraint FkAdministrativoUsers foreign key (idUser) references Users(UserId) -- crio uma constraint (identificador de ações) com nome "FkAdministrativoUsers" que será uma foreign key(chave estrangeira)fazendo com que o campo idUser faça referencia ao campo UserId da tabela User
) --chaves estrangeiras fazem com que duas tabelas possam "compartilhar dados", por exempo, um registro da tabela "administrativo" possui um idUser, este idUser quer dizer que um "administrativo" tem dados em outra tabela também, e com este idUser eu consigo pegar seus dados da outra tabela.
--todos os idUser devem existir na tabela User(UserId) senão da erro.
go
create table paciente(
id int primary key identity(1,1),
nome varchar(150)not null,
cpf varchar(11)not null,
email varchar(100),
sangue varchar(12),
sexo varchar(10),
idUser int,
constraint FkPacienteUsers foreign key (idUser) references Users(UserId)
)
go
create table especialidade(
id int primary key identity(1,1),
nome varchar(100)
) 
go
create table medico(
id int primary key identity(1,1),
nome varchar(150),
crm int not null,
idEspecialidade int,
constraint FkMedicoEspecialidade foreign key (idEspecialidade) references especialidade(id)
)
go
create table consulta(
id int primary key identity(1,1),
idMedico int not null,
idPaciente int not null,
descricao varchar(255),
dataa Datetime,
constraint FkConsultaMedico foreign key (idMedico) references medico(id),
constraint FkConsultaPaciente foreign key (idPaciente) references paciente(id),
CONSTRAINT UkMedicoData UNIQUE (idMedico,dataa)--constraint de nome "UkMedicoData" sendo uma UNIQUE para os campos (idMedico e dataa), isto faz com que um registro na tabela "consulta" so possa ter 1 registro tendo o idMedico e dataa
--por exemplo. se eu tenho um registro de consulta com idMedico = 2 e dataa = 02/09/2019, nao poderei fazer outro registro de consulta com o idMedico = 2 e a dataa = 02/09/2019
)
go
drop trigger tgAddAdministrativo --nao veja isso
go
create trigger tgAddAdministrativo on administrativo after insert --nao veja isso
as
begin
	declare
	@idRole int,
	@idAdministrativo int,
	@nomeAdministrativo varchar(150),
	@emailAdministrativo varchar(100),
	@senha varchar(200),
	@idUsu int

	--seta as variaveis
	select @idAdministrativo = inserted.id,@nomeAdministrativo = inserted.nome, @emailAdministrativo = inserted.email from inserted
	
	--cria uma senha para novos administrativos
		select @senha = @nomeAdministrativo + CAST(@idAdministrativo as varchar(100))
		
		--insere um usuario nova senha para os novos usuarios 
	insert into Users (UserName,Password,UserEmailAddress) values (@nomeAdministrativo,@senha,@emailAdministrativo)
	
	--pega o valor do id do usuario criado anteriormente
	select @idUsu = max(UserId) from Users
	update administrativo set idUser = @idUsu where id = @idAdministrativo
	--insere na tabela UserRoles os valores respectivos (id pego anteriormente e tipo de usuario (administrativo = 1))
	insert into UserRoles (UserId,RoleId) values (@idUsu,1)
	
end
go--@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@Presta atençao aqui jessica. Aqui começam os inserts de registros
--@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@				INSERTS
namespace insertsAdministrativo
INSERT INTO administrativo([nome],[email]) VALUES('Karleigh','quam@erategettincidunt.co.uk');--insert na tabela administrativo nos campos nome e email com os valores ....
--note que nao preciso inserir nada no campo Id, pois ele esta com o "identity", entao a cada registro que eu fizer, automaticamente ele colocara um id, que sendo "identity(1,1) ira de 1 em 1 (1,2,3,4,5...)"

INSERT INTO administrativo([nome],[email]) VALUES('Jamal','justo@nisi.ca');

INSERT INTO administrativo([nome],[email]) VALUES('Chester','magna.Ut@maurisInteger.net');

INSERT INTO administrativo([nome],[email]) VALUES('Amethyst','quam.elementum.at@luctusaliquetodio.ca');

INSERT INTO administrativo([nome],[email]) VALUES('Hanna','Proin.sed.turpis@Namligula.co.uk');

INSERT INTO administrativo([nome],[email]) VALUES('Kuame','Curae.Phasellus.ornare@eratvolutpatNulla.ca');

INSERT INTO administrativo([nome],[email]) VALUES('Marny','tristique@dolor.com');

INSERT INTO administrativo([nome],[email]) VALUES('Evelyn','Integer.sem.elit@mollislectus.edu');

INSERT INTO administrativo([nome],[email]) VALUES('Martena','quam.Curabitur@mollisIntegertincidunt.ca');

INSERT INTO administrativo([nome],[email]) VALUES('Ivana','blandit.Nam.nulla@ipsumnonarcu.com');

INSERT INTO administrativo([nome],[email]) VALUES('Ezekiel','sagittis@euerosNam.ca');

INSERT INTO administrativo([nome],[email]) VALUES('Ivan','magna.tellus@nisl.com');

INSERT INTO administrativo([nome],[email]) VALUES('Ann','est.mollis@augueid.org');

INSERT INTO administrativo([nome],[email]) VALUES('Sybil','vulputate.dui@nequevenenatislacus.co.uk');

INSERT INTO administrativo([nome],[email]) VALUES('Kessie','dictum.augue@Sed.com');

INSERT INTO administrativo([nome],[email]) VALUES('Karly','Aliquam.gravida@justo.ca');

INSERT INTO administrativo([nome],[email]) VALUES('Elaine','augue.eu@egestashendreritneque.net');

INSERT INTO administrativo([nome],[email]) VALUES('Pearl','pede@tincidunt.edu');

INSERT INTO administrativo([nome],[email]) VALUES('Curran','tristique.senectus.et@dapibusid.edu');

INSERT INTO administrativo([nome],[email]) VALUES('Leah','fringilla.ornare@at.ca');
go
drop trigger tgAddPaciente --nao olhar
go
create trigger tgAddPaciente on paciente after insert --nao olhar
as
begin
	declare
	@idRole int,
	@idPaciente int,
	@cpfPaciente varchar(12),
	@nomePaciente varchar(150),
	@emailPaciente varchar(100),
	@senha varchar(200),
	@idUsu int

	--seta as variaveis
	select @cpfPaciente = inserted.cpf,@idPaciente = inserted.id,@nomePaciente = inserted.nome, @emailPaciente = inserted.email from inserted
	
	--cria uma senha para novos administrativos
		select @senha = @cpfPaciente
		
		--insere um usuario nova senha para os novos usuarios 
	insert into Users (UserName,Password,UserEmailAddress) values (@nomePaciente,@senha,@emailPaciente)
	
	--pega o valor do id do usuario criado anteriormente
	select @idUsu = max(UserId) from Users
	update paciente set idUser = @idUsu where id = @idPaciente
	--insere na tabela UserRoles os valores respectivos (id pego anteriormente e tipo de usuario (administrativo = 1))
	insert into UserRoles (UserId,RoleId) values (@idUsu,2)
	
end
go

namespace insertsPaciente    --inserts paciente
INSERT INTO paciente([nome],[cpf],[email],[sangue],[sexo]) VALUES('Indigo','16441010449','venenatis.a.magna@nisl.ca','AB+','F');
INSERT INTO paciente([nome],[cpf],[email],[sangue],[sexo]) VALUES('Caleb','16120320929','enim@tellus.net','O+','M');
INSERT INTO paciente([nome],[cpf],[email],[sangue],[sexo]) VALUES('Kylee','16580525049','non.feugiat@duiFuscealiquam.org','B+','F');
INSERT INTO paciente([nome],[cpf],[email],[sangue],[sexo]) VALUES('Harrison','16480920399','nibh.sit@magnisdisparturient.co.uk','B+','M');
INSERT INTO paciente([nome],[cpf],[email],[sangue],[sexo]) VALUES('Amir','16960911639','molestie.tortor.nibh@primisin.co.uk','O+','F');
INSERT INTO paciente([nome],[cpf],[email],[sangue],[sexo]) VALUES('Chancellor','16430104979','Aliquam@diamvel.co.uk','AB+','M');
INSERT INTO paciente([nome],[cpf],[email],[sangue],[sexo]) VALUES('Denise','16110324299','sollicitudin.commodo@Nuncsed.org','O+','F');
INSERT INTO paciente([nome],[cpf],[email],[sangue],[sexo]) VALUES('Wang','16090906699','urna.Vivamus@id.edu','B-','M');
INSERT INTO paciente([nome],[cpf],[email],[sangue],[sexo]) VALUES('Halee','16791103699','tellus@torquentper.org','O+','M');
INSERT INTO paciente([nome],[cpf],[email],[sangue],[sexo]) VALUES('Sara','16041220389','molestie.tellus@auctornon.ca','B+','M');
INSERT INTO paciente([nome],[cpf],[email],[sangue],[sexo]) VALUES('Octavia','16460125499','et.malesuada.fames@id.net','B-','M');
INSERT INTO paciente([nome],[cpf],[email],[sangue],[sexo]) VALUES('Christian','16901845899','condimentum.eget@faucibusorciluctus.ca','AB+','M');
INSERT INTO paciente([nome],[cpf],[email],[sangue],[sexo]) VALUES('Susan','16691029199','nascetur.ridiculus.mus@mieleifendegestas.com','B-','F');
INSERT INTO paciente([nome],[cpf],[email],[sangue],[sexo]) VALUES('Brenna','16320120099','Pellentesque.ut.ipsum@aliquetProin.ca','AB+','F');
INSERT INTO paciente([nome],[cpf],[email],[sangue],[sexo]) VALUES('Xander','16360722699','nec@tempor.org','AB-','F');
INSERT INTO paciente([nome],[cpf],[email],[sangue],[sexo]) VALUES('Oscar','16631204899','gravida.molestie.arcu@ipsum.net','O-','M');
INSERT INTO paciente([nome],[cpf],[email],[sangue],[sexo]) VALUES('Tamara','16490315799','quis@turpis.co.uk','AB+','M');
INSERT INTO paciente([nome],[cpf],[email],[sangue],[sexo]) VALUES('Hasad','16590322099','eu@pharetrafeliseget.ca','B-','F');
INSERT INTO paciente([nome],[cpf],[email],[sangue],[sexo]) VALUES('Walter','16920034499','natoque@torquent.com','O-','F');
INSERT INTO paciente([nome],[cpf],[email],[sangue],[sexo]) VALUES('Levi','16790610799','nonummy.ut.molestie@ut.com','A+','F');
INSERT INTO paciente([nome],[cpf],[email],[sangue],[sexo]) VALUES('paciente','16792610799','paciente@paciente.co','O+','M');
go

namespace insertsEspecialidade --inserts especialidade. note que nao preciso inserir o id pois esta como identity
insert into especialidade values('pediatra'),
('cirurgiao'),
('cardiologia'),
('geriatra'),
('oftaumologista'),
('clinico geral'),
('andrologista'),
('ginecologista'),
('neuro'),
('oculista')
go

namespace insertsMedico
INSERT INTO medico([nome],[crm],[idEspecialidade]) VALUES('Iliana C. Waller',3725,1);--inserts nos medicos. Note que os numeros que sao inseridos no campo de "idEspecialidade" vao apenas até 10, pois há apenas 10 especialidades cadastradas e como disse, nao posso inserir dados na chave estrangeira que nao existam na tabela referenciada
INSERT INTO medico([nome],[crm],[idEspecialidade]) VALUES('Seth S. Leblanc',7941,2);
INSERT INTO medico([nome],[crm],[idEspecialidade]) VALUES('Blake Z. Burton',7665,3);
INSERT INTO medico([nome],[crm],[idEspecialidade]) VALUES('Diana S. Avery',7459,4);
INSERT INTO medico([nome],[crm],[idEspecialidade]) VALUES('Reed Q. Mcmahon',9505,5);
INSERT INTO medico([nome],[crm],[idEspecialidade]) VALUES('Chava L. Mccullough',6284,6);
INSERT INTO medico([nome],[crm],[idEspecialidade]) VALUES('Joy M. Mcconnell',6861,7);
INSERT INTO medico([nome],[crm],[idEspecialidade]) VALUES('Rylee F. Foreman',5218,8);
INSERT INTO medico([nome],[crm],[idEspecialidade]) VALUES('Joan R. Pollard',5189,9);
INSERT INTO medico([nome],[crm],[idEspecialidade]) VALUES('Ora Q. Pacheco',3868,10);
INSERT INTO medico([nome],[crm],[idEspecialidade]) VALUES('medico10',1233,9);
INSERT INTO medico([nome],[crm],[idEspecialidade]) VALUES('medico9',5555,8);
INSERT INTO medico([nome],[crm],[idEspecialidade]) VALUES('medico8',4344,7);
INSERT INTO medico([nome],[crm],[idEspecialidade]) VALUES('medico7',3868,6);
INSERT INTO medico([nome],[crm],[idEspecialidade]) VALUES('medico6',5656,5);
INSERT INTO medico([nome],[crm],[idEspecialidade]) VALUES('medico5',3868,4);
INSERT INTO medico([nome],[crm],[idEspecialidade]) VALUES('medico4',3422,3);
INSERT INTO medico([nome],[crm],[idEspecialidade]) VALUES('medico3',7987,2);
INSERT INTO medico([nome],[crm],[idEspecialidade]) VALUES('medico2',9090,1);
INSERT INTO medico([nome],[crm],[idEspecialidade]) VALUES('medico1',7777,10);
go


namespace insertsConsulta
INSERT INTO consulta([idMedico],[idPaciente],[descricao],[dataa]) VALUES(1,1,'velit egestas lacinia. Sed congue, elit sed consequat auctor, nuncsasddassadsda sadsdasda sdadsasdaasd asdsda sda asdssdasdasdasd aasdsdasdaasdsdaasd sda sdsdasdaasdasd','2019-08-01 12:45:00');
INSERT INTO consulta([idMedico],[idPaciente],[descricao],[dataa]) VALUES(2,2,'ipsum. Donec sollicitudin adipiscing ligula. Aenean gravida nunc sed pede.','2019-08-02 12:45:00');
INSERT INTO consulta([idMedico],[idPaciente],[descricao],[dataa]) VALUES(3,3,'magnis dis parturient montes, nascetur ridiculus mus. Proin vel nisl.','2019-08-03 12:45:00');
INSERT INTO consulta([idMedico],[idPaciente],[descricao],[dataa]) VALUES(4,4,'lobortis ultrices. Vivamus rhoncus. Donec est. Nunc ullamcorper, velit in','2019-08-04 12:45:00');
INSERT INTO consulta([idMedico],[idPaciente],[descricao],[dataa]) VALUES(5,5,'justo. Praesent luctus. Curabitur egestas nunc sed libero. Proin sed','2019-08-05 12:45:00');
INSERT INTO consulta([idMedico],[idPaciente],[descricao],[dataa]) VALUES(6,6,'penatibus et magnis dis parturient montes, nascetur ridiculus mus. Aenean','2019-08-06 12:45:00');
INSERT INTO consulta([idMedico],[idPaciente],[descricao],[dataa]) VALUES(7,7,'quis, pede. Praesent eu dui. Cum sociis natoque penatibus et','2019-08-07 12:45:00');
INSERT INTO consulta([idMedico],[idPaciente],[descricao],[dataa]) VALUES(8,8,'enim. Etiam gravida molestie arcu. Sed eu nibh vulputate mauris','2019-08-08 12:45:00');
INSERT INTO consulta([idMedico],[idPaciente],[descricao],[dataa]) VALUES(9,9,'luctus vulputate, nisi sem semper erat, in consectetuer ipsum nunc','2019-08-09 12:45:00');
INSERT INTO consulta([idMedico],[idPaciente],[descricao],[dataa]) VALUES(10,10,'risus. Donec egestas. Aliquam nec enim. Nunc ut erat. Sed','2019-08-10 12:45:00');
INSERT INTO consulta([idMedico],[idPaciente],[descricao],[dataa]) VALUES(1,11,'vitae nibh. Donec est mauris, rhoncus id, mollis nec, cursus','2019-08-11 12:45:00');
INSERT INTO consulta([idMedico],[idPaciente],[descricao],[dataa]) VALUES(2,12,'dui. Suspendisse ac metus vitae velit egestas lacinia. Sed congue,','2019-08-12 12:45:00');
INSERT INTO consulta([idMedico],[idPaciente],[descricao],[dataa]) VALUES(3,13,'Nunc mauris elit, dictum eu, eleifend nec, malesuada ut, sem.','2019-08-13 12:45:00');
INSERT INTO consulta([idMedico],[idPaciente],[descricao],[dataa]) VALUES(4,14,'tortor. Nunc commodo auctor velit. Aliquam nisl. Nulla eu neque','2019-08-14 12:45:00');
INSERT INTO consulta([idMedico],[idPaciente],[descricao],[dataa]) VALUES(5,15,'adipiscing fringilla, porttitor vulputate, posuere vulputate, lacus. Cras interdum. Nunc','2019-08-14 12:45:00');
INSERT INTO consulta([idMedico],[idPaciente],[descricao],[dataa]) VALUES(6,16,'neque. In ornare sagittis felis. Donec tempor, est ac mattis','2019-08-15 12:45:00');
INSERT INTO consulta([idMedico],[idPaciente],[descricao],[dataa]) VALUES(7,17,'et libero. Proin mi. Aliquam gravida mauris ut mi. Duis','2019-08-15 10:45:00');
INSERT INTO consulta([idMedico],[idPaciente],[descricao],[dataa]) VALUES(8,18,'Cum sociis natoque penatibus et magnis dis parturient montes, nascetur','2019-08-16 15:30:00');
INSERT INTO consulta([idMedico],[idPaciente],[descricao],[dataa]) VALUES(9,19,'risus varius orci, in consequat enim diam vel arcu. Curabitur','2019-08-16 15:45:00');
INSERT INTO consulta([idMedico],[idPaciente],[descricao],[dataa]) VALUES(10,20,'nunc sed pede. Cum sociis natoque penatibus et magnis dis','2019-08-05 12:45:00');
INSERT INTO consulta([idMedico],[idPaciente],[descricao],[dataa]) VALUES(7,21,'nunc sed pede. Cum sociis natoque penatibus et magnis dis','2019-08-05 12:45:00');
INSERT INTO consulta([idMedico],[idPaciente],[descricao],[dataa]) VALUES(8,21,'nunc sed pede. Cum sociis natoque penatibus et magnis dis','2019-09-05 13:00:00');
go
drop table sangue
go
create table sangue (
id int primary key identity(1,1),
tipo varchar(3)not null

)

go
insert into sangue values ('A+'),('A-'),('B+'),('B-'),('AB+'),('AB-'),('O+'),('O-')
go
Alter table paciente add TipoSanguineo int foreign key references sangue(id) --alterei a tabela paciente adicionando um campo com nome "TipoSanguineo" do tipo int sendo uma chave estrangeira referenciando o campo id da tabela sangue
go
update paciente set paciente.TipoSanguineo = s.id from paciente as p join sangue as s on p.sangue = s.tipo where s.tipo = p.sangue --atualizando a tabela paciente e colocando no campoTipoSanguineo o valor do id da tabela sangue onde o campo "sangue" da tabela "paciente" seja igual ao campo "tipo" da tabela "sangue"
go
alter table especialidade add descricao varchar(255)--alterei a tabela especialiadde e adicionei o campo "descricao" do tipo varchar com 255characteres
go
update especialidade set descricao = 'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Suspendisse nisl nunc, commodo laoreet dui egestas, mollis finibus enim. Aenean sit.' where id>0 --estou atualizando o campo "descricao" da tabela "especialidade" colocando o texto que esta entre aspas onde o id seja maior que 0 (resumindo, para todos)
go
alter table paciente drop column sangue --deletei o campo sangue da tabela paciente
go
create view AgendaMedica as -- nao olhe
select m.nome as nomeMedico, p.nome as pacienteNome, p.sexo as pacienteSexo, s.tipo as pacienteSangue,
e.nome as especialidade, c.descricao as consultaDescricao,c.dataa as dataConsulta from medico m, paciente p, sangue s, especialidade e, consulta c
where m.id = c.idMedico and p.id = c.idPaciente and p.TipoSanguineo = s.id and m.idEspecialidade = e.id 
and cast(dataa as date) = cast(getdate() as date)
go
--update paciente set paciente.TipoSanguineo = s.id from paciente as p join sangue as s on p.sangue = s.tipo where s.tipo = p.sangue update para adicionar tipo sanguineo
go
drop trigger tgAddAdministrativo
go
drop trigger tgAddPaciente
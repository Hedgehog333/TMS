# TMS

## Tech
* WPF
* ADO .NET
* XML

#### Databese Microsoft SQL Server: 
Table Role:
```sql
create table [Roles]
(
	[id] int primary key identity(1,1) not null,
	[name] varchar(24) not null
);
```
Table User:
```sql
create table [Users](
	[id] int primary key identity(1,1) not null,
	[fName] varchar(36) not null,
	[lName] varchar(36) not null,
	[sName] varchar(36),
	[email] varchar(64) not null,
	[password] varchar(64) not null,
	[roleId] int not null
	foreign key references [Roles]([id])
	on update cascade
	on delete cascade,
	[registrationDate] datetime default getdate(),
	[lastOnlineDate] datetime,
	check([registrationDate] <= [lastOnlineDate])
);
```
Table Categories:
```sql
create table [Categories]
(
	[id] int primary key identity(1,1) not null,
	[title] varchar(24) not null
);
```
Table Tests:
```sql
create table [Tests](
	[id] int primary key identity(1,1) not null,
	[title] varchar(255) not null,
	[description] varchar(512) not null,
	[categoriesId] int not null
	foreign key references [Categories]([id])
	on update cascade
	on delete cascade,
	[creationDate] datetime default getdate(),
	[lastModefied] datetime,
	check([creationDate] <= [lastModefied]),
	[authorId] int not null
	foreign key references [Users]([id])
	on update cascade
	on delete cascade,
	[isDraft] bit not null default 1
);
```
Table Questions:
```sql
create table [Questions](
	[id] int primary key identity(1,1) not null,
	[body] varchar(4096) not null,
	[testId] int not null
	foreign key references [Tests]([id])
	on update cascade
	on delete cascade,
	[isFowAnswers] bit not null,
	[isDraft] bit not null default 1,
);
```
Table Answers:
```sql
create table [Answers](
	[id] int primary key identity(1,1) not null,
	[body] varchar(1024) not null,
	[isCorrect] bit not null,
	[questionId] int not null
	foreign key references [Questions]([id])
	on update cascade
	on delete cascade,
	[isDraft] bit not null default 1,
);
```
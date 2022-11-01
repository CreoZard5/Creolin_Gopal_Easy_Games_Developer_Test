Create database EasyGames_Developer_Assesment; 
use EasyGames_Developer_Assesment;

Create Table Client
(
ClientID int not null Primary key,
ClientName nvarchar(50)not null,
ClientSurname nvarchar(50)not null,
ClientBalance decimal(18,2)not null
);

Create Table TransactionType
(
TransactionTypeID smallint not null Primary key,
TransactionTypeName varchar(50)not null);

Create Table TransactionTbl
(
TransactionID bigint not null Primary key,
TransactionAmount decimal(18,2),
TransactionTypeID smallint not null,
ClientID int not null,
TransactionComment nVarchar(100),
FOREIGN KEY (TransactionTypeID) references TransactionType(TransactionTypeID),
FOREIGN KEY (ClientID) references Client(ClientID)
);

Drop table Client;
Drop table TransactionType;
Drop table TransactionTbl;

insert into Client (ClientID,ClientName,ClientSurname,ClientBalance)
	VALUES 
		(1,'Peter','Parker',1000.00), 
		(2,'Tony','Stark',800000.00),
		(3,'Bruce','Banner',254111.00);



insert into TransactionType (TransactionTypeID,TransactionTypeName)
	VALUES 
		(1,'Debit'), 
		(2,'Credit');

insert into TransactionTbl (TransactionID,TransactionAmount,TransactionTypeID,ClientID,TransactionComment)
	VALUES 
		(1,1000.00,1,1,'Winnings'), 
		(4,-500.00,2,3,'losing'), 
		(5,-9000.00,2,2,'losing');


Select * from Client;
Select * from TransactionType;
Select * from TransactionTbl;

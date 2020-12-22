
create table [Clients](
   [ID]    INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL
  ,[Name]  varchar(max) NULL
  ,[DateRegister] datetime
  
  ,CONSTRAINT [PK_Clients] PRIMARY KEY NONCLUSTERED ([ID] ASC)
)

create table [Orders](
   [ID]    INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL
  ,[ClientID]   INT NOT NULL
  ,[Count]  int NULL
  ,[Date] datetime

   ,CONSTRAINT [FK_Orders_ClientID] FOREIGN KEY ([ClientID]) REFERENCES Clients ([ID])
)

insert into Clients ([Name], [DateRegister])
values 
    ('Mike', DATEADD(DAY, -20, GETDATE())),
    ('Fill', DATEADD(DAY, -10, GETDATE())),
    ('Anton', DATEADD(DAY, -10, GETDATE())),
    ('Denis', DATEADD(DAY, -1, GETDATE()))

insert into Orders ([ClientID], [Count], [Date])
values 
    (1, 11, DATEADD(DAY, -15, GETDATE())),
    (2, 22, DATEADD(DAY, -2, GETDATE())),
    (3, 33, DATEADD(DAY, -7, GETDATE())),
    (3, 44, DATEADD(DAY, -8, GETDATE()))

--select * from Clients
--select * from Orders

;with cc (ID, FirstShopDate) as
(
    select 
        c.ID
        ,Min(o.Date) FirstShopDate
    from Clients c
    left join Orders o on o.ClientID = c.ID
    group by c.ID
)
select 
    c.*,
    cc.FirstShopDate,
    DATEDIFF(day, c.[DateRegister], cc.FirstShopDate) as [Date Dif]
from Clients c
inner join cc on cc.ID = c.ID
where DATEDIFF(day, c.[DateRegister], cc.FirstShopDate) <= 5


delete Orders
drop table Orders
delete Clients
drop table Clients

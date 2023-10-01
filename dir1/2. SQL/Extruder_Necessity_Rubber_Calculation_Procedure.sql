DECLARE @Ref varchar (5) = 'P7719'


IF EXISTS (
		  select 0 from gProducts as P
			inner join (select P.ID as RubberId, R.quantity as RubberQty, R.productId from gRecipes as R
			inner join gProducts as P on P.ID = R.componentId where P.typeId = 4) as R on R.productId = P.ID
			where P.code like @Ref
		 )
		BEGIN
			select 0 from gProducts as P
			inner join (select P.ID as RubberId, R.quantity as RubberQty, R.productId from gRecipes as R
			inner join gProducts as P on P.ID = R.componentId where P.typeId = 4) as R on R.productId = P.ID
			where P.code like @Ref
			
		END
	ELSE
		BEGIN
			Print 'Does not exist'
		END


select 0 from gProducts as P
left join (select P.ID as RubberId, R.quantity as RubberQty, R.productId from gRecipes as R
inner join gProducts as P on P.ID = R.componentId where P.typeId = 4) as R on R.productId = P.ID
where P.code like 'P7719'


select top 1 * from gProducts as P
left join (select P.ID as RubberId, R.quantity as RubberQty, R.productId from gRecipes as R
inner join gProducts as P on P.ID = R.componentId where P.typeId = 4) as R on R.productId = P.ID
where P.code like 'P7719'

select top 10 * from gRecipes

select * from gProductType

select top 1 R.S3Id from gProducts as P
left join (select P.ID as S3Id, R.productId from gRecipes as R
inner join gProducts as P on P.ID = R.componentId where P.typeId = 3) as R on R.productId = P.ID
where P.code like 'P7719'

select * from gProducts where id = 66
select * from PROSTOCKDF where PLAN_NO = 'S82002170214-017' --and LOT_NO = '3BF229'
select * from IODATADF where SHEET_NO like 'S82002170214-017'

Update PROSTOCKDF set Quantity = 0.203040 where PLAN_NO = 'S82002170214-017' and LOT_NO = '3BF229'

select * from plnmolddf where plan_no like '2390026' 
select * from plnshiftdf where plan_no like '2390026' order by plan_date

delete from plnshiftdf where plan_no like '2370084' and plan_date > '2023-07-15 00:00:00'
delete from plnmolddf where plan_no like '2290187'

update plnmolddf set END_TIME = '2023-07-14 00:00:00', END_SHIFT = '3' where plan_no like '2370084'
update plnmolddf set START_TIME = '2023-05-27 00:00:00', START_SHIFT = '2' where plan_no like '2350029'


select * from plnmolddf where plan_no like '2330040'
select * from plnshiftdf where plan_no like '2330040' order by plan_date

update plnshiftdf set PLAN_DATE = '2023-09-02 00:00:00' where plan_no like '2390026' and DATEDIFF(DAY,Plan_date, '2023-09-04 00:00:00')=0
update plnshiftdf set PLAN_DATE = '2023-09-04 00:00:00' where plan_no like '2390026' and DATEDIFF(DAY,Plan_date, '2023-09-05 00:00:00')=0
update plnshiftdf set PLAN_DATE = '2023-09-05 00:00:00' where plan_no like '2390026' and DATEDIFF(DAY,Plan_date, '2023-09-06 00:00:00')=0
update plnshiftdf set PLAN_DATE = '2023-09-06 00:00:00' where plan_no like '2390026' and DATEDIFF(DAY,Plan_date, '2023-09-07 00:00:00')=0
update plnshiftdf set PLAN_DATE = '2023-09-07 00:00:00' where plan_no like '2390026' and DATEDIFF(DAY,Plan_date, '2023-09-08 00:00:00')=0
update plnshiftdf set PLAN_DATE = '2023-09-08 00:00:00' where plan_no like '2390026' and DATEDIFF(DAY,Plan_date, '2023-09-11 00:00:00')=0
update plnshiftdf set PLAN_DATE = '2023-09-09 00:00:00' where plan_no like '2390026' and DATEDIFF(DAY,Plan_date, '2023-09-12 00:00:00')=0
update plnshiftdf set PLAN_DATE = '2023-09-11 00:00:00' where plan_no like '2390026' and DATEDIFF(DAY,Plan_date, '2023-09-13 00:00:00')=0
update plnshiftdf set PLAN_DATE = '2023-09-12 00:00:00' where plan_no like '2390026' and DATEDIFF(DAY,Plan_date, '2023-09-14 00:00:00')=0
update plnshiftdf set PLAN_DATE = '2023-09-13 00:00:00' where plan_no like '2390026' and DATEDIFF(DAY,Plan_date, '2023-09-15 00:00:00')=0
update plnshiftdf set PLAN_DATE = '2023-09-14 00:00:00' where plan_no like '2390026' and DATEDIFF(DAY,Plan_date, '2023-09-18 00:00:00')=0
update plnshiftdf set PLAN_DATE = '2023-09-15 00:00:00' where plan_no like '2390026' and DATEDIFF(DAY,Plan_date, '2023-09-19 00:00:00')=0
update plnshiftdf set PLAN_DATE = '2023-09-16 00:00:00' where plan_no like '2390026' and DATEDIFF(DAY,Plan_date, '2023-09-20 00:00:00')=0
update plnshiftdf set PLAN_DATE = '2023-09-18 00:00:00' where plan_no like '2390026' and DATEDIFF(DAY,Plan_date, '2023-09-21 00:00:00')=0
update plnshiftdf set PLAN_DATE = '2023-09-19 00:00:00' where plan_no like '2390026' and DATEDIFF(DAY,Plan_date, '2023-09-22 00:00:00')=0
update plnshiftdf set PLAN_DATE = '2023-09-20 00:00:00' where plan_no like '2390026' and DATEDIFF(DAY,Plan_date, '2023-09-25 00:00:00')=0
update plnshiftdf set PLAN_DATE = '2023-09-21 00:00:00' where plan_no like '2390026' and DATEDIFF(DAY,Plan_date, '2023-09-26 00:00:00')=0
update plnshiftdf set PLAN_DATE = '2023-09-22 00:00:00' where plan_no like '2390026' and DATEDIFF(DAY,Plan_date, '2023-09-27 00:00:00')=0
update plnshiftdf set PLAN_DATE = '2023-09-23 00:00:00' where plan_no like '2390026' and DATEDIFF(DAY,Plan_date, '2023-09-28 00:00:00')=0
update plnshiftdf set PLAN_DATE = '2023-09-25 00:00:00' where plan_no like '2390026' and DATEDIFF(DAY,Plan_date, '2023-09-29 00:00:00')=0
update plnmolddf set --START_TIME = '2023-03-06 00:00:00', 
	END_TIME = '2023-09-29 00:00:00' where plan_no like '2390026'

update gplnshiftdf set PLAN_QTY = 2000 where data_no in ('176522', '176523', '176524')

update gplnmolddf set PLAN_QTY = 6000 where plan_no like 'G22X066'

delete from gplnshiftdf where plan_no like 'G22X026' and plan_date > '2022-10-31'
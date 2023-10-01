SELECT df.Lot_no, lt.LOT_QTY, lt.INPUT_DATE, Defect_Name, repair_qty, md.MC_NO FROM GDEFPRODF as df
left join gshiagelotdf as lt on lt.LOT_NO = df.LOT_NO
left join GPLNMOLDDF as md on md.PLAN_NO = lt.PLAN_NO
where repair_qty > 0 --(LOT_NO LIKE 'IG4902%') 
and df.lot_no in (select lot_no from gshiagelotdf where INPUT_DATE between('2023-07-04 00:00:00') and ('2023-07-23 00:00:00'))


select * from gshiagelotdf 
where --pro_code1 like 'MD' and 
--start_time between('2023-06-01 00:00:00') and ('2023-07-01 00:00:00')
lot_no like 'IG4902235019-037'
order by start_time desc

select top 10 * from gdefprodf where defect_name like '%rew%'

select top 10 * from GSHIAGELOTDF
select top 10 * from GPLNMOLDDF order by START_TIME desc
select top 10 * from GPROLOTDF